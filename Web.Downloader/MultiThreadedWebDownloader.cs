/****************************** Module Header ******************************\
* Module Name:  MultiThreadedWebDownloader.cs
* Project:	    Web.Downloader
* Copyright (c) Microsoft Corporation.
* 
* This class is used to download files through internet using multiple threads. 
* It supplies public  methods to Start, Pause, Resume and Cancel a download. 
* 
* Before the download starts, the remote server should be checked 
* whether it supports "Accept-Ranges" header.
* 
* When the download starts, it will check whether the destination file exists. If
* not, create a file with the same size as the file to be downloaded, then
* creates multiple HttpDownloadClients to download the file in background threads.
* 
* It will fire a DownloadProgressChanged event when it has downloaded a
* specified size of data. It will also fire a DownloadCompleted event if the 
* download is completed or canceled.
* 
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
* All other rights reserved.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Web.Downloader
{
    public class MultiThreadedWebDownloader
    {
        // Used while calculating download speed.
        static object locker = new object();


        /// <summary>
        /// The Url of the file to be downloaded. 
        /// </summary>
        public Uri Url { get; private set; }

        /// <summary>
        /// Specify whether the remote server supports "Accept-Ranges" header.
        /// </summary>
        public bool IsRangeSupported { get; private set; }

        /// <summary>
        /// The total size of the file.
        /// </summary>
        public long TotalSize { get; private set; }

        private string downloadPath;

        /// <summary>
        /// The local path to store the file.
        /// If there is no file with the same name, a new file will be created.
        /// </summary>
        public string DownloadPath
        {
            get
            {
                return downloadPath;
            }
            set
            {
                if (this.Status != MultiThreadedWebDownloaderStatus.Checked)
                {
                    throw new ApplicationException("The path could only be set when the status is Checked.");
                }

                downloadPath = value;
            }
        }

        /// <summary>
        /// The Proxy of all the download client.
        /// </summary>
        public WebProxy Proxy { get; set; }

        /// <summary>
        /// The downloaded size of the file.
        /// </summary>
        public long DownloadedSize { get; private set; }


        // Store the used time spent in downloading data. The value does not include
        // the paused time and it will only be updated when the download is paused, 
        // canceled or completed.
        private TimeSpan usedTime = new TimeSpan();

        private DateTime lastStartTime;

        /// <summary>
        /// If the status is Downloading, then the total time is usedTime. Else the 
        /// total should include the time used in current download thread.
        /// </summary>
        public TimeSpan TotalUsedTime
        {
            get
            {
                if (this.Status != MultiThreadedWebDownloaderStatus.Downloading)
                {
                    return usedTime;
                }
                else
                {
                    return usedTime.Add(DateTime.Now - lastStartTime);
                }
            }
        }

        // The time and size in last DownloadProgressChanged event. These two fields
        // are used to calculate the download speed.
        private DateTime lastNotificationTime;

        private long lastNotificationDownloadedSize;

        private int bufferCount = 0;

        /// <summary>
        /// If get a number of buffers, then fire DownloadProgressChanged event.
        /// </summary>
        public int BufferCountPerNotification { get; private set; }

        /// <summary>
        /// Set the BufferSize when read data in Response Stream.
        /// </summary>
        public int BufferSize { get; private set; }

        /// <summary>
        /// The cache size in memory.
        /// </summary>
        public int MaxCacheSize { get; private set; }

        MultiThreadedWebDownloaderStatus status;

        /// <summary>
        /// If status changed, fire StatusChanged event.
        /// </summary>
        public MultiThreadedWebDownloaderStatus Status
        {
            get
            { return status; }

            private set
            {
                if (status != value)
                {
                    status = value;
                    this.OnStatusChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// The max threads count. The real threads count number is the min value of this
        /// value and TotalSize / MaxCacheSize.
        /// </summary>
        public int MaxThreadCount { get; private set; }

        // The HttpDownloadClients to download the file. Each client uses one thread to
        // download part of the file.
        List<HttpDownloadClient> downloadClients = null;

        public int DownloadThreadsCount
        {
            get
            {
                if (downloadClients != null)
                {
                    return downloadClients.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        public event EventHandler<MultiThreadedWebDownloaderProgressChangedEventArgs>
            DownloadProgressChanged;

        public event EventHandler<MultiThreadedWebDownloaderCompletedEventArgs>
            DownloadCompleted;

        public event EventHandler StatusChanged;

        public event EventHandler<ErrorEventArgs> ErrorOccurred;


        /// <summary>
        /// Download the whole file. The default buffer size is 1KB, memory cache is
        /// 1MB, buffer count per notification is 64, threads count is the double of 
        /// logic processors count.
        /// </summary>
        public MultiThreadedWebDownloader(string url)
            : this(url, 1024, 1048576, 256, Math.Min(Environment.ProcessorCount * 2, 4))
        {
        }

        public MultiThreadedWebDownloader(string url, int bufferSize, int cacheSize,
            int bufferCountPerNotification, int maxThreadCount)
        {

            if (bufferSize < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "BufferSize cannot be less than 0. ");
            }

            if (cacheSize < bufferSize)
            {
                throw new ArgumentOutOfRangeException(
                    "MaxCacheSize cannot be less than BufferSize ");
            }

            if (bufferCountPerNotification <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "BufferCountPerNotification cannot be less than 0. ");
            }

            if (maxThreadCount < 1)
            {
                throw new ArgumentOutOfRangeException(
                       "maxThreadCount cannot be less than 1. ");
            }

            this.Url = new Uri(url);
            this.BufferSize = bufferSize;
            this.MaxCacheSize = cacheSize;
            this.BufferCountPerNotification = bufferCountPerNotification;
            
            this.MaxThreadCount = maxThreadCount;
            
            // Set the maximum number of concurrent connections allowed by 
            // a ServicePoint object
            ServicePointManager.DefaultConnectionLimit = maxThreadCount;

            // Initialize the HttpDownloadClient list.
            downloadClients = new List<HttpDownloadClient>();

            // Set the idle status.
            this.status = MultiThreadedWebDownloaderStatus.Idle;
        }

        /// <summary>
        /// Check total size and IsRangeSupported of the file in remote server. 
        /// If there is no exception, then the file can be downloaded. 
        /// </summary>
        public void CheckFile()
        {

            // The file could be checked only in Idle status.
            if (this.status != MultiThreadedWebDownloaderStatus.Idle)
            {
                throw new ApplicationException(
                    "The file could be checked only in Idle status.");
            }

            // Check the file information on the remote server.
            var webRequest = (HttpWebRequest)WebRequest.Create(Url);

            // Set the proxy.
            if (Proxy != null)
            {
                webRequest.Proxy = Proxy;
            }

            using (var response = webRequest.GetResponse())
            {
                this.IsRangeSupported = response.Headers.AllKeys.Contains("Accept-Ranges");
                this.TotalSize = response.ContentLength;

                if (TotalSize <= 0)
                {
                    throw new ApplicationException(
                        "The file to download does not exist!");
                }
            }

            // Set the checked status.
            this.Status = MultiThreadedWebDownloaderStatus.Checked;
        }


        /// <summary>
        /// Check whether the destination file exists. If  not, create a file with the same
        /// size as the file to be downloaded.
        /// </summary>
        void CheckFileOrCreateFile()
        {
            // Lock other threads or processes to prevent from creating the file.
            lock (locker)
            {
                FileInfo fileToDownload = new FileInfo(DownloadPath);
                if (fileToDownload.Exists)
                {

                    // The destination file should have the same size as the file to be 
                    // downloaded.
                    if (fileToDownload.Length != this.TotalSize)
                    {
                        throw new ApplicationException(
                            "The download path already has a file which does not match"
                            + " the file to download. ");
                    }
                }

                // Create a file.
                else
                {
                    using (FileStream fileStream = File.Create(this.DownloadPath))
                    {
                        long createdSize = 0;
                        byte[] buffer = new byte[4096];
                        while (createdSize < TotalSize)
                        {
                            int bufferSize = (TotalSize - createdSize) < 4096 ?
                                (int)(TotalSize - createdSize) : 4096;
                            fileStream.Write(buffer, 0, bufferSize);
                            createdSize += bufferSize;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Start to download.
        /// </summary>
        public void Start()
        {
            // Check whether the destination file exist.
            CheckFileOrCreateFile();

            // Only Checked downloader can be started.
            if (this.status != MultiThreadedWebDownloaderStatus.Checked)
            {
                throw new ApplicationException(
                    "Only Checked downloader can be started. ");
            }

            // If the file does not support "Accept-Ranges" header, then create one 
            // HttpDownloadClient to download the file in a single thread, else create
            // multiple HttpDownloadClients to download the file.
            if (!IsRangeSupported)
            {
                HttpDownloadClient client = new HttpDownloadClient(
                    this.Url,
                    this.DownloadPath,
                    0,
                    long.MaxValue,
                    this.BufferSize,
                    this.BufferCountPerNotification * this.BufferSize);
                client.TotalSize = this.TotalSize;
                this.downloadClients.Add(client);
            }
            else
            {
                // Calculate the block size for each client to download.
                int maxSizePerThread =
                    (int)Math.Ceiling((double)this.TotalSize / this.MaxThreadCount);
                if (maxSizePerThread < this.MaxCacheSize)
                {
                    maxSizePerThread = this.MaxCacheSize;
                }

                long leftSizeToDownload = this.TotalSize;

                // The real threads count number is the min value of MaxThreadCount and 
                // TotalSize / MaxCacheSize.              
                int threadsCount =
                    (int)Math.Ceiling((double)this.TotalSize / maxSizePerThread);

                for (int i = 0; i < threadsCount; i++)
                {
                    long endPoint = maxSizePerThread * (i + 1) - 1;
                    long sizeToDownload = maxSizePerThread;

                    if (endPoint > this.TotalSize)
                    {
                        endPoint = this.TotalSize - 1;
                        sizeToDownload = endPoint - maxSizePerThread * i;
                    }

                    // Download a block of the whole file.
                    HttpDownloadClient client = new HttpDownloadClient(
                        this.Url,
                        this.DownloadPath,
                        maxSizePerThread * i,
                        endPoint);

                    client.TotalSize = sizeToDownload;
                    this.downloadClients.Add(client);
                }
            }

            // Set the lastStartTime to calculate the used time.
            lastStartTime = DateTime.Now;

            // Set the downloading status.
            this.Status = MultiThreadedWebDownloaderStatus.Downloading;

            // Start all HttpDownloadClients.
            foreach (var client in this.downloadClients)
            {
                if (this.Proxy != null)
                {
                    client.Proxy = this.Proxy;
                }

                // Register the events of HttpDownloadClients.
                client.DownloadProgressChanged +=
                    new EventHandler<HttpDownloadClientProgressChangedEventArgs>(
                        client_DownloadProgressChanged);

                client.StatusChanged += new EventHandler(client_StatusChanged);

                client.ErrorOccurred += new EventHandler<ErrorEventArgs>(
                    client_ErrorOccurred);
                client.Start();
            }


        }

        /// <summary>
        /// Pause the download.
        /// </summary>
        public void Pause()
        {
            // Only downloading downloader can be paused.
            if (this.status != MultiThreadedWebDownloaderStatus.Downloading)
            {
                throw new ApplicationException(
                    "Only downloading downloader can be paused.");
            }

            this.Status = MultiThreadedWebDownloaderStatus.Pausing;

            // Pause all the HttpDownloadClients. If all of the clients are paused,
            // the status of the downloader will be changed to Paused.
            foreach (var client in this.downloadClients)
            {
                if (client.Status != HttpDownloadClientStatus.Completed)
                {
                    client.Pause();
                }
            }
        }

        /// <summary>
        /// Resume the download.
        /// </summary>
        public void Resume()
        {
            // Only paused downloader can be paused.
            if (this.status != MultiThreadedWebDownloaderStatus.Paused)
            {
                this.OnError( new ErrorEventArgs{
                    Error = new ApplicationException(
                    "Only paused downloader can be resumed. ")
                });
                return;
            }

            // Set the lastStartTime to calculate the used time.
            lastStartTime = DateTime.Now;

            // Set the downloading status.
            this.Status = MultiThreadedWebDownloaderStatus.Downloading;

            // Resume all HttpDownloadClients.
            foreach (var client in this.downloadClients)
            {
                if (client.Status != HttpDownloadClientStatus.Completed)
                {
                    client.Resume();
                }
            }

        }

        /// <summary>
        /// Cancel the download
        /// </summary>
        public void Cancel()
        {
            // Only downloading downloader can be canceled.
            if (this.status != MultiThreadedWebDownloaderStatus.Downloading)
            {
                this.OnError(new ErrorEventArgs
                {
                    Error = new ApplicationException(
                    "Only downloading downloader can be canceled.")
                });
                return;
            }

            this.Status = MultiThreadedWebDownloaderStatus.Canceling;

            this.OnError(new ErrorEventArgs
                {
                    Error = new Exception("Download is canceled.")
                });

            // Cancel all HttpDownloadClients.
            foreach (var client in this.downloadClients)
            {
                if (client.Status != HttpDownloadClientStatus.Completed)
                {
                    client.Cancel();
                }
            }

        }

        /// <summary>
        /// Handle the StatusChanged event of all the HttpDownloadClients.
        /// </summary>
        void client_StatusChanged(object sender, EventArgs e)
        {

            // If all the clients are completed, then the status of this downloader is 
            // completed.
            if (this.downloadClients.All(
                client => client.Status == HttpDownloadClientStatus.Completed))
            {
                this.Status = MultiThreadedWebDownloaderStatus.Completed;
            }
            else
            {

                // The completed clients will not be taken into consideration.
                var nonCompletedClients = this.downloadClients.
                    Where(client => client.Status != HttpDownloadClientStatus.Completed);

                // If all the nonCompletedClients are Paused, then the status of this 
                // downloader is Paused.
                if (nonCompletedClients.All(
                    client => client.Status == HttpDownloadClientStatus.Paused))
                {
                    this.Status = MultiThreadedWebDownloaderStatus.Paused;
                }

                // If all the nonCompletedClients are Canceled, then the status of this 
                // downloader is Canceled.
                if (nonCompletedClients.All(
                    client => client.Status == HttpDownloadClientStatus.Canceled))
                {
                    this.Status = MultiThreadedWebDownloaderStatus.Canceled;
                }
            }

        }

        /// <summary>
        /// Handle the DownloadProgressChanged event of all the HttpDownloadClients, and 
        /// calculate the download speed.
        /// </summary>
        void client_DownloadProgressChanged(object sender, HttpDownloadClientProgressChangedEventArgs e)
        {
            lock (locker)
            {
                DownloadedSize += e.Size;
                bufferCount++;

                if (bufferCount == BufferCountPerNotification)
                {
                    if (DownloadProgressChanged != null)
                    {
                        int speed = 0;
                        DateTime current = DateTime.Now;
                        TimeSpan interval = current - lastNotificationTime;

                        if (interval.TotalSeconds < 60)
                        {
                            speed = (int)Math.Floor((this.DownloadedSize -
                                this.lastNotificationDownloadedSize) / interval.TotalSeconds);
                        }

                        lastNotificationTime = current;
                        lastNotificationDownloadedSize = this.DownloadedSize;

                        var downloadProgressChangedEventArgs =
                            new MultiThreadedWebDownloaderProgressChangedEventArgs(
                                DownloadedSize, TotalSize, speed);
                        this.OnDownloadProgressChanged(downloadProgressChangedEventArgs);

                    }

                    // Reset the bufferCount.
                    bufferCount = 0;
                }

            }
        }

        /// <summary>
        /// Handle the ErrorOccurred event of all the HttpDownloadClients.
        /// </summary>
        void client_ErrorOccurred(object sender, ErrorEventArgs e)
        {
            if (this.status != MultiThreadedWebDownloaderStatus.Canceling
                && this.status != MultiThreadedWebDownloaderStatus.Canceled)
            {
                this.Cancel();

                // Raise ErrorOccurred event.
                this.OnError(e);

            }

        }

        /// <summary>
        /// Raise DownloadProgressChanged event. If the status is Completed, then raise
        /// DownloadCompleted event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDownloadProgressChanged(
            MultiThreadedWebDownloaderProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
            {
                DownloadProgressChanged(this, e);
            }
        }

        /// <summary>
        /// Raise StatusChanged event.
        /// </summary>
        protected virtual void OnStatusChanged(EventArgs e)
        {
            if (StatusChanged != null)
            {

                if (this.status == MultiThreadedWebDownloaderStatus.Paused
                    || this.Status == MultiThreadedWebDownloaderStatus.Canceled
                    || this.Status == MultiThreadedWebDownloaderStatus.Completed)
                {
                    // Update the used time when the current download is stopped.
                    usedTime = usedTime.Add(DateTime.Now - lastStartTime);
                }


                StatusChanged(this, e);

                if (this.Status == MultiThreadedWebDownloaderStatus.Completed)
                {
                    MultiThreadedWebDownloaderCompletedEventArgs downloadCompletedEventArgs =
                        new MultiThreadedWebDownloaderCompletedEventArgs
                        (
                            this.DownloadedSize,
                            this.TotalSize,
                            this.TotalUsedTime
                        );

                    this.OnDownloadCompleted(downloadCompletedEventArgs);
                }
            }
        }

        /// <summary>
        /// Raise DownloadCompleted event.
        /// </summary>
        protected virtual void OnDownloadCompleted(
            MultiThreadedWebDownloaderCompletedEventArgs e)
        {
            if (DownloadCompleted != null)
            {
                DownloadCompleted(this, e);
            }
        }

        /// <summary>
        /// Raise ErrorOccurred event.
        /// </summary>
        protected virtual void OnError(ErrorEventArgs e)
        {
            if (ErrorOccurred != null)
            {
                ErrorOccurred(this, e);
            }
        }
    }
}

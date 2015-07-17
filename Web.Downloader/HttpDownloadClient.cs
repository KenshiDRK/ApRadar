/****************************** Module Header ******************************\
* Module Name:  HttpDownloadClient.cs
* Project:	    Web.Downloader
* Copyright (c) Microsoft Corporation.
* 
* This class is used to download files through internet.  It supplies public
* methods to Start, Pause, Resume and Cancel a download. One client will use 
* a single thread to download part of the whole file. The property StartPoint 
* can be used in the multi-thread download scenario, and every thread starts 
* to download a specific block of the whole file. 
* 
* The downloaded data is stored in a MemoryStream first, and then written to local
* file.
* 
* It will fire a DownloadProgressChanged event when it has downloaded a
* specified size of data. It will also fire a DownloadCompleted event if the 
* download is completed or canceled.
* 
* The property DownloadedSize stores the size of downloaded data which will be 
* used to Resume the download.
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
using System.IO;
using System.Net;
using System.Threading;

namespace Web.Downloader
{
    public class HttpDownloadClient
    {
        // Used when creates or writes a file.
        static object locker = new object();

        // The Url of the file to be downloaded.
        public Uri Url { get; private set; }

        // The local path to store the file.
        // If there is no file with the same name, a new file will be created.
        public string DownloadPath { get; private set; }

        // The properties StartPoint and EndPoint can be used in the multi-thread download scenario, and
        // every thread starts to download a specific block of the whole file. 
        public long StartPoint { get; private set; }

        public long EndPoint { get; private set; }

        public WebProxy Proxy { get; set; }

        // Set the BufferSize when read data in Response Stream.
        public int BufferSize { get; private set; }

        // The cache size in memory.
        public int MaxCacheSize { get; private set; }



        // Ask the server for the file size and store it
        public long TotalSize { get; set; }


        // The size of downloaded data that has been written to local file.
        public long DownloadedSize { get; private set; }

        HttpDownloadClientStatus status;

        // If status changed, fire StatusChanged event.
        public HttpDownloadClientStatus Status
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

        public event EventHandler<HttpDownloadClientProgressChangedEventArgs>
            DownloadProgressChanged;

        public event EventHandler<ErrorEventArgs> ErrorOccurred;

        public event EventHandler StatusChanged;

        /// <summary>
        /// Download the whole file.
        /// </summary>
        public HttpDownloadClient(Uri url, string downloadPath)
            : this(url, downloadPath, 0)
        {
        }

        /// <summary>
        /// Download the file from a start point to the end.
        /// </summary>
        public HttpDownloadClient(Uri url, string downloadPath,
           long startPoint)
            : this(url, downloadPath, startPoint, long.MaxValue)
        {
        }

        /// <summary>
        /// Download a block of the file. The default buffer size is 1KB, memory cache is
        /// 1MB, and buffer count per notification is 64.
        /// </summary>
        public HttpDownloadClient(Uri url, string downloadPath,
          long startPoint, long endPoint)
            : this(url, downloadPath, startPoint, endPoint, 1024, 1048576)
        {
        }

        public HttpDownloadClient(Uri url, string downloadPath, long startPoint,
            long endPoint, int bufferSize, int cacheSize)
        {
            if (startPoint < 0)
            {
                throw new ArgumentOutOfRangeException(
                    "StartPoint cannot be less than 0. ");
            }

            if (endPoint < startPoint)
            {
                throw new ArgumentOutOfRangeException(
                    "EndPoint cannot be less than StartPoint ");
            }


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


            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.BufferSize = bufferSize;
            this.MaxCacheSize = cacheSize;

            this.Url = url;
            this.DownloadPath = downloadPath;

            // Set the idle status.
            this.status = HttpDownloadClientStatus.Idle;

        }

        /// <summary>
        /// Start to download.
        /// </summary>
        public void Start()
        {

            // Only idle download client can be started.
            if (this.Status != HttpDownloadClientStatus.Idle)
            {
                throw new ApplicationException("Only idle download client can be started.");
            }

            // Start to download in a background thread.
            BeginDownload();
        }

        /// <summary>
        /// Pause the download.
        /// </summary>
        public void Pause()
        {
            // Only downloading client can be paused.
            if (this.Status != HttpDownloadClientStatus.Downloading)
            {
                throw new ApplicationException("Only downloading client can be paused.");
            }

            // The background thread will check the status. If it is Pausing, the download
            // will be paused and the status will be changed to Paused.
            this.Status = HttpDownloadClientStatus.Pausing;
        }

        /// <summary>
        /// Resume the download.
        /// </summary>
        public void Resume()
        {
            // Only paused client can be resumed.
            if (this.Status != HttpDownloadClientStatus.Paused)
            {
                throw new ApplicationException("Only paused client can be resumed.");
            }

            // Start to download in a background thread.
            BeginDownload();
        }

        /// <summary>
        /// Cancel the download
        /// </summary>
        public void Cancel()
        {
            // Only a downloading or paused client can be canceled.
            if (this.Status != HttpDownloadClientStatus.Paused
                && this.Status != HttpDownloadClientStatus.Downloading)
            {
                throw new ApplicationException("Only a downloading or paused client"
                    + " can be canceled.");
            }

            // The background thread will check the status. If it is Canceling, the download
            // will be canceled and the status will be changed to Canceled.
            this.Status = HttpDownloadClientStatus.Canceling;
        }

        /// <summary>
        /// Create a thread to download data.
        /// </summary>
        void BeginDownload()
        {
            ThreadStart threadStart = new ThreadStart(Download);
            Thread downloadThread = new Thread(threadStart);
            downloadThread.IsBackground = true;
            downloadThread.Start();
        }

        /// <summary>
        /// Download the data using HttpWebRequest. It will read a buffer of bytes from the
        /// response stream, and store the buffer to a MemoryStream cache first.
        /// If the cache is full, or the download is paused, canceled or completed, write
        /// the data in cache to local file.
        /// </summary>
        void Download()
        {
            HttpWebRequest webRequest = null;
            HttpWebResponse webResponse = null;
            Stream responseStream = null;
            MemoryStream downloadCache = null;

            // Set the status.
            this.Status = HttpDownloadClientStatus.Downloading;

            try
            {

                // Create a request to the file to be  downloaded.
                webRequest = (HttpWebRequest)WebRequest.Create(Url);
                webRequest.Method = "GET";
                webRequest.Credentials = CredentialCache.DefaultCredentials;


                // Specify the block to download.
                if (EndPoint != long.MaxValue)
                {
                    webRequest.AddRange(StartPoint + DownloadedSize, EndPoint);
                }
                else
                {
                    webRequest.AddRange(StartPoint + DownloadedSize);
                }

                // Set the proxy.
                if (Proxy != null)
                {
                    webRequest.Proxy = Proxy;
                }

                // Retrieve the response from the server and get the response stream.
                webResponse = (HttpWebResponse)webRequest.GetResponse();

                responseStream = webResponse.GetResponseStream();


                // Cache data in memory.
                downloadCache = new MemoryStream(this.MaxCacheSize);

                byte[] downloadBuffer = new byte[this.BufferSize];

                int bytesSize = 0;
                int cachedSize = 0;

                // Download the file until the download is paused, canceled or completed.
                while (true)
                {

                    // Read a buffer of data from the stream.
                    bytesSize = responseStream.Read(downloadBuffer, 0, downloadBuffer.Length);

                    // If the cache is full, or the download is paused, canceled or 
                    // completed, write the data in cache to local file.
                    if (this.Status != HttpDownloadClientStatus.Downloading
                        || bytesSize == 0
                        || this.MaxCacheSize < cachedSize + bytesSize)
                    {
                        try
                        {
                            // Write the data in cache to local file.
                            WriteCacheToFile(downloadCache, cachedSize);

                            this.DownloadedSize += cachedSize;

                            // Stop downloading the file if the download is paused, 
                            // canceled or completed. 
                            if (this.Status != HttpDownloadClientStatus.Downloading
                                || bytesSize == 0)
                            {
                                break;
                            }

                            // Reset cache.
                            downloadCache.Seek(0, SeekOrigin.Begin);
                            cachedSize = 0;
                        }
                        catch (Exception ex)
                        {
                            // Fire the DownloadCompleted event with the error.
                            this.OnError(new ErrorEventArgs { Error = ex });
                            return;
                        }
                    }

                    // Write the data from the buffer to the cache in memory.
                    downloadCache.Write(downloadBuffer, 0, bytesSize);

                    cachedSize += bytesSize;

                    // Fire the DownloadProgressChanged event.
                    OnDownloadProgressChanged(
                        new HttpDownloadClientProgressChangedEventArgs
                        {
                            Size = bytesSize
                        });
                }

                // Update the status of the client. Above loop will be stopped when the 
                // status of the client is pausing, canceling or completed.
                if (this.Status == HttpDownloadClientStatus.Pausing)
                {
                    this.Status = HttpDownloadClientStatus.Paused;
                }
                else if (this.Status == HttpDownloadClientStatus.Canceling)
                {
                    this.Status = HttpDownloadClientStatus.Canceled;

                    Exception ex = new Exception("Downloading is canceled by user's request. ");

                    this.OnError(new ErrorEventArgs { Error = ex });
                }
                else
                {
                    this.Status = HttpDownloadClientStatus.Completed;
                    return;
                }

            }
            finally
            {
                // When the above code has ended, close the streams.
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (webResponse != null)
                {
                    webResponse.Close();
                }
                if (downloadCache != null)
                {
                    downloadCache.Close();
                }
            }
        }

        /// <summary>
        /// Write the data in cache to local file.
        /// </summary>
        void WriteCacheToFile(MemoryStream downloadCache, int cachedSize)
        {
            // Lock other threads or processes to prevent from writing data to the file.
            lock (locker)
            {
                using (FileStream fileStream = new FileStream(DownloadPath, FileMode.Open))
                {
                    byte[] cacheContent = new byte[cachedSize];
                    downloadCache.Seek(0, SeekOrigin.Begin);
                    downloadCache.Read(cacheContent, 0, cachedSize);
                    fileStream.Seek(DownloadedSize + StartPoint, SeekOrigin.Begin);
                    fileStream.Write(cacheContent, 0, cachedSize);
                }
            }
        }

        /// <summary>
        /// Raise the ErrorOccurred event.
        /// </summary>
        protected virtual void OnError(ErrorEventArgs e)
        {
            if (ErrorOccurred != null)
            {
                ErrorOccurred(this, e);
            }
        }

        /// <summary>
        /// Raise the DownloadProgressChanged event.
        /// </summary>
        protected virtual void OnDownloadProgressChanged(HttpDownloadClientProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
            {
                DownloadProgressChanged(this, e);
            }
        }

        /// <summary>
        /// Raise the StatusChanged event.
        /// </summary>
        protected virtual void OnStatusChanged(EventArgs e)
        {
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, e);
            }
        }
    }
}

/****************************** Module Header ******************************\
* Module Name:  HttpDownloadCompletedEventArgs.cs
* Project:	    Web.Downloader
* Copyright (c) Microsoft Corporation.
* 
* The class MultiThreadedWebDownloaderCompletedEventArgs defines the arguments
* used by the DownloadCompleted event of MultiThreadedWebDownloader.
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

namespace Web.Downloader
{
    public class MultiThreadedWebDownloaderCompletedEventArgs:EventArgs
    {
        public long DownloadedSize { get; private set; }
        public long TotalSize { get; private set; }
        public TimeSpan TotalTime { get; private set; }

        public MultiThreadedWebDownloaderCompletedEventArgs(long downloadedSize, 
            long totalSize,TimeSpan totalTime)
        {
            this.DownloadedSize = downloadedSize;
            this.TotalSize = totalSize;
            this.TotalTime = totalTime;
        }
    }
}

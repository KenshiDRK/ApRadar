/****************************** Module Header ******************************\
* Module Name:  MultiThreadedWebDownloaderProgressChangedEventArgs.cs
* Project:	    Web.Downloader
* Copyright (c) Microsoft Corporation.
* 
* The class MultiThreadedWebDownloaderProgressChangedEventArgs defines the 
* arguments used by the DownloadProgressChanged event of MultiThreadedWebDownloader.
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
    public class MultiThreadedWebDownloaderProgressChangedEventArgs:EventArgs
    {
        public long ReceivedSize { get; private set; }
        public long TotalSize { get; private set; }
        public int DownloadSpeed { get; private set; }

        public MultiThreadedWebDownloaderProgressChangedEventArgs(long receivedSize,
            long totalSize, int downloadSpeed)
        {
            this.ReceivedSize = receivedSize;
            this.TotalSize = totalSize;
            this.DownloadSpeed = downloadSpeed;
        }
    }
}

/****************************** Module Header ******************************\
* Module Name:  HttpDownloadClientProgressChangedEventArgs.cs
* Project:	    Web.Downloader
* Copyright (c) Microsoft Corporation.
* 
* The class HttpDownloadClientProgressChangedEventArgs defines the 
* arguments used by the DownloadProgressChanged event of HttpDownloadClient.
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
    public class HttpDownloadClientProgressChangedEventArgs : EventArgs
    {
        public int Size { get; set; }
    }
}

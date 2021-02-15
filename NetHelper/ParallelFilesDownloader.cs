using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Runtime.InteropServices;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System.Security.Policy;

namespace Boost
{
    public sealed partial class NetHelper
    {
        public class ParallelFileDownloader
        {
            public WebClient[] WebClients;
            private List<UriFileSize> DownloadQueue;

            public bool AutoStart = false;
            public bool OptimizedQueue
            {
                get => _OptimizedQueue;
                set
                {
                    if (!(value == _OptimizedQueue) && value!=false) // Намеренное сравнение bool типа для улучшения читабельности
                    {
                        this.OptimizeQueue();
                    }
                }
            }
            private bool _OptimizedQueue = true;

            public DirectoryInfo TargetDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)).CreateSubdirectory("ProgramDownloads");

            public bool IsBusy
            {
                get => WebClients.Any(wc => wc.IsBusy);
            }

            public int NumOfBusyClients
            {
                get => WebClients.Count(wc => wc.IsBusy);
            }

            public ParallelFileDownloader(int NumOfWebClients)
            {
                WebClients = new WebClient[NumOfWebClients];
            }
            public ParallelFileDownloader(WebClient[]WCs)
            {
                WebClients = WCs;
            }

            public void AddToQueue(Uri FUri)
            {
                var ValueToAdd = new UriFileSize { FileUri = FUri, FileSize = -1 };
                if (!OptimizedQueue) DownloadQueue.Add(ValueToAdd);
                else DownloadQueue.Insert(Binary.InsertionIndex<UriFileSize>(DownloadQueue, ValueToAdd), ValueToAdd);
            }

            public void BeginDownload()
            {
                for (int i = 0; i < WebClients.Length; i++)
                {
                    lock (DownloadQueue)
                    {
                        if (DownloadQueue.Count > 0)
                        {
                            TakeDownload(WebClients[i]);
                        }
                    }
                }
            }

            private void WebClientDownloadCompleteTakeNext(object sender, EventArgs e)
            {
                WebClient SenderWebClient = (WebClient)sender;

                lock (DownloadQueue)
                {
                    if (DownloadQueue.Count > 0)
                    {
                        TakeDownload(SenderWebClient);
                    }
                }
            }

            private void TakeDownload(WebClient wc)
            {
                   // FileInfo NewFileInfo = GenerateFileInfoByUri(TargetDirectory, DownloadQueue.Peek().FileUri);
                   // NewFileInfo.Directory.Create();

                    //wc.DownloadFileAsync(DownloadQueue.Dequeue().FileUri, NewFileInfo.FullName);
            }

            private void OptimizeQueue()
            {
                this.DownloadQueue.Sort();
            }

            private static FileInfo GenerateFileInfoByUri(DirectoryInfo TargetDirectory, Uri FileUri)
                => new FileInfo(TargetDirectory.FullName + '\\' + FileUri.AbsoluteUri.Split('/').Last());

            private static Queue<UriFileSize> TryBuildQueueByFileSize(ICollection<Uri> FileUris)
                => new Queue<UriFileSize>(FileUris.Select(x => new UriFileSize() { FileUri = x, FileSize = TryGetFileSize(x) })
                    .OrderBy(x => x.FileSize));


            public struct UriFileSize :IComparable<UriFileSize>
            {
                public Uri FileUri;
                public long FileSize;

                public int CompareTo(UriFileSize item)
                {
                    return this.FileSize.CompareTo(item.FileSize);
                }

            }

        }
    }
}
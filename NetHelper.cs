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
using Boost;


#pragma warning disable CA1303

namespace Boost
{
	public class NetHelper
	{
		public static void AutorizeWebClientFor2ch(WebClient wc)
		{

		}

		public static HtmlDocument GetPageByChromeDriver(Uri PageUrl) => GetPageByChromeDriver(PageUrl.AbsoluteUri);
		public static HtmlDocument GetPageByChromeDriver(string PageUrl)
		{
			OpenQA.Selenium.Chrome.ChromeDriver ChromeDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
			ChromeDriver.Url = PageUrl;

			HtmlDocument Out = new HtmlDocument();
			try
			{
				Out.Load(
					ChromeDriver.FindElementByXPath(@"//body")
					.GetAttribute(@"outerHTML"));
			}
			catch (System.IO.PathTooLongException ple)
			{
				Out.LoadHtml(new Regex(@"[<]body(.*?)[<][/]body(.*?)[>]", RegexOptions.IgnorePatternWhitespace).Matches(ple.Message).OrderBy(x => x.Length).First().Value);
			}

			return Out;
		}

		public static long GetFileSizeByFtp(Uri FileUri)
		{
			WebRequest FtpReq = FtpWebRequest.Create(FileUri);
			FtpReq.Method = WebRequestMethods.Ftp.GetFileSize;
			return FtpReq.GetResponse().ContentLength;
		}

		/// <summary>
		/// Non-primary method to get FileSize on remote server by Uri.
		/// Use if Ftp is not allowed (405) or in another supossed cases.
		/// </summary>
		public static long GetFileSizeByHttp(Uri FileUri)
		{
			WebRequest HttpReq = HttpWebRequest.Create(FileUri);
			HttpReq.Method = WebRequestMethods.Http.Get;
			return HttpReq.GetResponse().ContentLength;
		}

		public static long TryGetFileSize(Uri FileUri)
		{
			try
			{
				return GetFileSizeByFtp(FileUri);
			}
			catch (WebException we)
			{
				try
				{
					return GetFileSizeByHttp(FileUri);
				}
				catch (WebException we1)
				{
					return -1;
				}
			}
		}

		public class ParallelFileDownloader
		{
			public WebClient[] WebClients;
			public Queue<UriFileSize> DownloadQueue;
			public DirectoryInfo TargetDirectory;



			public bool IsBusy
			{
				get => WebClients.Any(wc => wc.IsBusy);
			}

			public int NowFilesDownloading = 0;


			public ParallelFileDownloader(ICollection<Uri> FileUris, DirectoryInfo DownloadDirectory, WebClient[] WCs = null)
			{
				this.DownloadQueue = TryBuildQueueByFileSize(FileUris);
				this.TargetDirectory = DownloadDirectory;
				this.WebClients = WCs == null ? new WebClient[3] : WCs;
			}


			public void BeginDownloadAsync()
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

			public void BeginDownloadSync()
			{
				this.BeginDownloadAsync();

				while (this.IsBusy && DownloadQueue.Count > 0) Thread.Sleep(10);
			}

			private void WebClientDownloadCompleteTakeNext(object sender, EventArgs e)
			{
				NowFilesDownloading--;

				WebClient ThisWebClient = (WebClient)sender;

				lock (DownloadQueue)
				{
					if (DownloadQueue.Count > 0)
					{
						TakeDownload(ThisWebClient);
						NowFilesDownloading++;
					}
				}
			}

			private void TakeDownload(WebClient wc)
			{
				if (DownloadQueue.Count > 0)
				{
					FileInfo NewFileInfo = GenerateFileInfoByUri(TargetDirectory, DownloadQueue.Peek().FileUri);
					NewFileInfo.Directory.Create();

					wc.DownloadFileAsync(DownloadQueue.Dequeue().FileUri, NewFileInfo.FullName);

					NowFilesDownloading++;
				}
			}

			private static FileInfo GenerateFileInfoByUri(DirectoryInfo TargetDirectory, Uri FileUri)
				=> new FileInfo(TargetDirectory.FullName + '\\' + FileUri.AbsoluteUri.Split('/').Last());

			private static Queue<UriFileSize> TryBuildQueueByFileSize(ICollection<Uri> FileUris)
				=> new Queue<UriFileSize>(FileUris.Select(x => new UriFileSize() { FileUri = x, FileSize = TryGetFileSize(x) })
					.OrderBy(x => x.FileSize));


			public struct UriFileSize
			{
				public Uri FileUri;
				public long FileSize;
			}

		}
	}
}

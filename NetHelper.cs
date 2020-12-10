using System;
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

			HtmlDocument Out= new HtmlDocument();
			try
			{
				Out.Load(
					ChromeDriver.FindElementByXPath(@"//body")
					.GetAttribute(@"outerHTML"));
			}
			catch(System.IO.PathTooLongException ple)
			{
				Out.LoadHtml(new Regex(@"[<]body(.*?)[<][/]body(.*?)[>]", RegexOptions.IgnorePatternWhitespace).Matches(ple.Message).OrderBy(x=>x.Length).First().Value);
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
			WebRequest FileSizeReq;
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
			public Queue<Uri> DownloadQuery;
			public DirectoryInfo TargetDirectory;
			public bool IsBusy
			{
				get => WebClients.Any(wc => wc.IsBusy);
			}
			public int NowFilesDownloading = 0;


			public ParallelFileDownloader(ICollection<Uri> FileUris, DirectoryInfo DownloadDirectory, bool OptimizeQueueByFileSize = true, int NumOfWebClients = 3)
			{
				DownloadQuery = OptimizeQueueByFileSize ? TryBuildQueryByFileSize(FileUris) : new Queue<Uri>(FileUris);
				this.TargetDirectory = DownloadDirectory;

				this.WebClients = new WebClient[NumOfWebClients];
				for (int i = 0; i < WebClients.Length; i++)
				{
					WebClients[i] = new WebClient();
					WebClients[i].DownloadFileCompleted += WebClientDownloadCompleteTakeNext;
				}
			}

			public ParallelFileDownloader(ICollection<Uri> FileUris, DirectoryInfo DownloadDirectory, WebClient[] WCs, bool OptimizeQueueByFileSize = true)
			{

				DownloadQuery = OptimizeQueueByFileSize ? TryBuildQueryByFileSize(FileUris) : new Queue<Uri>(FileUris);
				this.TargetDirectory = DownloadDirectory;
				this.WebClients = WCs;

			}


			public void BeginDownloadAsync()
			{
				for (int i = 0; i < WebClients.Length; i++)
				{
					lock (DownloadQuery)
					{
						if (DownloadQuery.Count > 0)
						{
							TakeDownload(WebClients[i]);
						}
					}
				}
			}
			public  void BeginDownloadSync()
			{
				this.BeginDownloadAsync();

				while (this.IsBusy && DownloadQuery.Count>0) Thread.Sleep(10);
			}

			private void WebClientDownloadCompleteTakeNext(object sender, EventArgs e)
			{
				NowFilesDownloading--;

				WebClient ThisWebClient = (WebClient)sender;

				lock (DownloadQuery)
				{
					if (DownloadQuery.Count > 0)
					{
						TakeDownload(ThisWebClient);
						NowFilesDownloading++;
					}
				}
			}

			private void TakeDownload(WebClient wc)
			{
				if (DownloadQuery.Count > 0)
				{
					FileInfo NewFileInfo = GenerateFileInfoByUri(TargetDirectory, DownloadQuery.Peek());
					NewFileInfo.Directory.Create();

					wc.DownloadFileAsync(DownloadQuery.Dequeue(), NewFileInfo.FullName);

					NowFilesDownloading++;
				}
			}

			private static FileInfo GenerateFileInfoByUri(DirectoryInfo TargetDirectory, Uri FileUri)
				=> new FileInfo(TargetDirectory.FullName + '\\' + FileUri.AbsoluteUri.Split('/').Last());

			private static Queue<Uri> TryBuildQueryByFileSize(ICollection<Uri> FileUris)
				=> new Queue<Uri>(FileUris.OrderBy(file => TryGetFileSize(file)));

		}
	}
}

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


#pragma warning disable CA1303

namespace Boost
{
	public sealed partial class NetHelper
	{
		/*
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
		

		public static string GetRequestCookies(Uri SiteUri)
		{
			ChromeDriver ChrDrv = new ChromeDriver{Url = SiteUri.AbsoluteUri};
			_ =ChrDrv.Manage().Timeouts().ImplicitWait;
			var Out= ChrDrv.Manage().Cookies.AllCookies.Select(x => x.Name + ':' + x.Value + ';').Aggregate((x,y)=>x+y);
			ChrDrv.Close(); return Out;
		}

		public static string GetPageByOpening(Uri PageUri) =>
			(new ChromeDriver {Url = PageUri.AbsoluteUri}).FindElementByTagName("body").Text;

		public static void OpenInChrome(Uri PageUri) => new ChromeDriver {Url = PageUri.AbsoluteUri};

		*/

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


		public static long GetFileSize(Uri FileUri)
		{
			Trace.WriteLine("Trying to get file size of " + FileUri.AbsoluteUri);
			try
			{
				return GetFileSizeByFtp(FileUri);
			}
			catch (WebException)
			{
				return GetFileSizeByHttp(FileUri);
			}
		}

		public static long TryGetFileSize(Uri FileUri)
		{
			Trace.WriteLine("Trying to get file size of "+ FileUri.AbsoluteUri);
			try
			{
				return GetFileSizeByFtp(FileUri);
			}
			catch (WebException)
			{
				try
				{
					return GetFileSizeByHttp(FileUri);
				}
				catch (WebException)
				{
					return -1;
				}
			}
		}

		
	}
}

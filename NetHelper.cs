using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CA1303

namespace Boost
{
	public class NetHelper
	{
		public static string GetElementByParams(string HtmlString,string Tag, string Class,string Id)
		{


			return "";
		}
		public static string FindHtmlElementEnd(string HtmlString, string Tag, int IndexOfTagOpeningChar)
		{
			if (HtmlString == null || Tag == null)
				throw new ArgumentException("Null string are not allowed as parameters for this function");
			if (HtmlString.Length - 1 < IndexOfTagOpeningChar + Tag.Length)
				throw new ArgumentException("Unacceptable HtmlString length");
			if (HtmlString[IndexOfTagOpeningChar] != '<')
				throw new ArgumentException("No < char found on selected index");
			if (HtmlString.Substring(IndexOfTagOpeningChar + 1, Tag.Length) != Tag)
				throw new ArgumentException("No selected tag found on selected index");

			int OpenCloseCounter = 0;

			for(int i = IndexOfTagOpeningChar; i < HtmlString.Length; i++)
			{

			}

			return "";
		}

	}
}

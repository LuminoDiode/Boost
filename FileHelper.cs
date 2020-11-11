using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using Shell32;

namespace Boost
{
	public class FileHelper
	{
		static Shell shell = new Shell();
		[STAThread]
		public static string EnsureNotShortcut(string LinkPath) =>
			((ShellLinkObject)( // creating an instance of LinkObject
				shell.NameSpace(LinkPath.Substring(0, LinkPath.LastIndexOf("\\"))). // Getting LinkObject folder
				Items().Item(LinkPath.Split('\\').Last()).GetLink // getting LinkObject as file
				)).Path; // getting original file path 
		

		/*
		 public static string EnsureNotShortcut(string LinkPath)
		{
			// Get link file as FolderItem
			var folder = shell.NameSpace(LinkPath.Substring(0, LinkPath.LastIndexOf("\\"))); // Getting link folder
			var LinkFile = folder.Items().Item(LinkPath.Split('\\').Last());
			//
			if (!LinkFile.IsLink)
			{
				return LinkFile.Path;
			}
			else
			{
				return ((Shell32.ShellLinkObject)(LinkFile.GetLink)).Path;
			}
		}
		 */
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using Shell32;

namespace Boost
{
	public static class FileHelper
	{
		static Shell shell = new Shell();

		[STAThread]
		public static string EnsureNotShortcut(string LinkPath)
		{
			FolderItem ShortcutFile = null;
			try
			{
				ShortcutFile = shell.NameSpace(LinkPath.Substring(0, LinkPath.LastIndexOf('\\'))).Items().Item(LinkPath.Split('\\').Last());
				if (!ShortcutFile.IsLink) return LinkPath;
			}
			catch
			{
				Trace.WriteLine("Unexpected error for " + LinkPath);
				return LinkPath;
			}

			ShellLinkObject ShortcutFileLinkObject;
			try
			{
				ShortcutFileLinkObject = (ShellLinkObject)(ShortcutFile);
			}
			catch (System.InvalidCastException e)
			{
				Trace.WriteLine($"Error while try casting Shortcut file {LinkPath} to ShellLinkObject: " + e.Message +"; Shortcut path will be returned.");
				return LinkPath;
			}

			return ShortcutFileLinkObject.Path;
		}
		

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

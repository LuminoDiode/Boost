using System;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Boost
{
	public static class Program
	{
		[DllImport("kernel32.dll")]
		static extern bool AllocConsole();
		public static void Main()
		{
			AllocConsole();
			double[,]initmtr = {{1,2,3},{1,2,3}};
			NumericMatrix mtr = initmtr;
			Console.Write(mtr.Size.ToString());
			Console.ReadKey();
		}
	}
}

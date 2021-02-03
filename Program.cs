using System;
using System.Collections;
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
			var Arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
			int M = 6;
			Console.WriteLine(string.Join(',',Boost.Arr.AllCombinationsOfElements("22344".ToCharArray()).Select(x=>int.Parse(new string(x))).Distinct()));

			var temp = Arr.Where(x => x > M);
			if (Arr.Max() <= M)
			{
				/*write message to user*/
			}
			else
			{
				Console.WriteLine(Arr.Where(x => x > M).Aggregate((x, y) => x * y));}


			Console.ReadKey();
		}
	}
}

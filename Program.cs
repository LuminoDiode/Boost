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
			Random r = new();
			var arr = new double[53].Select(x => r.NextDouble()).ToArray();
			foreach (var i in arr) Console.WriteLine(i + " ");
			var EvenIndexEls = new double[(int)((float)arr.Length / 2+0.5)];
			var NotEvenIndexEls = new double[arr.Length / 2 ==0? arr.Length / 2 : arr.Length / 2 +1];
			for (var i = 0; i < arr.Length; i++)
			{
				if (i % 2 == 0) EvenIndexEls[i / 2] = arr[i];
				else EvenIndexEls[i / 2] = arr[i];
			}

			Array.Sort(EvenIndexEls); Array.Sort(NotEvenIndexEls);
			for(int i = 0; i < arr.Length; i++)
			{
				arr[i]= i%2==0? EvenIndexEls[i/2] : NotEvenIndexEls[i/2];
			}
			foreach (var i in arr) Console.WriteLine(i + " ");
			Console.ReadLine();
		}
	}
}

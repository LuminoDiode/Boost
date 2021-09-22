using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Boost
{
	public static class Program
	{
		[DllImport("kernel32.dll")]
		static extern bool AllocConsole();
		public static void Main()
		{
			Random r = new Random();

			var point1 = DateTime.Now.Ticks;
			Func1();
			var point2 = DateTime.Now.Ticks;

			Console.WriteLine(point2 - point1);
			point1 = DateTime.Now.Ticks;
			Func1();
			point2 = DateTime.Now.Ticks;

			Console.WriteLine(point2 - point1);
			point1 = DateTime.Now.Ticks;
			Func1();
			point2 = DateTime.Now.Ticks;

			Console.WriteLine(point2 - point1);

			point1 = DateTime.Now.Ticks;
			Func2();
			point2 = DateTime.Now.Ticks;

			Console.WriteLine(point2 - point1);
			point1 = DateTime.Now.Ticks;
			Func2();
			point2 = DateTime.Now.Ticks;

			Console.WriteLine(point2 - point1);
			point1 = DateTime.Now.Ticks;
			Func2();
			point2 = DateTime.Now.Ticks;

			Console.WriteLine(point2 - point1);
			point1 = DateTime.Now.Ticks;
			Func2();
			point2 = DateTime.Now.Ticks;

			Console.WriteLine(point2 - point1);
		}


		public static void Func1()
		{
			NumericMatrix m1 = new double[][]
			{
				new double[]{1,2,3},
				new double[]{4,5,6},
				new double[]{7,8,9}
			};
			for (int i = 0; i < 1000; i++) m1 = m1 * m1;
			Console.WriteLine(m1.ToString());
		}

		public static void Func2()
		{
			NumericMatrix m1 = new double[][]
			{
				new double[]{1,2,3},
				new double[]{4,5,6},
				new double[]{7,8,9}
			};
			NumericMatrix m2 = m1.Clone();

			Parallel.Invoke(
			() =>
			{
				for (int i = 0; i < 500; i++) m2 = m2 * m2;
			},
			() =>
			{
				for (int i = 0; i < 499; i++) m1 = m1 * m1;
			});

			Console.WriteLine((m1*m2).ToString());
		}
	}
}

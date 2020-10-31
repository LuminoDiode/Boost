using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Text;
using System.Security.Cryptography;

namespace Boost
{
	public static class Gen
	{
		static Random Rnd = new Random();

		public static int NotLessThanZero(int val) => val > 0 ? val : 0;

		public static T Min<T>(params T[] Vals) where T : IComparable
			=> Vals.Min();
		public static T Max<T>(params T[] Vals) where T : IComparable
			=> Vals.Max();
		public static double Mid(params dynamic[] Vals)
		{
			double Sum = 0; for (int i = 0; i < Vals.Length; i++) Sum += Vals[i];
			return Sum / Vals.Length;
		}
		public static int Mid(params int[] Vals)
		{
			int Sum = 0; for (int i = 0; i < Vals.Length; i++) Sum += Vals[i];
			return Sum / Vals.Length;
		}

		public static void Swap<T>(ref T Var1, ref T Var2)
		{
			T temp = Var1;
			Var1 = Var2;
			Var2 = temp;
		}

		public static int RanInt(int Min, int Max)
		{
			return Rnd.Next(Min, Max);
		}
		public static double RanDouble(double Min, double Max)
		{
			return Min + (Max - Min) * Rnd.NextDouble();
		}

		public static bool IsNumeric<T>()
		{
			return
				typeof(T) == typeof(byte) ||
				typeof(T) == typeof(sbyte) ||
				 typeof(T) == typeof(short) ||
				typeof(T) == typeof(ushort) ||
				typeof(T) == typeof(int) ||
				typeof(T) == typeof(uint) ||
				typeof(T) == typeof(long) ||
				typeof(T) == typeof(ulong) ||
				typeof(T) == typeof(float) ||
				typeof(T) == typeof(double) ||
				typeof(T) == typeof(BigInteger);
		}
		public static FileInfo SelectFileDialog(string InitialDirectory = null)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			if (InitialDirectory != null && Directory.Exists(InitialDirectory)) dlg.InitialDirectory = InitialDirectory;
			dlg.ShowDialog();
			return new FileInfo(dlg.FileName);
		}


		public static void AddLocationToEachControl(Size AdditionLocation,params Control[]Controls)
		{
			for(int i = 0; i < Controls.Length; i++)
			{
				Controls[i].Location = Point.Add(Controls[i].Location, AdditionLocation);
			}
		}
		public static void AddLocationToEachControl(Point AdditionLocation, params Control[] Controls)
		{
			AddLocationToEachControl(new Size(AdditionLocation), Controls);
		}
		public static Size Invert(Size s)
		{
			return new Size(-1 * s.Width, -1 * s.Height);
		}
		/*
		public static Size Invert(Size s)
		{
			return new Size(-1 * s.Width, -1 * s.Height);
		}
		*/
		public static int IntParseAny(string s)
		{
			int m;
			if (int.TryParse(s, out m)) return m;
			return 0;
		}
	}
}
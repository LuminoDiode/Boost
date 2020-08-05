using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace Boost
{
	class Program
	{

		static Random rnd = new Random();

		static void Main(string[] args)
		{
			double[][]Mat=
			{
				new double[]{1,2,3},
				new double[]{4,5,6},
				new double[]{7,8,9}
			};
			double Min=30, Max=20;
			for(long i = 0; i < 100000000; i++)
			{
				var temp = Gen.RanDouble(20, 30);
				if (temp > Max) Max = temp;
				if (temp < Min) Min = temp;
				if (!(temp <= 30 && temp >= 20)) Console.ReadKey();
			}
			Console.WriteLine("Min = " + Min);
			Console.WriteLine("Max = " + Max);
			Console.WriteLine();
			var m1 = Matrix.Clone(Mat);
			var m2 = Matrix.RotateRight(Mat);
			Console.WriteLine(Matrix.ToString(Matrix.GetMinor(m1,1,1)));
			Console.WriteLine(foo<BigInteger>());
		}

		public static bool foo<T>()
		{
			return typeof(T) == typeof(BigInteger);
		}
	}
}

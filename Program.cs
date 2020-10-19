using System;
using System.Linq;
using System.Numerics;

namespace Boost
{
	public class Program
	{

		static Random rnd = new Random();

		public static void Main(string[] args)
		{
			/*
			double[][] Mat =
			{
				new double[]{1,1,1,1},
				new double[]{0,0,-2,-2},
				new double[]{0,-2,0,-2},
				new double[]{0,0,-2,2}
			};
			Console.WriteLine("Matrix : ");
			Console.WriteLine(Matrix.ToString(Mat));
				Console.WriteLine();
			Console.WriteLine("Determinator : ");
			Console.WriteLine(Matrix.GetDet(Mat));
				Console.WriteLine();
			Console.WriteLine("Transposed : ");
			Console.WriteLine(Matrix.ToString(Matrix.Transpose(Mat)));
				Console.WriteLine();
			Console.WriteLine("Inverted : ");
			Console.WriteLine(Matrix.ToString(Matrix.Invert(Mat)));
		*/

			int count = 0;
			for(int i = 0; i < 10000000000; i++)
			{
				if (i.ToString().Length == 5)
				{
					if(i.ToString().All(x=> (new char[] {'1','2','3','5' }).Contains(x)))
					{
						if(i%4==0)
							Console.WriteLine((++count).ToString() + ". " + i.ToString());
					}
				}
			}

			Console.ReadKey();
		}

		public static bool foo<T>()
		{
			return typeof(T) == typeof(BigInteger);
		}
	}
}

using System;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace Boost
{
	public class Program
	{

		static Random rnd = new Random();

		public static void Main(string[] args)
		{

		}

		public static bool foo<T>()
		{
			return typeof(T) == typeof(BigInteger);
		}

		public static void Swap<T>(ref T val1,ref T val2)
		{
			T temp = val1;
			val1 = val2;
			val2 = temp;
		}
	}
}

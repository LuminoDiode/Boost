using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

static class Gen
{
	static Random Rnd = new Random();

	public static T Min<T>(params T[] Vals) where T : IComparable
		=> Vals.Min();
	public static T Max<T>(params T[] Vals) where T : IComparable
		=> Vals.Max();
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
}

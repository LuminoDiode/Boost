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
		public static int[] GenRandArr(int len)
		{
			Random rnd = new Random();
			int[] result = new int[len];
			for (int i = 0; i < len; i++) result[i] = rnd.Next(0, byte.MaxValue);
			return result;
		}

		public static void Main()
		{
			int[][] TestMtr = new int[][]
			{
				new int[] { 1, 2, 3 },
				new int[] { 1, 2, 3 },
				new int[] { 1, 2, 3 },
			};
			Console.WriteLine(Matrix.GetMainDiag(TestMtr).Sum());
		}
	}

	static class QSort
	{
		/// <summary>
		/// Быстрая сортировка.
		/// </summary>
		/// <param name="Arr"></param>
		/// <returns></returns>
		public static T[] Sort<T>(T[] Arr) where T : IComparable //Debugged
		{
			if (Arr.Length < 2) return Arr;
			T[] LeftArr; T[] RightArr; T BaseEl;
			SplitArr(Arr, out LeftArr, out RightArr, out BaseEl);
			return CombineArrs(Sort(LeftArr), Sort(RightArr), BaseEl);
		}
		private static void SplitArr<T>(T[] Arr, out T[] LeftArr, out T[] RightArr, out T BaseEl) where T : IComparable
		{
			List<T> LeftArrO = new List<T>();
			List<T> RightArrO = new List<T>();
			int BaseIndex = Arr.Length / 2, i;

			for (i = 0; i < BaseIndex; i++)
			{
				if (Comparer<T>.Default.Compare(Arr[i], Arr[BaseIndex]) < 0)
					LeftArrO.Add(Arr[i]);
				else
					RightArrO.Add(Arr[i]);
			}
			for (i = BaseIndex + 1; i < Arr.Length; i++)
			{
				if (Comparer<T>.Default.Compare(Arr[i], Arr[BaseIndex]) < 0)
					LeftArrO.Add(Arr[i]);
				else
					RightArrO.Add(Arr[i]);
			}

			LeftArr = LeftArrO.ToArray();
			RightArr = RightArrO.ToArray();
			BaseEl = Arr[BaseIndex];
		}
		private static T[] CombineArrs<T>(T[] LeftArr, T[] RightArr, T BaseEl) //Debugged
		{
			T[] Out = new T[LeftArr.Length + RightArr.Length + 1];
			int i;

			for (i = 0; i < LeftArr.Length; i++) Out[i] = LeftArr[i];
			Out[LeftArr.Length] = BaseEl;
			for (i = LeftArr.Length + 1; i < Out.Length; i++) Out[i] = RightArr[i - (LeftArr.Length + 1)];

			return Out;
		}
	}
}
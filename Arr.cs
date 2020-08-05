﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boost
{
	partial class Arr
	{
		private static Random Rnd = new Random();

		//complete f
		public static T[] Reversed<T>(T[] Arr1)
		{
			T[] Out = new T[Arr1.Length];

			for (int i = 0; i < Arr1.Length; i++)
				Out[i] = Arr1[Arr1.Length - 1 - i];

			return Out;
		}
		//complete f
		public static void Reverse<T>(T[] Arr1)
		{
			for (int i = 0; i < Arr1.Length / 2; i++) 
				Gen.Swap(ref Arr1[i], ref Arr1[Arr1.Length - 1 - i]);
		}

		//complete f
		public static void Copy<T>(T[] CopyFrom, T[] CopyTo)
		{
			for (int i = 0; i < CopyTo.Length && i < CopyFrom.Length; i++) CopyTo[i] = CopyFrom[i];
		}

		//complete f
		public static T[] Clone<T>(T[] Arr1)
		{
			T[] Out = new T[Arr1.Length];
			for (int i = 0; i < Arr1.Length; i++) Out[i] = Arr1[i];
			return Out;
		}

		//complete f
		public static double[] ToDouble<T>(T[] Arr1)
		{
			double[] Out = new double[Arr1.Length];
			for (int i = 0; i < Arr1.Length; i++) Out[i] = (double)(Arr1[i] as dynamic);
			return Out;
		}

		public static string ToString<T>(T[] Arr)
		{
			string Out = string.Empty;
			for (int i = 0; i < Arr.Length; i++) Out += Arr[i].ToString() + " ";
			return Out;
		}

		public static int[] Random(int Len, int Min, int Max)
		{
			int[] Out = new int[Len];
			for (int i = 0; i < Out.Length; i++) Out[i] = Gen.RanInt(Min, Max);
			return Out;
		}
		public static double[] Random(int Len, double Min, double Max)
		{
			double[] Out = new double[Len];
			for (int i = 0; i < Out.Length; i++) Out[i] = Gen.RanDouble(Min, Max);
			return Out;
		}

		public static void Insert<T>(ref T[] Arr, T Val, int Id)
		{
			T[] Out = new T[Arr.Length + 1]; int i;

			for (i = 0; i < Id; i++) Out[i] = Arr[i];
			Out[Id] = Val;
			for (i = Id + 1; i < Out.Length; i++) Out[i] = Arr[i - 1];

			Arr = Out;
		}
		public static T[] Inserted<T>(T[] Arr, T Val, int Id)
		{
			T[] Out = new T[Arr.Length + 1]; int i;

			for (i = 0; i < Id; i++) Out[i] = Arr[i];
			Out[Id] = Val;
			for (i = Id + 1; i < Out.Length; i++) Out[i] = Arr[i - 1];

			return Out;
		}
		public static void BinaryInsert<T>(ref T[] SortedArr, T Obj) where T : IComparable
		{
			Insert(ref SortedArr, Obj, Binary.InsertionIndex(SortedArr, Obj));
		}
		public static T[] BinaryInserted<T>(T[] SortedArr, T Obj) where T : IComparable
		{
			return Inserted(SortedArr, Obj, Binary.InsertionIndex(SortedArr, Obj));
		}

	}
}
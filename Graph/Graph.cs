using System;

namespace Boost
{
	partial class Graph
	{
		private static Random Rnd = new Random();
		public const int NO_WAY = int.MinValue;

		public static int[][] RandomScheme(int VertexNum = 5, int MinDist = 2, int MaxDist = 10)
		{
			int[][] Out = Matrix.Generate<int>(VertexNum, VertexNum);

			for (int i1 = 0; i1 < VertexNum; i1++)
				for (int i2 = i1; i2 < VertexNum; i2++)
					if (i1 != i2)
						Out[i1][i2] = Out[i2][i1] = Rnd.Next(MinDist, MaxDist);

			return Out;
		}

		public static int GetShortestWay(
			int[][] Scheme,
			int FromCt,
			int ToCt,
			int OnGoing = 0,
			bool[] Visited = null)
		{
			if (Visited == null) Visited = new bool[Scheme.GetLength(0)];

			int Current, MinDist = int.MaxValue;

			for (int NowTo = 0; NowTo < Scheme.Length; NowTo++)
			{
				if (!Visited[NowTo] && NowTo != FromCt && Scheme[FromCt][NowTo] != NO_WAY)
				{
					if (NowTo == ToCt)
					{
						Current = OnGoing + Scheme[FromCt][NowTo];
					}
					else
					{
						Visited[NowTo] = true;
						Current = GetShortestWay(Scheme, NowTo, ToCt, OnGoing + Scheme[FromCt][NowTo], Visited);
						Visited[NowTo] = false;
					}

					if (Current < MinDist) MinDist = Current;
				}
			}

			return MinDist;
		}

		public static void PrintScheme(int[][] Scheme)
		{
			for (int i1 = 0; i1 < Scheme.Length; i1++)
			{
				for (int i2 = 0; i2 < Scheme[i1].Length; i2++)
				{
					if (Scheme[i1][i2] == NO_WAY) Console.Write("N" + " ");
					else Console.Write(Scheme[i1][i2] + " ");
				}
				Console.WriteLine();
			}
		}
	}
}
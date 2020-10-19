using System;
using System.Collections.Generic;
using System.Drawing;

namespace Boost
{
	public static partial class Matrix
	{
		public static bool FoolProof = true;

		public static class Exceptions
		{
			public static readonly ArgumentException NotNumeric = new
				ArgumentException("Данная операция допустима только для матриц числового типа.");
			public static readonly FormatException NotMatrix = new
				FormatException("Передан jagged array, хотя ожидалась матрица.");
			public static readonly ArgumentException NotMultable = new
				ArgumentException("Умножение переданных матриц невозможно. Количество строк первой матрицы должно быть равно количеству столбцов второй.");
			public static readonly ArgumentException NotSumable = new
				ArgumentException("Суммирование данных матриц невозможно. Размеры суммируемых матриц должнs быть идентичны.");
			public static readonly AggregateException ZeroDeterminator = new
				AggregateException("Определитель матрицы равен нулю. Инвертирование невозможно.");
			public static readonly ArgumentException NotSquare = new
				ArgumentException("Данная операция допустима только для квадратных матриц.");
			public static readonly ArgumentException Wrong_row_len = new
				ArgumentException("Переданная строка не соотвествует длинам строки данной матрицы.");
		}



		//complete
		public static string ToString<T>(T[][] Mtr)
		{
			var Out = new List<char>(); int i1, i2;

			for (i1 = 0; i1 < Mtr.Length; i1++)
			{
				for (i2 = 0; i2 < Mtr[i1].Length; i2++)
				{
					Out.AddRange(Mtr[i1][i2].ToString().ToCharArray());
					Out.Add(' ');
				}
				Out.Add('\n');
			}

			return new string(Out.ToArray());
		}

		//complete f
		public static T[][] Clone<T>(T[][] Arr1)
		{
			T[][] Out = new T[Arr1.Length][];
			for (int i = 0; i < Arr1.Length; i++) Out[i] = Arr.Clone(Arr1[i]);
			return Out;
		}
		//complete f
		public static T[][] Generate<T>(int Len1, int Len2)
		{
			T[][] Out = new T[Len1][];
			for (int i = 0; i < Len1; i++) Out[i] = new T[Len2];
			return Out;
		}

		//complete f
		private static double[][] ToDouble<T>(T[][] Mtr)
		{
			if (FoolProof)
			{
				if (!Gen.IsNumeric<T>()) throw Exceptions.NotNumeric;
			}

			double[][] Out = new double[Mtr.Length][];
			for (int i = 0; i < Out.Length; i++) Out[i] = Arr.ToDouble(Mtr[i]);
			return Out;
		}

		//complete f
		private static bool IsMatrix<T>(params T[][][] Mtrs)
		{
			int i1, i2;

			for (i1 = 0; i1 < Mtrs.Length; i1++) //switch martices
				for (i2 = 1; i2 < Mtrs[i1].Length; i2++) //switch rows
					if (Mtrs[i1][i2].Length != Mtrs[i1][0].Length) //compare to other rows
						return false; //false if not equal length

			return true;
		}
		//complete f
		private static bool IsSquare<T>(params T[][][] Mtrs)
		{
			for (int i = 0; i < Mtrs.Length; i++)
				if (Mtrs[i].Length != Mtrs[i][0].Length)
					return false;

			return true;
		}
		//complete f
		private static bool IsMultAble<T>(T[][] Mtr1, T[][] Mtr2, params T[][][] Mtrs)
		{
			if (Mtr1.Length != Mtr2[0].Length) return false;
			if (Mtrs.Length == 0) return true;

			int NewSide = Gen.Min(Mtr1.Length, Mtr1[0].Length);

			for (int i = 0; i < Mtrs.Length; i++)
				if (!IsSquare(Mtrs[i]) || Mtrs[i].Length != NewSide)
					return false;

			return true;
		}
		//complete f
		private static bool IsSumAble<T>(T[][] Mtr1, T[][] Mtr2, params T[][][] Mtrs)
		{
			if (Mtr1.Length == Mtr2.Length && Mtr1[0].Length == Mtr2[0].Length)
				return false;

			for (int i = 1; i < Mtrs.Length; i++)
				if (Mtrs[0].Length != Mtrs[i].Length || Mtrs[0][0].Length != Mtrs[i][0].Length)
					return false;

			return true;
		}

		
		public static T[] GetRow<T>(T[][] Mtr, int index)
		{
			return Arr.Clone(Mtr[index]);
		}
		public static T[] GetCol<T>(T[][] Mtr, int index)
		{
			T[] NewArr = new T[Mtr.Length];
			for (int i = 0; i < NewArr.Length; i++) NewArr[i] = Mtr[i][index];
			return NewArr;
		}

		public static void InsertRow<T>(ref T[][] Mtr, T[] NewRow, int index)
		{
			for (int i = 0; i < NewRow.Length; i++)
				Mtr[index][i] = NewRow[i];
		}
		public static T[][] InsertRow<T>(T[][] Mtr, T[] NewRow, int index)
		{
			Mtr = Clone(Mtr);
			InsertRow(ref Mtr, NewRow, index);
			return Mtr;
		}
		public static void InsertCol<T>(ref T[][] Mtr, T[] NewCol, int index)
		{
			for (int i = 0; i < Mtr.Length; i++)
				Mtr[i][index] = NewCol[i];
		}
		public static T[][] InsertCol<T>(T[][] Mtr, T[] NewCol, int index)
		{
			Mtr = Clone(Mtr);
			InsertCol(ref Mtr, NewCol, index);
			return Mtr;
		}

		public static void RotateRight<T>(ref T[][] Mtr)
		{
			if (FoolProof)
			{
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			}

			T[][] Out = Generate<T>(Mtr[0].Length, Mtr.Length);

			for (int i = 0; i < Mtr.Length; i++)
				InsertCol(ref Out, GetRow(Mtr, i), Out[0].Length - 1 - i);

			Mtr = Out;
		}
		//complete f
		public static T[][] RotateRight<T>(T[][] Mtr)
		{
			if (FoolProof)
			{
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			}

			Mtr = Clone(Mtr);
			RotateRight(ref Mtr);
			return Mtr;
		}

		
		public static T[][] Transpose<T>(T[][] Mtr)
		{
			if (FoolProof)
			{
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			}

			T[][] Out = new T[Mtr[0].Length][];
			for (int i = 0; i < Out.Length; i++)
			{
				Out[i] = new T[Mtr.Length];
				InsertRow(ref Out, GetCol(Mtr, i), i);
			}
			return Out;
		}
		public static void Transpose<T>(ref T[][] Mtr)
		{
			Mtr = Transpose(Clone(Mtr));
		}

		//complete f
		public static T[] GetMainDiag<T>(T[][] Mtr)
		{
			if (FoolProof)
			{
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			}

			T[] Out;

			if (IsSquare(Mtr))
			{
				Out = new T[Mtr.Length];

				for (int i = Mtr[0].Length; i >= 0; i--)
					Out[i] = Mtr[i][i];
			}
			else if (Mtr.Length > Mtr[0].Length) // more rows = switch rows
			{
				Out = new T[Mtr.Length];
				float ColsToRows = (float)Mtr[0].Length / (float)Mtr.Length;

				for (int i = 0; i < Mtr.Length; i++)
					Out[i] = Mtr[i][(int)(ColsToRows * i + 0.5)];
			}
			else // more cols = switch cols
			{
				Out = new T[Mtr[0].Length];
				float RowsToCols = (float)Mtr.Length / (float)Mtr[0].Length;

				for (int i = 0; i < Mtr[0].Length; i++)
					Out[i] = Mtr[(int)(RowsToCols * i + 0.5)][i];
			}

			return Out;
		}
		//complete f
		public static T[] GetAntiDiag<T>(T[][] Mtr)
		{
			if (FoolProof)
			{
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			}

			T[] Out;

			if (IsSquare(Mtr))
			{
				Out = new T[Mtr[0].Length];

				for (int i = Mtr[0].Length; i >= 0; i--)
					Out[i] = Mtr[Mtr.Length - 1 - i][i];
			}
			else if (Mtr.Length > Mtr[0].Length) // more rows = switch rows
			{
				Out = new T[Mtr.Length];
				float ColsToRows = (float)Mtr[0].Length / (float)Mtr.Length;

				for (int i = 0; i < Mtr.Length; i++)
					Out[i] = Mtr[i][(int)(ColsToRows * (Mtr.Length - 1 - i) + 0.5)];
			}
			else // more cols = switch cols
			{
				Out = new T[Mtr[0].Length];
				float RowsToCols = (float)Mtr.Length / (float)Mtr[0].Length;

				for (int i = 0; i < Mtr[0].Length; i++)
					Out[i] = Mtr[(int)(RowsToCols * (Mtr.Length - 1 - i) + 0.5)][i];
			}

			return Out;
		}

		//complete f
		public static T[][] GetMinor<T>(T[][] Mtr, int row, int col)
		{
			if (FoolProof)
			{
				if (!Gen.IsNumeric<T>()) throw Exceptions.NotNumeric;
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			}

			T[][] Out = new T[Mtr.Length - 1][];
			int i1, i2;
			for (i1 = 0; i1 < row; i1++)
			{
				Out[i1] = new T[Out.Length];

				for (i2 = 0; i2 < col; i2++)
					Out[i1][i2] = Mtr[i1][i2];

				for (i2 = col; i2 < Out[0].Length; i2++)
					Out[i1][i2] = Mtr[i1][i2 + 1];
			}
			for (i1 = row; i1 < Out.Length; i1++)
			{
				Out[i1] = new T[Out.Length];

				for (i2 = 0; i2 < col; i2++)
					Out[i1][i2] = Mtr[i1 + 1][i2];

				for (i2 = col; i2 < Out[0].Length; i2++)
					Out[i1][i2] = Mtr[i1 + 1][i2 + 1];
			}
			return Out;
		}

		//complete f
		public static double GetDet<T>(T[][] Mtr)
		{
			if (FoolProof)
			{
				if (!Gen.IsNumeric<T>()) throw Exceptions.NotNumeric;
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
				if (!IsSquare(Mtr)) throw Exceptions.NotSquare;
			}

			if (Mtr.Length == 2) return
				(Mtr[0][0]) * (Mtr[1][1] as dynamic)
				- (Mtr[1][0]) * (Mtr[0][1] as dynamic);

			double Det = 0d;
			for (int i = 0; i < Mtr[0].Length; i++)
			{
				Det +=
					(i % 2 == 0 ? 1 : -1)
					* (Mtr[0][i] as dynamic)
					* GetDet(GetMinor(Mtr, 0, i));
			}

			return Det;
		}

		//complete f
		public static double GetAlgComp<T>(T[][] Mtr, int row, int col)
		{
			if (FoolProof)
			{
				if (!Gen.IsNumeric<T>()) throw Exceptions.NotNumeric;
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			}
			return ((row + col) % 2 == 0 ? 1 : -1) * GetDet(GetMinor(Mtr, row, col));
		}

		//complete f
		public static void Invert<T>(ref double[][] Mtr)
		{
			if (FoolProof)
			{
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
				if (!IsSquare(Mtr)) throw Exceptions.NotSquare;
			}

			double[][] Out = Matrix.ToDouble(Mtr);

			int i1, i2;
			double Det = GetDet(Out);

			if (Det == 0) throw Exceptions.ZeroDeterminator;

			for (i1 = 0; i1 < Out.Length; i1++)
				for (i2 = 0; i2 < Out[0].Length; i2++)
					Out[i1][i2] = GetAlgComp(Mtr, i1, i2) / Det;

			Mtr = Transpose(Out);
		}
		//complete f
		public static double[][] Invert<T>(T[][] Mtr)
		{
			if (FoolProof)
			{
				if (!Gen.IsNumeric<T>()) throw Exceptions.NotNumeric;
				if (!IsMatrix(Mtr)) throw Exceptions.NotMatrix;
				if (!IsSquare(Mtr)) throw Exceptions.NotSquare;
			}

			double[][] Out = Matrix.ToDouble(Mtr);

			int i1, i2;
			double Det = GetDet(Out);

			if (Det == 0) throw Exceptions.ZeroDeterminator;

			for (i1 = 0; i1 < Out.Length; i1++)
				for (i2 = 0; i2 < Out[0].Length; i2++)
					Out[i1][i2] = GetAlgComp(Mtr, i1, i2) / Det;

			return Transpose(Out);
		}


		public static T[][] Sum<T>(T[][] Mtr1, T[][] Mtr2, params T[][][] Mtrs)
		{
			if (FoolProof)
			{
				if (!Gen.IsNumeric<T>()) throw Exceptions.NotNumeric;
				if (!IsMatrix(Mtrs)) throw Exceptions.NotMatrix;
				if (!IsSumAble(Mtr1, Mtr2, Mtrs)) throw Exceptions.NotSumable;
			}

			T[][] Out = new T[Mtr1.Length][];
			int i1, i2, i3;

			for (i1 = 0; i1 < Mtr1.Length; i1++)
			{
				Out[i1] = new T[Mtr1[0].Length];

				for (i2 = 0; i2 < Mtr1.Length; i2++)
					Out[i1][i2] = Mtr1[i1][i2] + (Mtr2[i1][i2] as dynamic);
			}


			for (i1 = 1; i1 < Mtrs.Length; i1++) //switching matrices
				for (i2 = 0; i2 < Mtrs[0].Length; i2++) //switching rows
					for (i3 = 0; i3 < Mtrs[0][0].Length; i3++) //switchin cols
						Out[i2][i3] += Mtrs[i1][i2][i3] as dynamic;

			return Out;
		}

		public static T[][] Mult<T>(T[][] Mtr1, T[][] Mtr2)
		{
			if (FoolProof)
			{
				if (!Gen.IsNumeric<T>()) throw Exceptions.NotNumeric;
				if (!IsMatrix(Mtr1, Mtr2)) throw Exceptions.NotMatrix;
				if (!IsMultAble(Mtr1, Mtr2)) throw Exceptions.NotMultable;
			}

			T[][] Out = new T[Mtr1.Length][];

			int i1, i2;
			for (i1 = 0; i1 < Mtr1.Length; i1++)
			{
				Out[i1] = new T[Mtr1.Length];

				for (i2 = 0; i2 < Mtr2[0].Length; i2++)
					Out[i1][i2] = InnerProd(
						GetRow(Mtr1, i1), GetCol(Mtr2, i2));
			}

			return Out;
		}

		// {1,2,3},{4,5,6} => 1*4 + 2*5 + 3*6.
		private static T InnerProd<T>(T[] Arr1, T[] Arr2)
		{
			if (FoolProof)
			{
				if (Gen.IsNumeric<T>()) throw Exceptions.NotNumeric;
			}

			T Sum = 0 as dynamic;

			for (int i = 0; i < Arr1.Length; i++)
				Sum += (Arr1[i] as dynamic) * (Arr2[i] as dynamic);

			return Sum;
		}

		public static (int Row, int Col) IndexOfMin<T>(T[][] Mtr) where T : IComparable
		{
			T Min = Mtr[0][0];

			int
				row, col,
				OutRow = 0, OutCol = 0;

			for (row = 0; row < Mtr.Length; row++)
				for (col = 0; col < Mtr[row].Length; col++)
					if (Mtr[row][col].CompareTo(Min) < 0)
					{
						Min = Mtr[row][col];
						OutRow = row;
						OutCol = col;
					}

			return (OutRow, OutCol);
		}
		public static (int Row, int Col) IndexOfMax<T>(T[][] Mtr) where T : IComparable
		{
			T Max = Mtr[0][0];

			int
				row, col,
				OutRow = 0, OutCol = 0;

			for (row = 0; row < Mtr.Length; row++)
				for (col = 0; col < Mtr[row].Length; col++)
					if (Mtr[row][col].CompareTo(Max) > 0)
					{
						Max = Mtr[row][col];
						OutRow = row;
						OutCol = col;
					}

			return (OutRow, OutCol);
		}
		public static (int Row, int Col) LastIndexOfMin<T>(T[][] Mtr) where T : IComparable
		{
			T Min = Mtr[0][0];

			int
				row, col,
				OutRow = 0, OutCol = 0;

			for (row = Mtr.Length; row >= 0; row--)
				for (col = Mtr[row].Length; col >= 0; col--)
					if (Mtr[row][col].CompareTo(Min) < 0)
					{
						Min = Mtr[row][col];
						OutRow = row;
						OutCol = col;
					}

			return (OutRow, OutCol);
		}
		public static (int Row, int Col) LastIndexOfMax<T>(T[][] Mtr) where T : IComparable
		{
			T Max = Mtr[0][0];

			int
				row, col,
				OutRow = 0, OutCol = 0;

			for (row = Mtr.Length; row >= 0; row--)
				for (col = Mtr[row].Length; col >= 0; col--)
					if (Mtr[row][col].CompareTo(Max) > 0)
					{
						Max = Mtr[row][col];
						OutRow = row;
						OutCol = col;
					}

			return (OutRow, OutCol);
		}
		public static T GetMin<T>(T[][] Mtr) where T : IComparable
		{
			var Index = IndexOfMin(Mtr);
			return Mtr[Index.Row][Index.Col];
		}
		public static T GetMax<T>(T[][] Mtr) where T : IComparable
		{
			var Index = IndexOfMax(Mtr);
			return Mtr[Index.Row][Index.Col];
		}
		public static T[] GetSubArray<T>(T[][] Mtr, Point FirstPoint, Point SecondPoint)
		{
			// Земля пухом долбоебу
			if (Gen.NotLessThanZero(FirstPoint.X - 1) * Mtr[0].Length + Mtr[0].Length - FirstPoint.Y < Gen.NotLessThanZero(SecondPoint.X - 1) * Mtr[0].Length + SecondPoint.Y)
				Gen.Swap(ref FirstPoint, ref SecondPoint); // ну в прочем, поможем ему
				//return new T[0]; // хотя можно и не помогать

			// Колво на первой строке, кол-во на последний, полные строки между ними
			T[] Out = new T[Mtr[0].Length - FirstPoint.X + SecondPoint.X + (SecondPoint.X - FirstPoint.X - 1) * Mtr[0].Length];

			int row, col, AddIndex = 0;

			//first row
			for (row = FirstPoint.X; row == FirstPoint.X; row++)
				for (col = FirstPoint.Y; col < SecondPoint.Y || (row < SecondPoint.X && col < Mtr[row].Length); col++)
					Out[AddIndex++] = Mtr[row][col];
			// other rows
			for (; row < SecondPoint.X; row++)
				for (col = 0; col < Mtr[row].Length; col++)
					Out[AddIndex++] = Mtr[row][col];
			// last row
			for (; row == SecondPoint.X; row++)
				for (col = 0; col < SecondPoint.Y; col++)
					Out[AddIndex++] = Mtr[row][col];

			return Out;
		}
		public static T[][] GetSubMatrix<T>(T[][]Mtr, Point LeftUpperCorner,Point RightDownCorner)
		{
			T[][] Out = Generate<T>(RightDownCorner.X - LeftUpperCorner.X, RightDownCorner.Y - LeftUpperCorner.Y);
			for (int row = 0; row < Out.Length; row++)
				for (int col = 0; col < Out[row].Length; col++)
					Out[row][col] = Mtr[LeftUpperCorner.X + row][LeftUpperCorner.Y + col];
			return Out;
		}
	}


}


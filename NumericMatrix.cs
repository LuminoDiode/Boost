using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Boost
{
	class NumericMatrix
	{
		class Exceptions
		{
			public static readonly ArgumentException NotNumeric = new
				ArgumentException("Данная операция допустима только для матриц числового типа.");
			public static readonly FormatException NotMatrix = new
				FormatException("Passed jagged array has non-constant row length.");
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

		private double[][] ThisMatrix;
		public int RowCount => this.ThisMatrix.Length;
		public int ColCount => this.ThisMatrix[0].Length;
		

		private static double[][] ToDouble<T>(T[][] Mtr)
		{
			double[][] Out = Matrix.Generate<double>(Mtr.Length, Mtr[0].Length);
			for (int row = 0; row < Mtr.Length; row++)
				for (int col = 0; col < Mtr[row].Length; col++)
					Out[row][col] = (double)(Mtr[row][col] as dynamic);
			return Out;
		}

		#region Contructors
		public NumericMatrix(byte[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = ToDouble(Mtr);
		}
		public NumericMatrix(sbyte[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = ToDouble(Mtr);
		}
		public NumericMatrix(short[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = ToDouble(Mtr);
		}
		public NumericMatrix(ushort[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = ToDouble(Mtr);
		}
		public NumericMatrix(int[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = ToDouble(Mtr);
		}
		public NumericMatrix(uint[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = ToDouble(Mtr);
		}
		public NumericMatrix(long[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = ToDouble(Mtr);
		}
		public NumericMatrix(ulong[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = ToDouble(Mtr);
		}
		public NumericMatrix(float[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = ToDouble(Mtr);
		}
		public NumericMatrix(double[][] Mtr)
		{
			if (!Matrix.IsMatrix(Mtr)) throw Exceptions.NotMatrix;
			this.ThisMatrix = Mtr;
		}
		#endregion

		#region Implicit operators
		public static implicit operator NumericMatrix(byte[][] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(sbyte[][] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(short[][] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(ushort[][] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(int[][] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(uint[][] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(long[][] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(ulong[][] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(float[][] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(double[][] Mtr) => new NumericMatrix(Mtr);
		#endregion

		#region Explicit operators
		public static explicit operator double[][](NumericMatrix Mtr) => Mtr.ThisMatrix;
		#endregion

		#region Operation possible checks
		public static bool IsSumable(NumericMatrix Mtr1, NumericMatrix Mtr2) => Mtr1.RowCount == Mtr2.RowCount && Mtr1.ColCount == Mtr2.ColCount;
		public static bool IsMultAble(NumericMatrix Mtr1, NumericMatrix Mtr2) => Mtr1.RowCount == Mtr2.ColCount;
		#endregion

		#region Math operators
		public static NumericMatrix operator +(NumericMatrix Mtr1, NumericMatrix Mtr2)
		{
			if (!IsSumable(Mtr1, Mtr2)) throw Exceptions.NotSumable;

			double[][] Out = Matrix.Generate<double>(Mtr1.RowCount, Mtr1.ColCount);

			for (int row = 0; row < Mtr1.RowCount; row++)
				for (int col = 0; col < Mtr1.ColCount; col++)
					Out[row][col] = Mtr1.GetElement(row,col) + Mtr2.GetElement(row, col);

			return Out;
		}
		#endregion

		#region Get-functions
		public double[] GetRow(int index) => Matrix.GetRow(this.ThisMatrix, index);
		public double[] GetCol(int index) => Matrix.GetCol(this.ThisMatrix, index);
		public double GetElement(int RowIndex, int ColIndex) => this.ThisMatrix[RowIndex][ColIndex];
		#endregion
		#region Set-functions
		public void SetRow() { }
		public void SetCol() { }
		#endregion
	}
}

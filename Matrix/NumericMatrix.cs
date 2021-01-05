using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;

#pragma warning disable CA2208, CA1814, CS0660, CS0661


namespace Boost
{
	class NumericMatrix
	{
		abstract class Exceptions
		{
			
		}

		private double[][] ThisMatrix;
		public int NumOfRows => this.ThisMatrix.Length;
		public int NumOfCols => this.ThisMatrix[0].Length;
		public bool IsSquare => this.NumOfRows == this.NumOfCols;
		public Size Size => new Size(NumOfCols,NumOfRows);


		private static double[][] ToDouble(dynamic NumericTypeMatrix)
		{
			double[][] Out = Matrix.Generate<double>(NumericTypeMatrix.Length, NumericTypeMatrix[0].Length);
			for (int row = 0; row < NumericTypeMatrix.Length; row++)
				for (int col = 0; col < NumericTypeMatrix[row].Length; col++)
					Out[row][col] = (double)(NumericTypeMatrix[row][col]);
			return Out;
		}

		#region Contructors

		enum UnableToCreateErrors
		{
			NoError,
			NotNumericTypeMatrixPassed,
			RowsOfJuggedArrayHasDifferentLength,
			JuggedArrayHasNullRows
		}

		// has null raws
		// not numeric
		// diff lens
		private static bool ValidateInputMatrix<T>(T[][] NumericTypeMatrix, out UnableToCreateErrors Error)
		{
			Error = 0;
			/*
			if (!Gen.IsNumeric<T>())
			{
				Error = UnableToCreateErrors.NotNumericTypeMatrixPassed;
				return false;
			}
			*/
			if (NumericTypeMatrix.Any(x => x is null))
			{
				Error = UnableToCreateErrors.JuggedArrayHasNullRows;
				return false;
			}
			if (!NumericTypeMatrix.All(x => x.Length == NumericTypeMatrix[0].Length))
			{
				Error = UnableToCreateErrors.RowsOfJuggedArrayHasDifferentLength;
				return false;
			}

			return true;
		}

		public NumericMatrix(int NumOfRows, int NumOfCols)
		{
			this.ThisMatrix = Matrix.Generate<double>(NumOfRows, NumOfCols);
		}

		private void CreateThisFromNumeric(dynamic NumericTypeMatrix)
		{
			this.ThisMatrix = ToDouble(NumericTypeMatrix);
		}
		public NumericMatrix(byte[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}
		public NumericMatrix(sbyte[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}
		public NumericMatrix(short[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}
		public NumericMatrix(ushort[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}
		public NumericMatrix(int[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}
		public NumericMatrix(uint[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}
		public NumericMatrix(long[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}
		public NumericMatrix(ulong[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}
		public NumericMatrix(float[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}
		public NumericMatrix(double[][] Mtr)
		{
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(Mtr, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(Mtr);
		}

		public NumericMatrix(byte[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
		}
		public NumericMatrix(sbyte[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
		}
		public NumericMatrix(short[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
		}
		public NumericMatrix(ushort[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
		}
		public NumericMatrix(int[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
		}
		public NumericMatrix(uint[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
		}
		public NumericMatrix(long[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
		}
		public NumericMatrix(ulong[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
		}
		public NumericMatrix(float[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
		}
		public NumericMatrix(double[,] Mtr)
		{
			var JaggedCasted = Matrix.ToJugged(Mtr);
			UnableToCreateErrors OnCreateError;
			if (!ValidateInputMatrix(JaggedCasted, out OnCreateError)) throw new ArgumentException();
			CreateThisFromNumeric(JaggedCasted);
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

		public static implicit operator NumericMatrix(byte[,] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(sbyte[,] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(short[,] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(ushort[,] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(int[,] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(uint[,] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(long[,] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(ulong[,] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(float[,] Mtr) => new NumericMatrix(Mtr);
		public static implicit operator NumericMatrix(double[,] Mtr) => new NumericMatrix(Mtr);
		#endregion

		#region Explicit operators
		public static explicit operator double[][](NumericMatrix Mtr) => Mtr.ThisMatrix;
		#endregion

		#region Operation possible checks
		private static bool IsSumAble(NumericMatrix Mtr1, NumericMatrix Mtr2) => Mtr1.NumOfRows == Mtr2.NumOfRows && Mtr1.NumOfCols == Mtr2.NumOfCols;
		private static bool IsMultAble(NumericMatrix Mtr1, NumericMatrix Mtr2) => Mtr1.NumOfRows == Mtr2.NumOfCols;
		private static bool IsInvertAble(NumericMatrix Mtr) => GetDet(Mtr) != 0 && Mtr.IsSquare;
		#endregion

		#region Math operators

		public static NumericMatrix operator +(NumericMatrix Mtr1, NumericMatrix Mtr2) => Sum(Mtr1, Mtr2);

		public static NumericMatrix operator *(NumericMatrix Mtr1, NumericMatrix Mtr2) => Mult(Mtr1, Mtr2);
		public static NumericMatrix operator *(NumericMatrix Mtr, double Val) => Mult(Mtr, Val);
		public static NumericMatrix operator *(double Val, NumericMatrix Mtr) => Mult(Mtr, Val);

		public static NumericMatrix operator -(NumericMatrix Mtr1, NumericMatrix Mtr2) => Sum(Mtr1, Mtr2 * -1);

		public static NumericMatrix operator /(NumericMatrix Mtr1, NumericMatrix Mtr2) => Mult(Mtr1, Inverted(Mtr2));
		public static NumericMatrix operator /(double Val, NumericMatrix Mtr) => Mult(Inverted(Mtr), Val);
		public static NumericMatrix operator /(NumericMatrix Mtr, double Val) => Mult(Mtr,1/Val);

		public static bool operator ==(NumericMatrix Mtr1, NumericMatrix Mtr2)
		{
			if (Mtr1.Size != Mtr2.Size) return false;

			for(int i1=0;i1<Mtr1.NumOfRows;i1++)
				for (int i2 = 0; i2 < Mtr1.NumOfRows; i2++)
					if (Mtr1.ThisMatrix[i1][i2] != Mtr2.ThisMatrix[i1][i2])
						return false;

			return true;
		}
		public static bool operator !=(NumericMatrix Mtr1, NumericMatrix Mtr2) => !(Mtr1 == Mtr2);
		
		#endregion

		#region Get-functions
		public double[] GetRow(int index) => Matrix.GetRow(this.ThisMatrix, index);
		public double[] GetCol(int index) => Matrix.GetCol(this.ThisMatrix, index);
		public double GetElement(int RowIndex, int ColIndex) => this.ThisMatrix[RowIndex][ColIndex];
		#endregion
		#region Set-functions

		public void SetRow(int index, double[] NewRow)
		{
			if (NewRow.Length != this.NumOfCols) throw new ArgumentException();

			Arr.Copy(NewRow, ref this.ThisMatrix[index]);
		}

		public void SetCol(int index, double[] NewCol)
		{
			Matrix.InsertCol(ref this.ThisMatrix, NewCol, index);
		}

		public void SetElement(int Row, int Col, double NewElement)
		{
			this.ThisMatrix[Row][Col] = NewElement;
		}
		#endregion

		#region Functions

		public NumericMatrix Clone()
		{
			NumericMatrix Out = new NumericMatrix(this.NumOfRows, this.NumOfCols);

			for (int i1 = 0; i1 < this.NumOfRows; i1++)
				for (int i2 = 0; i2 < this.NumOfCols; i2++)
					Out.SetElement(i1, i2, this.ThisMatrix[i1][i2]);

			return Out;
		}

		public void Invert() => this.ThisMatrix = Inverted(this).ThisMatrix;
		public void GetAlgComp(int row, int col) => GetAlgComp(this, row, col);
		public void Transpose() => Matrix.Transpose(ref this.ThisMatrix);

		public void RotateRight(int times = 1)
		{
			while(times--%4!=0) Matrix.RotateRight(ref this.ThisMatrix);
		}

		#endregion

		#region static Functions

		public static NumericMatrix Inverted(NumericMatrix Mtr)
		{
			if (!IsInvertAble(Mtr)) throw new ArgumentException();

			double Det = GetDet(Mtr);

			double[][] Out = Matrix.Clone(Mtr.ThisMatrix);

			for (int i1 = 0; i1 < Out.Length; i1++)
				for (int i2 = 0; i2 < Out[0].Length; i2++)
					Out[i1][i2] = GetAlgComp(Mtr, i1, i2) / Det;

			return Out;
		}
		private static double GetDet(NumericMatrix Mtr)
		{
			if (!Mtr.IsSquare) throw new ArgumentException();

			if (Mtr.NumOfRows == 2) return
				(Mtr.ThisMatrix[0][0]) * (Mtr.ThisMatrix[1][1])
				- (Mtr.ThisMatrix[1][0]) * (Mtr.ThisMatrix[0][1]);

			double Det = 0d;
			for (int i = 0; i < Mtr.NumOfCols; i++)
			{
				Det +=
					(i % 2 == 0 ? 1 : -1)
					* (Mtr.ThisMatrix[0][i])
					* GetDet(Matrix.GetMinor(Mtr.ThisMatrix, 0, i));
			}

			return Det;
		}
		private static double GetAlgComp(NumericMatrix Mtr, int row, int col)
		{
			return ((row + col) % 2 == 0 ? 1 : -1) * GetDet(Matrix.GetMinor(Mtr.ThisMatrix, row, col));
		}
		private static NumericMatrix Mult(in NumericMatrix Mtr1, in NumericMatrix Mtr2)
		{
			if (IsMultAble(Mtr1, Mtr2)) throw new ArgumentException();

			NumericMatrix Out = new NumericMatrix(Mtr1.NumOfCols, Mtr2.NumOfRows);

			for (int i1 = 0; i1 < Mtr1.NumOfRows; i1++)
			for (int i2 = 0; i2 < Mtr2.NumOfCols; i2++)
				Out.ThisMatrix[i1][i2] = InnerProd(Mtr1.GetRow(i1), Mtr2.GetCol(i2));

			return Out;
		}
		private static double InnerProd(double[] Arr1, double[] Arr2)
		{
			double Sum = 0;

			for (int i = 0; i < Arr1.Length; i++) Sum += Arr1[i] * Arr2[i];

			return Sum;
		}
		private static NumericMatrix Mult(in NumericMatrix Mtr, double Val)
		{
			NumericMatrix Out = Mtr.Clone();

			for (int i1 = 0; i1 < Mtr.NumOfRows; i1++)
			for (int i2 = 0; i2 < Mtr.NumOfCols; i2++)
				Out.ThisMatrix[i1][i2] *= Val;

			return Out;
		}
		private static NumericMatrix Sum(NumericMatrix Mtr1, NumericMatrix Mtr2)
		{
			// if dimensions equals
			if (IsSumAble(Mtr1, Mtr2)) throw new ArgumentException();

			NumericMatrix Out = Mtr1.Clone();

			for (int i1 = 0; i1 < Mtr2.NumOfRows; i1++)
			for (int i2 = 0; i2 < Mtr2.NumOfCols; i2++)
				Out.SetElement(i1, i2, Out.GetElement(i2, i2) + Mtr2.GetElement(i1, i2));

			return Out;
		}


		#endregion
	}
}

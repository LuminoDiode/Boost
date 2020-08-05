using System;
using System.Collections.Generic;

namespace Boost
{
	static partial class Binary
	{
		public static int Search<T>(IList<T> Arr, T Val) where T : IComparable //Debugged
		{
			int Min = 0, Max = Arr.Count, Mid, Compare;
			while (Min < Max - 1)
			{
				Mid = Gen.Mid(Min, Max);
				Compare = Val.CompareTo(Arr[Mid]);
				if (Compare < 0) Max = Mid;
				else if (Compare > 0) Min = Mid;
				else return Mid;
			}
			return Arr[0].Equals(Val) ? 0 : -1; // -1 == Not Found
		}

		/// <summary>
		/// Возвращает индекс, на который следует вставить в сортированную коллекцию
		/// переданный элемент, что бы сохранить сортировку.
		/// </summary>
		public static int InsertionIndex<T>(IList<T> Arr, T Val) where T : IComparable //Debugged
		{
			if (Val.CompareTo(Arr[0]) <= 0) return 0;

			int Min = 0, Max = Arr.Count, Mid = 0, Compare;
			while (Min < Max - 1)
			{
				Mid = Gen.Mid(Min, Max);
				Compare = Val.CompareTo(Arr[Mid]);
				if (Compare < 0) Max = Mid;
				else if (Compare > 0) Min = Mid;
				else break;
			}

			for (; Mid < Arr.Count && Val.CompareTo(Arr[Mid]) > 0; Mid++) ;

			return Mid;
		}
	}
}
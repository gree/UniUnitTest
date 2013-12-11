using System;
using System.Collections.Generic;

namespace UniUnitTest
{
	static public class MathUtil
	{
		static public T Clamp<T>(T pMin, T pMax, T pValue)
			where T : IComparable
		{
			if(pValue.CompareTo(pMin) < 0) {
				return pMin;
			}

			if(pValue.CompareTo(pMax) > 0) {
				return pMax;
			}
			
			return pValue;
		}

		public static void Shuffle<T>(this IList<T> list)
		{
			Random rng = new Random(Environment.TickCount);
			int n = list.Count;
			while (n > 1) {
				n--;
				int k = rng.Next(n + 1);  
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}

		public static bool RandomBoolean()
		{
			if ((new Random(Environment.TickCount)).Next(0,2) == 0)
			{
				return false;
			} else {
				return true;
			}
		}

	}
}


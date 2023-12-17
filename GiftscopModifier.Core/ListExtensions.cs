using System.Collections.Generic;

namespace GiftscopModifier.Core
{
	internal static class ListExtensions
	{
		public static List<T> TakeAndRemove<T>(this List<T> list, int count)
		{
			List<T> takenList = list.Take(count).ToList();

			list.RemoveRange(0, count);

			return takenList;
		}
	}
}

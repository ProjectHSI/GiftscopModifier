using System.Collections.Generic;

namespace GiftscopModifier.Core
{
	internal static class ListExtensions
	{
		public static List<T> TakeAndRemove<T>(this List<T> list, int count)
		{
			List<T> takenList = ( List<T> ) list.Take(count);

			list.RemoveRange(0, count);

			return takenList;
		}
	}
}

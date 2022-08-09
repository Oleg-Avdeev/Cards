using System.Collections.Generic;
using System;
using System.Linq;

namespace Witches.Utilities
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Do<T>(this IEnumerable<T> enumerable, Action<T> action)
			=> enumerable.Select(x => { action(x); return x; });
	}
}
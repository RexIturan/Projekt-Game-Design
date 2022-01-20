using System.Collections.Generic;

namespace Util.Extensions {
	public static class ListExtensions {
		
		public static bool IsValidIndex<T>(this List<T> list, int index) {
			bool valid = index >= 0 && index < list.Count;
			return valid;
		}
	}
}
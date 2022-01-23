using System.Collections.Generic;
using System.Text;

namespace Util.Extensions {
	public static class ListExtensions {
		
		public static bool IsValidIndex<T>(this List<T> list, int index) {
			bool valid = index >= 0 && index < list.Count;
			return valid;
		}

		public static string AllToString<T>(this List<T> list) {
			var str = new StringBuilder();
			
			foreach ( var obj in list ) {
				str.Append(obj.ToString());
				str.Append(" | ");
			}

			return str.ToString();
		}
	}
}
using UnityEngine.UIElements;

namespace GDP01.Util.Util.UI {
	public static class VisualElementExtensions {
		public static void SetStyleDisplayVisibility(this VisualElement element, bool visibility) {
			element.style.display = visibility ? DisplayStyle.Flex : DisplayStyle.None;
		}
		
		public static void SetStyleVisibility(this VisualElement element, bool visibility) {
			element.style.visibility = visibility ? Visibility.Visible : Visibility.Hidden;
		}
	}
}
using UnityEngine.UIElements;

namespace GDP01.Util.Util.UI {
	public static class CustomVisualElementExtensions {
		public static void SetStyleDisplayVisibility(this VisualElement element, bool visibility) {
			element.style.display = visibility ? DisplayStyle.Flex : DisplayStyle.None;
		}
		
		public static void SetStyleVisibility(this VisualElement element, bool visibility) {
			element.style.visibility = visibility ? Visibility.Visible : Visibility.Hidden;
		}
		
		public static string GetComponentName(string baseName, string component) {
			return $"{baseName}-{component}";
		}
		
		public static string GetClassNameWithSuffix(string baseName, string suffix) {
			return ConcatClassNames(baseName, suffix);
		}

		public static string ConcatClassNames(string firstSuffix, string secondSuffix) {
			return $"{firstSuffix}-{secondSuffix}";	
		}
		
		public static string GetClassNameWithHover(string className) {
			return $"{className}:hover";
		}
		
		public static string GetClassNameWithFocus(string className) {
			return $"{className}:focus";
		}
		
		public static void InitContainer(
			ref VisualElement container, 
			string BaseComponentName, string ComponentName, 
			string baseClass, string[] suffix) {
			
			container = new VisualElement {
				name = GetComponentName(BaseComponentName, ComponentName),
			};
			foreach ( var s in suffix ) {
				container.AddToClassList(GetClassNameWithSuffix(baseClass, s));
			}
		}

		public static void InitLabel(
			ref Label label, 
			string BaseComponentName, string ComponentName, string labelText, 
			string baseClass, string suffix) {
			
			label = new Label {
				name = GetComponentName(BaseComponentName, ComponentName),
				text = labelText
			};
			
			label.AddToClassList(GetClassNameWithSuffix(baseClass, suffix));
		}

		public static void InitButton( 
			ref Button button, 
			string baseComponentName, string componentName, string buttonText, 
			string baseClass, string[] suffixes) {
			
			button = new Button {
				name = GetComponentName(baseComponentName, componentName),
				text = buttonText
			};

			foreach ( var suffix in suffixes ) {
				button.AddToClassList(GetClassNameWithSuffix(baseClass, suffix));
			}
		}
	}
}
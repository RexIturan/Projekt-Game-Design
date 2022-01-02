using UnityEngine.UIElements;

namespace UI.Components.StatusValue.StatusBar {
	public class StatusBar : VisualElement {

		//--- STATUS BAR ---//
		// name
		// text color
		// 
		
		//--- PROGRESS BAR ---//
		// current  value
		// min
		// max 
		// base color
		// add color
		// remove color
		// background color
		
		
		public new class UxmlFactory : UxmlFactory<StatusBar, UxmlTraits> { }
		public new class UxmlTraits : VisualElement.UxmlTraits { }
	}
}
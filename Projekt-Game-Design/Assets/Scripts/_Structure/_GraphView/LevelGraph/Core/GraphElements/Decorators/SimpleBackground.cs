using UnityEngine;
using UnityEngine.UIElements;

namespace _Structure._GraphView.LevelGraph.Core.GraphElements.Decorators {
	
	/// <summary>
	/// The grid drawn as a background of the <see cref="GraphView"/>.
	/// </summary>
	public class SimpleBackground : VisualElement {
		// public static readonly string ussClassName = "";
		// public static readonly string ussFileName = "";
		private static readonly string componentName = "Simple-Background";
		
		public static readonly Color s_DefaultGridBackgroundColor = new Color(0.17f, 0.17f, 0.17f, 1.0f);
		
		Color m_backgroundColor = s_DefaultGridBackgroundColor;
		Color backgroundColor => m_backgroundColor;

		public SimpleBackground() {
			name = componentName;
			
			pickingMode = PickingMode.Ignore;
			this.StretchToParentSize();
			style.backgroundColor = backgroundColor;
		}
	}
}
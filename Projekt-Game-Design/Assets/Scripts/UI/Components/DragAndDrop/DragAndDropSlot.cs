using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GDP01.Util.UI.CustomVisualElementExtensions;

namespace GDP01.UI.Components.DragAndDrop {
	public class DragAndDropSlot : VisualElement {
///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private static readonly string baseUssClassName = "characterIcon";

		private static readonly string containerSuffix = "container";
		
		private static readonly string defaultStyleSheet = "UI/characterIcon";

///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////

///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		private VisualElement container;

///// PROPERTIES ///////////////////////////////////////////////////////////////////////////////////

///// PRIVATE FUNCTIONS ////////////////////////////////////////////////////////////////////////////

		private void BuildComponent() {
			//default styleSheet
			this.styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
			this.AddToClassList(baseUssClassName);

			container = new VisualElement {
				name = "CharacterIcon-Container"
			};
			container.AddToClassList(GetClassNameWithSuffix(baseUssClassName, containerSuffix));

			this.Add(container);
		}

///// Util /////////////////////////////////////////////////////////////////////////////////////////

///// PUBLIC FUNCTIONS  //////////////////////////////////////////////////////////////////////////// 

		public void UpdateComponent() {
		}

///// PUBLIC CONSTRUCTORS / UI Element Functions ///////////////////////////////////////////////////		
		
		public new class UxmlFactory : UxmlFactory<DragAndDropSlot, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is DragAndDropSlot element ) {
					element.Clear();

					element.BuildComponent();
					element.UpdateComponent();
				}
			}
		}
		
		public DragAndDropSlot() : this("Character Name"){}

		public DragAndDropSlot(string name) {
			this.name = "CharacterIcon";
			
			BuildComponent();
			UpdateComponent();
		}
	}
}
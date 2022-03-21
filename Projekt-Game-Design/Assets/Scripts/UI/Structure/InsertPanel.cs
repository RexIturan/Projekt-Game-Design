using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GDP01.UI {
	public class InsertPanel : MonoBehaviour {
		[SerializeField] private UIDocument parentUiDocument;
		[SerializeField] private VisualTreeAsset sourceAsset;
		[SerializeField] private string containerName = "Content";
		[SerializeField] private bool useTemplateContainer;
		[SerializeField] private List<MonoBehaviour> enableAfter; 
///// Private Variables ////////////////////////////////////////////////////////////////////////////		
		
		private TemplateContainer sourceTemplate;
		private VisualElement sourceParent;
		private VisualElement[] sourceElements;
		private bool isCreated;

///// Properties ///////////////////////////////////////////////////////////////////////////////////

		public VisualElement Root => sourceParent;

///// Private Functions ////////////////////////////////////////////////////////////////////////////		
		
		private VisualElement[] GetChildrenAsArray(TemplateContainer templateContainer) {
			if ( templateContainer is null )
				return null;
			
			var childCount = templateContainer.contentContainer.childCount;
			var children = templateContainer.contentContainer;
			VisualElement[] elements = new VisualElement[childCount];
			for ( int i = 0; i < childCount; i++ ) {
				elements[i] = children[i];
			}
			return elements;
		}
		
		private void AddVisualElementsAsChildren(VisualElement parent, VisualElement[] children) {
			foreach ( var child in children ) {
				parent.Add(child);
			}
		}
		
		private void RemoveVisualElementsAsChildren(VisualElement parent, VisualElement[] children) {
			foreach ( var child in children ) {
				parent.Remove(child);
			}
		}
		
///// Public Functions /////////////////////////////////////////////////////////////////////////////

		public void Init() {
			if(isCreated)
				Reset();
			
			parentUiDocument ??= gameObject.GetComponentInParent<UIDocument>();
			
			if ( parentUiDocument is null ) {
				Debug.LogError("Cant Find UI Document in Parent!");
				return;
			}

			var root = parentUiDocument.rootVisualElement;
			
			//create Visual Tree from Asset
			sourceTemplate = sourceAsset.Instantiate();

			sourceParent = root.Q<VisualElement>(containerName) ?? root;
			
			sourceElements = useTemplateContainer ? new VisualElement[] { sourceTemplate } : GetChildrenAsArray(sourceTemplate);

			AddVisualElementsAsChildren(sourceParent, sourceElements);

			isCreated = true;
		}

		public void Reset() {
			if ( isCreated ) {
				RemoveVisualElementsAsChildren(sourceParent, sourceElements);
				isCreated = false;	
			}
		}

		///// Unity Functions //////////////////////////////////////////////////////////////////////////////		
		
		private void OnEnable() {
			Init();
			if(Root != null)
				enableAfter?.ForEach(behaviour => behaviour.enabled = true);
		}

		private void OnDisable() {
			if(Root != null)
				enableAfter?.ForEach(behaviour => behaviour.enabled = false);
			Reset();
		}

		private void OnValidate() {
			enableAfter?.ForEach(behaviour => behaviour.enabled = false);
		}
	}
}
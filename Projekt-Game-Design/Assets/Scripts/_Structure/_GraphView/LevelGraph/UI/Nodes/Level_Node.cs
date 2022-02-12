using _Structure._GraphView.LevelGraph.UI.Parts;
using _Structure._GraphView.LevelGraph.Util;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace _Structure._GraphView.LevelGraph.UI.Nodes {
	public class Level_Node : CollapsibleInOutNode {

		public static readonly string levelNodeUssClassName = "ge-level-node";
		public static readonly string levelPortContainerPartName = "port-container";
		
		private SelectionBorder _selectionBorder;
		private VisualElement _contentContainer;
		
		/// <inheritdoc />
		protected override void BuildPartList() {
			PartList.AppendPart(IconTitleProgressPart.Create(titleIconContainerPartName, Model, this, ussClassName));
			// PartList.AppendPart(InOutPortContainerPart.Create(portContainerPartName, Model, this, ussClassName));
			PartList.AppendPart(LevelConnection_PortContainer_Part.Create(levelPortContainerPartName, Model, this, levelNodeUssClassName));
		}
		
		/// <inheritdoc />
		protected override void BuildElementUI() {
			AddToClassList("ge-level-node");
			this.AddStylesheet("Custom/level_Node.uss");
			
			_selectionBorder = new SelectionBorder { name = selectionBorderElementName };
			_selectionBorder.AddToClassList(ussClassName.WithUssElement(selectionBorderElementName));
			Add(_selectionBorder);
			
			_contentContainer = new VisualElement{name = "Content-Container"};
			_contentContainer.AddToClassList(levelNodeUssClassName.WithUssElement("content-container"));
			Add(_contentContainer);
			m_ContentContainer = _contentContainer;

			// base.BuildElementUI();

			var disabledOverlay = new VisualElement { name = disabledOverlayElementName, pickingMode = PickingMode.Ignore };
			hierarchy.Add(disabledOverlay);
		}
	}
}
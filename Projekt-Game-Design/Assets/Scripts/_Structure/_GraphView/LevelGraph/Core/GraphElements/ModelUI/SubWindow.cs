using _Structure._GraphView.LevelGraph.Core.Model;
using _Structure._GraphView.LevelGraph.Core.Model.Interfaces;
using _Structure._GraphView.LevelGraph.Util;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Structure._GraphView.LevelGraph.Core.GraphElements.ModelUI {
	public class SubWindow : GraphElement {
		public new class UxmlFactory : UxmlFactory<SubWindow> { }

		public static readonly Vector2 defaultSize = new Vector2(200, 160);

		public new static readonly string ussClassName = "ge-sub-window";

		public static readonly string contentContainerElementName = "content-container";
		public static readonly string selectionBorderElementName = "selection-border";
		public static readonly string resizerPartName = "resizer";

		protected VisualElement m_ContentContainer;

		/// <inheritdoc />
		public override VisualElement contentContainer => m_ContentContainer ?? this;

		public ISubWindowModel SubWindowModelModel => Model as ISubWindowModel;

		/// <inheritdoc />
		protected override void BuildPartList() {
			PartList.AppendPart(FourWayResizerPart.Create(resizerPartName, Model, this, ussClassName));
		}

		/// <inheritdoc />
		protected override void BuildElementUI() {
			base.BuildElementUI();

			var selectionBorder = new SelectionBorder { name = selectionBorderElementName };
			selectionBorder.AddToClassList(ussClassName.WithUssElement(selectionBorderElementName));
			Add(selectionBorder);

			// var container = new VisualElement { name = "Content Container" };
			// container.AddToClassList(ussClassName.WithUssElement(contentContainerElementName));
			// Add(container);
			
			// m_ContentContainer = container.contentContainer;
		}

		/// <inheritdoc />
		protected override void PostBuildUI() {
			// base.PostBuildUI();

			usageHints = UsageHints.DynamicTransform;
			AddToClassList(ussClassName);
			this.AddStylesheet("/Core/SubWindow.uss");
		}

		/// <inheritdoc />
		protected override void UpdateElementFromModel() {
			base.UpdateElementFromModel();

			// update position
			var newPos = SubWindowModelModel.PositionAndSize;
			style.left = newPos.x;
			style.top = newPos.y;
			style.width = newPos.width;
			style.height = newPos.height;
			
			
		}
	}
}
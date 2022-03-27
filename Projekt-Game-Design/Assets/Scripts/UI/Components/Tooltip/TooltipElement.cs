using UnityEngine;
using UnityEngine.UIElements;
using Util.UpdateHelper;

namespace UI.Components.Tooltip {
	/// <summary>
	/// Generic tooltip component. Tooltip can be filled like a regular 
	/// VisualElement to display arbitrary content. 
	/// 
	/// </summary>
	public class TooltipElement : VisualElement, UpdatedClass {
		
		public new class UxmlFactory : UxmlFactory<TooltipElement, UxmlTraits> { }
		public new class UxmlTraits : VisualElement.UxmlTraits { }
		
		public TooltipElement() : base() { }


		private static readonly string defaultStyleSheet = "tooltip";
		private static readonly string className = "tooltipElement";

		private VisualElement tooltipParent;
		private VisualElement tooltipLayer;

		// these may change depending on the 
		protected bool onTop = true;
		protected float timeToWait = 1.5f;
		protected float blendInTime = 0.15f;

		protected bool isActive = true;

		private float hoverTime;
		private bool isHovering;


		/// <summary>
		/// Creates basic tooltip. Tooltip will be displayed underneath or on top of 
		/// given parent after the parent has been hovered for a certain time period. 
		/// </summary>
		/// <param name="tooltipParent">Parent of this tooltip </param>
		public TooltipElement(VisualElement tooltipParent) {
			// Initializing helper for calling updates 
			UpdateHelper helper = UpdateHelper.Current;
			if ( helper )
				helper.Subscribe(this);
			else
				Debug.LogWarning("No Update helper found. Cannot use tooltip element. ");

			// Setting up style
			styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
			AddToClassList(className);
			pickingMode = PickingMode.Position;

			this.tooltipParent = tooltipParent;

			isHovering = false;
			hoverTime = 0;
			HideTooltip();

			tooltipParent.RegisterCallback<MouseEnterEvent>(HandleMouseEnterParent);
			tooltipParent.RegisterCallback<MouseLeaveEvent>(HandleMouseLeaveParent);
			RegisterCallback<MouseEnterEvent>(HandleMouseEnterTooltip);
			RegisterCallback<MouseLeaveEvent>(HandleMouseLeaveTooltip);

			// Adding tooltip to layer
			tooltipLayer = TooltipLayer.FindTooltipLayer();
			if ( tooltipLayer != null )
				tooltipLayer.Add(this);
			else
				Debug.LogWarning("No tooltip layer found. ");
		}

		private void HandleMouseEnterParent(MouseEnterEvent enterEvent) {
			isHovering = true;
		}

		private void HandleMouseLeaveParent(MouseLeaveEvent leaveEvent) {
			if ( isHovering )
				tooltipParent.pickingMode = PickingMode.Position;

			isHovering = false;
		}

		private void HandleMouseEnterTooltip(MouseEnterEvent enterEvent) {
			isHovering = true;
		}

		private void HandleMouseLeaveTooltip(MouseLeaveEvent leaveEvent) {
			isHovering = false;

			tooltipLayer.pickingMode = PickingMode.Ignore;
		}

		private void ShowTooltip(float opacity) {
			tooltipLayer.BringToFront();
			BringToFront();
			style.visibility = Visibility.Visible;
			style.opacity = opacity;
		}

		private void HideTooltip() {
			style.visibility = Visibility.Hidden;

			if ( tooltipLayer != null )
				tooltipLayer.pickingMode = PickingMode.Ignore;
		}

		void UpdatedClass.Update() {
			if ( isActive && isHovering ) {
				// handle oppacity
				if ( hoverTime <= timeToWait + blendInTime )
					hoverTime += Time.deltaTime;

				if ( hoverTime >= timeToWait ) {
					float opacity = ( hoverTime - timeToWait ) / blendInTime;
					ShowTooltip(opacity);
				}

				// handle position
				UpdatePosition();
			}
			else {
				hoverTime = 0;
				HideTooltip();
			}
		}

		/// <summary>
		/// Calculates the position of the tooltip according to 
		/// its parent's position, their respective dimensions and 
		/// the settings. 
		/// </summary>
		private void UpdatePosition() {
			// y-axis
			// absolute position of upper border of parent
			float parentYPos = tooltipLayer.worldBound.height - tooltipParent.worldBound.y;
			// position of the tooltip if it is beneath the parent
			float posForBottom = parentYPos - tooltipParent.resolvedStyle.height - resolvedStyle.height;

			// draw on top if there is enough space and it is set to OnTop, or if on bottom is not enough space
			if ( ( onTop && parentYPos + resolvedStyle.height < tooltipLayer.worldBound.height ) ||
			     posForBottom < 0 )
				style.bottom = parentYPos;
			else {
				style.bottom = posForBottom;
			}

			// x-axis
			// left bound for a tooltip that's centered relative to its parent
			float xPos = tooltipParent.worldBound.x -
			             0.5f * ( resolvedStyle.width - tooltipParent.resolvedStyle.width );
			// correction if bounds are overlapped
			xPos = Mathf.Max(xPos, 0);
			xPos = Mathf.Min(xPos, tooltipLayer.worldBound.width - resolvedStyle.width);
			style.left = xPos;
		}

		public void Activate() {
			isActive = true;
		}

		public void Deactivate() {
			isActive = false;
		}
	}
}
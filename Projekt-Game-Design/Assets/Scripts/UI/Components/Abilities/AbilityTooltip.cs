using System.Collections;
using System.Collections.Generic;
using UI.Components.Ability;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.Tooltip
{
		/// <summary>
		/// Tooltip that contains ability properties (name and description) 
		/// and is shown when action button is hovered. 
		/// TODO: Use AbilityDetailPanel instead 
		/// </summary>
		public class AbilityTooltip : TooltipElement
		{
				private static readonly string className = "tooltipHeader";

				public AbilityTooltip(VisualElement tooltipParent) : base(tooltipParent) {}

				public void UpdateValues(AbilitySO ability) {
						Clear();
						Add(new AbilityDetailPanel(ability));
				}
		}
}
using System.Collections;
using System.Collections.Generic;
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

				private TextElement header;
				private TextElement description;

				public AbilityTooltip(VisualElement tooltipParent) : base(tooltipParent)
				{
						header = new TextElement();
						header.AddToClassList(className);
						description = new TextElement();

						Add(header);
						Add(description);
				}

				public void UpdateValues(string name, string description)
				{
						header.text = name;
						this.description.text = description;
				}
		}
}
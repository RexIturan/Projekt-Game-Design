using UI.Components.Item;
using UnityEngine.UIElements;

namespace UI.Components.Tooltip
{
		/// <summary>
		/// Tooltip that contains item properties (name, description and abilities). 
    /// Is displayed on hovering an item in the inventory. 
		/// </summary>
		public class ItemTooltip : TooltipElement
		{
				public ItemTooltip(VisualElement tooltipParent) : base(tooltipParent) {}

				public void UpdateValues(ItemSO item)
				{
						Clear();

						if(item is WeaponSO)
								Add(new ItemDetailPanel((WeaponSO) item, true));
						else if(item is ArmorSO)
								Add(new ItemDetailPanel((ArmorSO) item, false));
						else
								Add(new ItemDetailPanel(item, false));
				}
		}
}
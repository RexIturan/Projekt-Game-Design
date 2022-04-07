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
			public ItemTooltip(VisualElement tooltipParent) : base(tooltipParent) {
				timeToWait = 0.5f;
			}

				public void UpdateValues(ItemTypeSO itemType)
				{
						Clear();

						if(itemType is WeaponTypeSO)
								Add(new ItemDetailPanel((WeaponTypeSO) itemType, true));
						else if(itemType is ArmorTypeSO)
								Add(new ItemDetailPanel((ArmorTypeSO) itemType, true));
						else if(itemType is PotionTypeSO )
								Add(new ItemDetailPanel((PotionTypeSO)itemType, true));
						else
								Add(new ItemDetailPanel(itemType, false));
				}
		}
}
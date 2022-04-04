using UnityEngine;

/// <summary>
/// Healing potion. 
/// </summary>
[CreateAssetMenu(fileName = "new_healingPotion", menuName = "Items/Healing Potion")]
public class PotionTypeSO : ItemTypeSO
{
		public int healing;

		override public bool ValidForPosition(EquipmentPosition equipmentPosition) {
				return false;
		}
}

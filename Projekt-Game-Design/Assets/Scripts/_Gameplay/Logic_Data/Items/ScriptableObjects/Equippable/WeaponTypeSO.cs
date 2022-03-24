using UnityEngine;

/// <summary>
/// WeaponSO
/// </summary>
[CreateAssetMenu(fileName = "new_weapon", menuName = "Items/Weapon")]
public class WeaponTypeSO : ItemTypeSO
{
		override public bool ValidForPosition(EquipmentPosition equipmentPosition) {
				return equipmentPosition.Equals(EquipmentPosition.LEFT) || equipmentPosition.Equals(EquipmentPosition.RIGHT);
		}
}

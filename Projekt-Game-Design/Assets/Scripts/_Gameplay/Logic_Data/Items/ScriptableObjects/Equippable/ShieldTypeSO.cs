using UnityEngine;

/// <summary>
/// ShieldSO
/// </summary>
[CreateAssetMenu(fileName = "new_shield", menuName = "Items/shield")]
public class ShieldTypeSO : ArmorTypeSO
{
		override public bool ValidForPosition(EquipmentPosition equipmentPosition) { return equipmentPosition.Equals(EquipmentPosition.SHIELD); }
}

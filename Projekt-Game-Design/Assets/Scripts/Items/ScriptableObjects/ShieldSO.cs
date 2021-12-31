using UnityEngine;

/// <summary>
/// ShieldSO
/// </summary>
[CreateAssetMenu(fileName = "new_shield", menuName = "Items/shield")]
public class ShieldSO : ArmorSO
{
		override public bool ValidForPosition(EquipmentPosition equipmentPosition) { return equipmentPosition.Equals(EquipmentPosition.SHIELD); }
}

using UnityEngine;

/// <summary>
/// BodyArmorSO
/// </summary>
[CreateAssetMenu(fileName = "new_bodyArmor", menuName = "Items/Body Armor")]
public class BodyArmorSO : ArmorSO
{
		override public bool ValidForPosition(EquipmentPosition equipmentPosition) { return equipmentPosition.Equals(EquipmentPosition.BODY); }
}

using UnityEngine;

/// <summary>
/// BodyArmorSO
/// </summary>
[CreateAssetMenu(fileName = "new_bodyArmor", menuName = "Items/Body Armor")]
public class BodyArmorTypeSO : ArmorTypeSO
{
		override public bool ValidForPosition(EquipmentPosition equipmentPosition) { return equipmentPosition.Equals(EquipmentPosition.BODY); }
}

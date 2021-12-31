using UnityEngine;

/// <summary>
/// HeadArmorSO
/// </summary>
[CreateAssetMenu(fileName = "new_headArmor", menuName = "Items/Head Armor")]
public class HeadArmorSO : ItemSO
{
		override public bool ValidForPosition(EquipmentPosition equipmentPosition) { return equipmentPosition.Equals(EquipmentPosition.HEAD); }
}

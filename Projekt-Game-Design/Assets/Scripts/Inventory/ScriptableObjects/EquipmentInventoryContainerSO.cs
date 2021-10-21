using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Equipment Inventory Container
/// Container for all EquipmentInventories 
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/EquipmentInventoryContainer")]
public class EquipmentInventoryContainerSO : ScriptableObject
{
    [SerializeField] public List<EquipmentInventorySO> inventories = new List<EquipmentInventorySO>(3);
}

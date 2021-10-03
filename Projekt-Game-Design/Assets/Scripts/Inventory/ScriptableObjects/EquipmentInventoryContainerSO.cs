using System.Collections.Generic;
using UnityEngine;

// Inventar
// Enthält die Liste der Equipment Items, die einem Charakter zur Verfügung stehen
//
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/EquipmentInventoryContainer")]
public class EquipmentInventoryContainerSO : ScriptableObject
{
    [SerializeField] public List<EquipmentInventorySO> Inventorys = new List<EquipmentInventorySO>(3);
}

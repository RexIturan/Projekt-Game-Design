using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inventar
// Enthält die Liste der Equipment Items, die einem Charakter zur Verfügung stehen
//
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/EquipmentInventory")]
public class EquipmentInventorySO : ScriptableObject
{
    [SerializeField] public List<ItemSO> Inventory = new List<ItemSO>(7);
}

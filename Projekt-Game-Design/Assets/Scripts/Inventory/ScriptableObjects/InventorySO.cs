using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inventar
// Enthält die Liste der Items, die dem Spieler zur Verfügung stehen
//
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    public List<ItemSO> Inventory = new List<ItemSO>();
}

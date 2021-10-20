using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InventorySO
/// Has a List of Items
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    public List<ItemSO> inventory = new List<ItemSO>();
}

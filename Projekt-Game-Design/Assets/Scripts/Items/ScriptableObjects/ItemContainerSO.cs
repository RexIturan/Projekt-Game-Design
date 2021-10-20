using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item Container SO
/// List of each Item
/// </summary>
[CreateAssetMenu(fileName = "New ItemList", menuName = "Items/ItemList")]
public class ItemContainerSO : ScriptableObject
{
    [SerializeField] public List<ItemSO> itemList = new List<ItemSO>();
}

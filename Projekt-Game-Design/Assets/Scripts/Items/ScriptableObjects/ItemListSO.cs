using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ItemList
// Enthält alle Items die es gibt.
//
[CreateAssetMenu(fileName = "New ItemList", menuName = "Items/ItemList")]
public class ItemListSO : ScriptableObject
{
    [SerializeField] public List<ItemSO> ItemList = new List<ItemSO>();
}

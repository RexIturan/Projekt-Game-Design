using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item
// contains info about use, type and stats, looks
//
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] public int id;
    [SerializeField] public int goldValue;
    [SerializeField] public int rarity;
    [SerializeField] public Sprite icon;
    [SerializeField] public GameObject prefab;

    [SerializeField] public EItemType type; // Quest-item? Weapon? Etc. 

    [SerializeField] public object[] actions; 

}

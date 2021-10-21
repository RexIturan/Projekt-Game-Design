using UnityEngine;

/// <summary>
/// Item SO
/// contains info about use, type, stats and looks of a Item 
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] public int id;

    // art
    [SerializeField] public Sprite icon;
    [SerializeField] public GameObject prefab;

    // data
    [SerializeField] public int goldValue;
    [SerializeField] public int rarity;
    [SerializeField] public ItemType type; // Quest-item? Weapon? Etc. 

    [SerializeField] public AbilitySO[] abilities; 
}

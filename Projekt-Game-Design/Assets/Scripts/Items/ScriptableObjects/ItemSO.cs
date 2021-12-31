using UnityEngine;

/// <summary>
/// Item SO
/// contains info about use, type, stats and looks of a Item 
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    public int id;

    // art
    public Sprite icon;
    public Mesh model;
    public GameObject prefab;
    public Mesh mesh;
    public Material material;

    // data
    public int goldValue;
    public int rarity;
    public ItemType type; // Quest-item? Weapon? Etc. 

		/**
		 * returns true, if the item type can be used for respective equipment positions
		 */
		public virtual bool ValidForPosition(EquipmentPosition equipmentPosition) { return false; }
}

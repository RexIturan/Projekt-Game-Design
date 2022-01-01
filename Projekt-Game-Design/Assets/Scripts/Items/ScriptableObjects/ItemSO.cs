using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Item SO
/// contains info about use, type, stats and looks of a Item 
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemSO : ScriptableObject {
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

    public AbilitySO[] abilities;

#if UNITY_EDITOR
    private void OnEnable() {
			var itemContainers = AssetDatabase.FindAssets($"t:{nameof(ItemContainerSO)}");

			foreach ( var containerGuid in itemContainers ) {
				var containerPath = AssetDatabase.GUIDToAssetPath(containerGuid);
				var itemContainer = AssetDatabase.LoadAssetAtPath<ItemContainerSO>(containerPath);
			    
				if ( !itemContainer.itemList.Contains(this) ) {
					itemContainer.itemList.Add(this);
					itemContainer.UpdateItemList();
				}  
			}
    }

    private void OnDisable() {
	    var itemContainers = AssetDatabase.FindAssets($"t:{nameof(ItemContainerSO)}");

	    foreach ( var containerGuid in itemContainers ) {
		    var containerPath = AssetDatabase.GUIDToAssetPath(containerGuid);
		    var itemContainer = AssetDatabase.LoadAssetAtPath<ItemContainerSO>(containerPath);
			    
		    if ( itemContainer.itemList.Contains(this) ) {
			    itemContainer.itemList.Remove(this);
			    itemContainer.UpdateItemList();
		    }  
	    }
    }
#endif
}

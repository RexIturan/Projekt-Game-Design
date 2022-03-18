using System;
using GDP01.Util;
using SaveSystem.V2.Data;
using UnityEditor;
using UnityEngine;
using WorldObjects;

/// <summary>
/// Item SO
/// contains info about use, type, stats and looks of a Item 
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemSO : WorldObject.TypeSO {
	
		public class ItemTypeData : ReferenceData {
			public new ItemSO obj;
		}
	

    // art
    public Sprite icon;
    public Mesh mesh;
    public Material material;

    // data
    public int goldValue;
    public int rarity;
    public ItemType type; // Quest-item? Weapon? Etc. 

    public AbilitySO[] abilities;
    
		/// <summary>
		/// returns true, if the item type can be used for respective equipment positions
		/// </summary>
		public virtual bool ValidForPosition(EquipmentPosition equipmentPosition) { return false; }

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
#endif

	public new ItemTypeData ToData() {
		return new ItemTypeData {
			guid = this.Guid,
			name = name,
			obj = this
		};
	}

	public ItemComponent.ItemData ToComponentData() {
		return base.ToComponentData<ItemComponent.ItemData>();
	}
}

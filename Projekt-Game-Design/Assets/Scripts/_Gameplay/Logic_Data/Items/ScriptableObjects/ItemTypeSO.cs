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
public class ItemTypeSO : WorldObject.TypeSO {
	
		public class ItemTypeData : ReferenceData {
			public new ItemTypeSO obj;
		}

		// art
		public string itemName;
		public string description;

    public Sprite icon;
    public Mesh mesh;
    public Material material;
		public float scale;
		public Vector3 rotation;

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
			var itemContainers = AssetDatabase.FindAssets($"t:{nameof(ItemTypeContainerSO)}");

			foreach ( var containerGuid in itemContainers ) {
				var containerPath = AssetDatabase.GUIDToAssetPath(containerGuid);
				var itemContainer = AssetDatabase.LoadAssetAtPath<ItemTypeContainerSO>(containerPath);
			    
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

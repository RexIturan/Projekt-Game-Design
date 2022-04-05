using System.Collections;
using GDP01._Gameplay.Provider;
using GDP01.Loot.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util.Extensions;

namespace WorldObjects {
	public partial class WorldObjectManager {
		[Header("Item Data")]
		[SerializeField] private ItemTypeSO defaultItemTypeSO;
		[SerializeField] private Transform itemParent;
		[SerializeField] private List<ItemComponent> _itemComponents;
		
		private List<ItemComponent.ItemData> SaveItems() {
			return SaveComponents(_itemComponents, managerData.ItemDataList);
		}

		private void LoadItems(List<ItemComponent.ItemData> itemDataList) {
			LoadComponents(
				ref _itemComponents, 
				ref managerData.ItemDataList, 
				itemDataList, 
				defaultItemTypeSO.prefab, 
				itemParent);
		}
		
		[ContextMenu("Add Item")]
		private void AddNewItem() {
			CreateItem(defaultItemTypeSO);
		}

		public void AddItem(ItemComponent item) {
			item.transform.SetParent(itemParent ? itemParent : transform);
			item.Id = _itemComponents.Count;
			_itemComponents.Add(item);
		}
		
		private ItemComponent CreateItem(ItemTypeSO itemType) {
			ItemComponent.ItemData data = itemType.ToComponentData();
			
			//todo refactor get next playerchar id
			data.Id = _itemComponents.Count + managerData.ItemDataList?.Count ?? 0;
			var itemComponent = CreateComponent<ItemComponent, ItemComponent.ItemData>(data, itemParent);
			_itemComponents.Add(itemComponent);
			return itemComponent;
		}
		
		public void AddItemAt(ItemTypeSO itemTypeSO, Vector3 worldPos) {
			var item = CreateItem(itemTypeSO);
			item.GridTransform.MoveTo(worldPos);
			InitItemAppearance(item, itemTypeSO);
		}
		
		public void AddItemAt(ItemTypeSO itemTypeSO, Vector3Int gridPos) {
			var item = CreateItem(itemTypeSO);
			item.GridTransform.MoveTo(gridPos);
			InitItemAppearance(item, itemTypeSO);
		}
		
		public void RemoveItemAt(Vector3 worldPos) {
			var item = GetItemAt(worldPos);
			if ( item is { } ) {
				_itemComponents.Remove(item);
				Destroy(item.gameObject);
			}
		}

		private void InitItemAppearance(ItemComponent item, ItemTypeSO type) {
			item.GetComponentInChildren<MeshFilter>().mesh = type.mesh;
			item.GetComponentInChildren<Renderer>().material = type.material;
		}

		private ItemComponent GetItemAt(Vector3 worldPos) {
			return GetItemAt(_gridData.GetGridPos3DFromWorldPos(worldPos));
		}

		private ItemComponent GetItemAt(Vector3Int gridPos) {
			return _itemComponents.FirstOrDefault(item => item.GridPosition.Equals(gridPos));
		}

		private void ClearItems() {
			_itemComponents.ClearMonoBehaviourGameObjectReferences();
			managerData.ItemDataList.Clear();
		}

		public void RemoveItem(ItemComponent itemComponent) {
			_itemComponents.Remove(itemComponent);
			Destroy(itemComponent.gameObject);
		}
				
		public static void DropLoot(LootTableSO lootTable,
			Vector3Int gridPos) {
			var worldObjectManager = GameplayProvider.Current.WorldObjectManager;

			if ( !worldObjectManager )
				Debug.LogWarning("Could not find worldObjectManager. ");
			else {
				ItemTypeSO itemType = null;
				int dropID = -1;
				var lootDrop = lootTable.GetLootDrop();

				//todo drop multiple items 
				if ( lootDrop.items.Count > 0 ) {
					dropID = lootDrop.items[0].id;
					itemType = lootDrop.items[0];
				}

				if ( dropID >= 0 && itemType is {} ) {
					//todo move reference to grid controller to ItemSpawner or so and just invoke a event here 
					Debug.Log("Dropping loot: " + dropID + " at " + gridPos.x + ", " + gridPos.z);
					
					worldObjectManager.AddItemAt(itemType, gridPos);
				}
			}
		}

		public List<ItemComponent> GetItems() {
			return _itemComponents;
		}
	}
}
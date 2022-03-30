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
			_itemComponents.Add(item);
		}
		
		public void AddItemAt(ItemTypeSO itemTypeSO, Vector3Int gridPos) {
			var item = CreateItem(itemTypeSO);
			item.GridTransform.MoveTo(gridPos);
			_itemComponents.Add(item);
		}
		
		public void RemoveItemAt(Vector3 worldPos) {
			var item = GetItemAt(worldPos);
			if ( item is { } ) {
				_itemComponents.Remove(item);
				Destroy(item.gameObject);
			}
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
	}
}
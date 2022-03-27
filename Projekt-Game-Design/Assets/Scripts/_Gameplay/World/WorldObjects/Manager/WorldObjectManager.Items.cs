using System.Collections.Generic;
using UnityEngine;

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
		private void AddItem() {
			CreateItem(defaultItemTypeSO);
		}

		private ItemComponent CreateItem(ItemTypeSO itemType) {
			ItemComponent.ItemData data = defaultItemTypeSO.ToComponentData();
			
			//todo refactor get next playerchar id
			data.Id = _itemComponents.Count + managerData.ItemDataList?.Count ?? 0;
			var itemComponent = CreateComponent<ItemComponent, ItemComponent.ItemData>(data, itemParent);
			_itemComponents.Add(itemComponent);
			return itemComponent;
		}
		
		public void AddItemAt(ItemTypeSO itemTypeSO, Vector3 worldPos) {
			var item = CreateItem(itemTypeSO);
			item.GridTransform.MoveTo(worldPos);
		}
		
		public void AddItemAt(ItemTypeSO itemTypeSO, Vector3Int gridPos) {
			var item = CreateItem(itemTypeSO);
			item.GridTransform.MoveTo(gridPos);
		}
	}
}
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
			ItemComponent.ItemData data = defaultItemTypeSO.ToComponentData();
			
			//todo refactor get next playerchar id
			data.Id = _itemComponents.Count + managerData.ItemDataList?.Count ?? 0;
			_itemComponents.Add(CreateComponent<ItemComponent, ItemComponent.ItemData>(data, itemParent));
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using GDP01._Gameplay.World.Character;
using UnityEngine;
using Util.Extensions;

//todo(refactor) rename -> ItemDict
/// <summary>
/// Item Container SO
/// List of each Item
/// </summary>
[CreateAssetMenu(fileName = "New ItemList", menuName = "Items/ItemList")]
public class ItemTypeContainerSO : ScriptableObject {
	[SerializeField] public List<ItemTypeSO> itemList = new List<ItemTypeSO>();
	[SerializeField] private ItemTypeSO defaultItemType;
	
///// Properties ///////////////////////////////////////////////////////////////////////////////////
	
	public ItemTypeSO Default => defaultItemType;

///// Public Functions /////////////////////////////////////////////////////////////////////////////

	public ItemTypeSO GetItemFromID(int id) {
		ItemTypeSO itemType = null;
		if ( itemList.IsValidIndex(id) ) {
			itemType = itemList[id];
		}

		return itemType;
	}

	public void UpdateItemList() {
		//todo remove magic
		for ( int i = 0; i < itemList.Count;) {
			if ( itemList[i] == null ) {
				itemList.RemoveAt(i);
			}
			else {
				itemList[i].id = i;
				i++;
			}
		}
	}

///// Unity Functions //////////////////////////////////////////////////////////////////////////////
	
	private void OnValidate() {
		UpdateItemList();
	}

	public ItemTypeSO GetItemTypeByGuid(string guid) {
		return itemList.FirstOrDefault(item => item.Guid == guid);
	}

	public ItemTypeSO GetItemTypeByName(string name) {
		return itemList.FirstOrDefault(item => item.name.Equals(name));
	}
}
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
public class ItemContainerSO : ScriptableObject {
	[SerializeField] public List<ItemSO> itemList = new List<ItemSO>();
	[SerializeField] private ItemSO _defaultItem;
	
///// Properties ///////////////////////////////////////////////////////////////////////////////////
	
	public ItemSO Default => _defaultItem;

///// Public Functions /////////////////////////////////////////////////////////////////////////////

	public ItemSO GetItemFromID(int id) {
		ItemSO item = null;
		if ( itemList.IsValidIndex(id) ) {
			item = itemList[id];
		}

		return item;
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

	public ItemSO GetItemTypeByGuid(string guid) {
		return itemList.FirstOrDefault(item => item.Guid == guid);
	}

	public ItemSO GetItemTypeByName(string name) {
		return itemList.FirstOrDefault(item => item.name.Equals(name));
	}
}
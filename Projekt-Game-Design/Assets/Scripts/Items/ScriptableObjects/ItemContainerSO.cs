using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//todo(refactor) rename -> ItemDict
/// <summary>
/// Item Container SO
/// List of each Item
/// </summary>
[CreateAssetMenu(fileName = "New ItemList", menuName = "Items/ItemList")]
public class ItemContainerSO : ScriptableObject {
	[SerializeField] public List<ItemSO> itemList = new List<ItemSO>();

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

	private void OnValidate() {
		// UpdateItemList();
	}
}
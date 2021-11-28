using System;
using UnityEngine;
using Util;

namespace Level.Grid.ItemGrid {
	[Serializable]
	public class ItemGrid : GenericGrid1D<Item> {

		private static Func<GenericGrid1D<Item>, int, int, Item> createGridObject = (grid, x, y) => new Item(); 
		
		public ItemGrid( int width, int height, float cellSize, Vector3 originPosition ) : 
			base( width, height, cellSize, originPosition, createGridObject, false) { }
	}

	[Serializable]
	public class Item {
		[HideInInspector] public new string name = "Item";
		[SerializeField] private int id = -1;

		public int ID {
			get => id;
		}

		public Item(int value) {
			SetId(value);
			name = $"Item {id}";
		}

		public Item() : this(-1) {}
		
		public void SetId(int value) {
			id = value;
			name = $"Item {id}";
		}
		
		// public ItemSO
		public bool Exists() {
			return id > -1;
		}
	}
}
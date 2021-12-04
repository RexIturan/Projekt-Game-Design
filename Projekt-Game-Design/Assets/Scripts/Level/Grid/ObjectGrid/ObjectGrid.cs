using System;
using Level.Grid.CharacterGrid;
using UnityEngine;
using Util;

namespace Level.Grid.ObjectGrid {
	[Serializable]
	public class ObjectGrid : GenericGrid1D<ObjectPlaceholder> {
		public ObjectGrid(int width, int height, float cellSize, Vector3 originPosition) : base(width, height, cellSize,
			originPosition, (grid, x, y) => new ObjectPlaceholder(), false) { }
	}

	[Serializable]
	public class ObjectPlaceholder {
		public int id;

		public void SetId(int newId) {
			this.id = newId;
		}
	}
}
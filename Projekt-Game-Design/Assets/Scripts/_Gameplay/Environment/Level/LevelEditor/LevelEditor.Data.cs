using System;
using Grid;
using UnityEngine;

namespace LevelEditor {
	public partial class LevelEditor {
		[Serializable]
		public class EditorState {
			public LevelEditor.LayerType layerType = LayerType.Tile;
			public LevelEditor.EditorMode editorMode = EditorMode.Select;
			public TileTypeSO selectedTileType;

			public override string ToString() {
				return $"layer: {layerType}, mode: {editorMode}, selectedTile: {selectedTileType} ";
			}
		}
		
		//todo(vincent) rename
		public enum EditorMode {
			None,
			Select,
			Paint,
			Box,
		}

		//todo rename
		public enum LayerType {
			None,
			Tile,
			Item,
			Character_Player,
			Character_Enemy,
			Door,
			Switch,
			Effect,
		}

	}
}
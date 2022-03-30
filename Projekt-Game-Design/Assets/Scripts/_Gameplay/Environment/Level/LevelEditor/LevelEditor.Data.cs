using System;
using GDP01._Gameplay.Provider;
using GDP01.TileEffects;
using Grid;
using WorldObjects;

namespace LevelEditor {
	public partial class LevelEditor {
		[Serializable]
		public class EditorState {
			public LevelEditor.LayerType layerType = LayerType.Tile;
			public LevelEditor.EditorMode editorMode = EditorMode.Select;
			public TileTypeSO selectedTileType;
			public PlayerTypeSO selectedPlayerType;
			public EnemyTypeSO selectedEnemyType;
			public ItemTypeSO selectedItemType;
			public DoorTypeSO selectedDoorType;
			public SwitchTypeSO selectedSwitchType;
			public int selectedEffectTypeId;

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
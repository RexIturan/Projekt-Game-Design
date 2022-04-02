using System.Collections.Generic;
using GDP01._Gameplay.Provider;
using UnityEngine;
using Util.Extensions;

namespace LevelEditor {
	public partial class LevelEditor {
		
		// private List<Vector3> _clickedAbovePos;
		// private List<Vector3> _clickedSelectedPos;
		
		private bool editing;
		private bool left;
		
		private Vector3 StartPos;
		private Vector3 StartAbovePos;
		private Vector3 EndPos;
		private Vector3 EndAbovePos;
		private Vector3 CurrentPos;
		private Vector3 CurrentAbovePos;

		private void ClearCachedClickedPositions() {
			
		}
		
		// private Vector3 FirstClickedAbovePos => GetClickedPos(true, 0);

		// private Vector3 GetClickedPos(bool above, int idx) {
		// 	Vector3 clickedPos = Vector3.zero;
		// 	if ( above ) {
		// 		
		// 		if ( _clickedAbovePos.IsValidIndex(idx) ) {
		// 			clickedPos = _clickedAbovePos[idx];
		// 		}
		// 		else {
		// 			Debug.LogError($"GetClickedPos at:{idx} in AbovePos not Found!");
		// 		}
		// 	}
		// 	else {
		// 		if ( _clickedSelectedPos.IsValidIndex(idx) ) {
		// 			clickedPos = _clickedSelectedPos[idx];
		// 		}
		// 		else {
		// 			Debug.LogError($"GetClickedPos at:{idx} in SelectedPos not Found!");
		// 		}
		// 	}
		//
		// 	return clickedPos;
		// }
		
		#region Add

		private void AddOne() {
			Debug.Log($"AddOne: {Layer} {Mode} selected {CurrentPos}, above {CurrentAbovePos}");

			switch ( Layer ) {
				case LayerType.Tile:
					AddTileAt(CurrentAbovePos);
					RedrawLevel();
					break;

				//PC
				case LayerType.Character_Player:
					GameplayProvider.Current.CharacterManager.AddPlayerCharacterAt(_editorState.selectedPlayerType, CurrentAbovePos);
					break;
				
				//EC
				case LayerType.Character_Enemy:
					GameplayProvider.Current.CharacterManager.AddEnemyCharacterAt(_editorState.selectedEnemyType, CurrentAbovePos);
					break;
				
				//item
				case LayerType.Item:
					GameplayProvider.Current.WorldObjectManager.AddItemAt(_editorState.selectedItemType, CurrentAbovePos);
					break;
				
				case LayerType.Door:
					GameplayProvider.Current.WorldObjectManager.AddDoorAt(_editorState.selectedDoorType, CurrentAbovePos);
					break;
				
				case LayerType.Switch:
					GameplayProvider.Current.WorldObjectManager.AddSwitchAt(_editorState.selectedSwitchType, CurrentAbovePos);
					break;
				
				case LayerType.Effect:
					GameplayProvider.Current.TileEffectManager.AddTileEffectAt(
						_editorState.selectedEffectTypeId, 
						gridDataSO.GetGridPos3DFromWorldPos(CurrentAbovePos));
					break;
			}

			ClearCachedClickedPositions();
			_leftClicked = false;
		}

		private void AddMany() {
			
			switch ( Layer ) {
				case LayerType.Tile:
					AddMultipleTilesAt(StartAbovePos, EndAbovePos);
					RedrawLevel();
					break;
			}
			
			cursorDrawer.HideCursor();
			ClearCachedClickedPositions();
			_leftClicked = false;
			_dragEnd = false;
		}

		#endregion

		#region Remove

		private void RemoveOne() {
			Debug.Log($"RemoveOne: {Layer} {Mode} selected {CurrentPos}, above {CurrentAbovePos}");
			
			switch ( Layer ) {
				case LayerType.Tile:
					GridController.RemoveTileAt(CurrentPos);
					//todo this should only be done if a tile is removed not everytime v
					RedrawLevel();
					break;
				
				//PC
				case LayerType.Character_Player:
					GameplayProvider.Current.CharacterManager.RemovePlayerCharacterAt(CurrentAbovePos);
					break;
				
				//EC
				case LayerType.Character_Enemy:
					GameplayProvider.Current.CharacterManager.RemoveEnemyCharacterAt(CurrentAbovePos);
					break;
				
				//item
				case LayerType.Item:
					GameplayProvider.Current.WorldObjectManager.RemoveItemAt(CurrentAbovePos);
					break;
				
				case LayerType.Door:
					GameplayProvider.Current.WorldObjectManager.RemoveDoorAt(CurrentAbovePos);
					break;
				
				case LayerType.Switch:
					GameplayProvider.Current.WorldObjectManager.RemoveSwitchAt(CurrentAbovePos);
					break;
				
				case LayerType.Effect:
					GameplayProvider.Current.TileEffectManager.RemoveTileEffectAt(
						gridDataSO.GetGridPos3DFromWorldPos(CurrentAbovePos));
					break;
			}

			ClearCachedClickedPositions();
			_rightClicked = false;
		}

		private void RemoveMany() {
			switch ( Layer ) {
				case LayerType.Tile:
					//todo remove 
					GridController.RemoveMultipleTilesAt(StartPos, EndPos);
					RedrawLevel();
					break;
			}

			//todo move to iunput cache??
			// reset input cache
			ClearCachedClickedPositions();
			_rightClicked = false;
			_dragEnd = false;
			cursorDrawer.HideCursor();
		}

		#endregion
		
		private void AddMultipleTilesAt(Vector3 clickPos, Vector3 dragPos) {
			GridController.AddMultipleTilesAt(clickPos, dragPos, _editorState.selectedTileType.id);
		}

		private void AddTileAt(Vector3 clickPos) {
			GridController.AddTileAt(clickPos, _editorState.selectedTileType);
		}
		
		public void ResetLevel() {
			GridController.ResetGrid();
			RedrawLevel();
		}
		
		
	}
}
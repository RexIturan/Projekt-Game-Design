using System.Collections.Generic;
using UnityEngine;
using Util.Extensions;

namespace LevelEditor {
	public partial class LevelEditor {
		
		private List<Vector3> _clickedAbovePos;
		private List<Vector3> _clickedSelectedPos;

		private void ClearCachedClickeDPositions() {
			_clickedAbovePos.Clear();
			_clickedSelectedPos.Clear();
		}
		
		private Vector3 FirstClickedAbovePos => GetClickedPos(true, 0);

		private Vector3 GetClickedPos(bool above, int idx) {
			Vector3 clickedPos = Vector3.zero;
			if ( above ) {
				if ( _clickedAbovePos.IsValidIndex(idx) ) {
					clickedPos = _clickedAbovePos[idx];
				}
				else {
					Debug.LogError($"GetClickedPos at:{idx} in AbovePos not Found!");
				}
			}
			else {
				if ( _clickedSelectedPos.IsValidIndex(idx) ) {
					clickedPos = _clickedSelectedPos[idx];
				}
				else {
					Debug.LogError($"GetClickedPos at:{idx} in SelectedPos not Found!");
				}
			}

			return clickedPos;
		}
		
		#region Add

		private void AddOne() {
			Debug.Log($"AddOne: {Layer} {Mode} {FirstClickedAbovePos}");

			switch ( Layer ) {
				case LayerType.Tile:
					AddTileAt(FirstClickedAbovePos);
					RedrawLevel();
					break;

				case LayerType.Item:
					//todo Add item at
					break;
			}

			ClearCachedClickeDPositions();
			_leftClicked = false;
		}

		private void AddMany() {
			
			switch ( Layer ) {
				case LayerType.Tile:
					AddMultipleTilesAt(GetClickedPos(true, 0), GetClickedPos(true, 1));
					RedrawLevel();
					break;
			}
			
			cursorDrawer.HideCursor();
			ClearCachedClickeDPositions();
			_leftClicked = false;
			_dragEnd = false;
		}

		#endregion

		#region Remove

		private void RemoveOne() {
			switch ( Layer ) {
				case LayerType.Tile:
					GridController.RemoveTileAt(GetClickedPos(true, 0));
					RedrawLevel();
					break;
				
				case LayerType.Item:
					
					break;
			}

			ClearCachedClickeDPositions();
			_rightClicked = false;
		}

		private void RemoveMany() {
			switch ( Layer ) {
				case LayerType.Tile:
					//todo remove 
					GridController.RemoveMultipleTilesAt(GetClickedPos(false, 0), GetClickedPos(false, 1));
					RedrawLevel();
					break;
			}

			//todo move to iunput cache??
			// reset input cache
			ClearCachedClickeDPositions();
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
using System;
using Characters;
using Events.ScriptableObjects;
using Grid;
using Input;
using LevelEditor;
using UnityEngine;
using Util;
using CursorMode = LevelEditor.CursorMode;

namespace Player {
	public class PlayerController : MonoBehaviour {
		[Header("Receiving events on")] [SerializeField]
		private PathNodeEventChannelSO targetTileEvent;

		[Header("Input")] 
		[SerializeField] private InputReader input;
		//todo used here??
		[SerializeField] private InputCache inputCache;
		[SerializeField] private GridDataSO gridData;

		[SerializeField] private CursorDrawer cursorDrawer;

		[SerializeField] private Selectable selectedPlayerCharacter;
		[SerializeField] private EnemyCharacterSC selectedEnemyCharacter;

		private CharacterList characterList; 
		
		private void Awake() {
			input.MouseClicked += ToggleIsSelected;
			targetTileEvent.OnEventRaised += TargetTile;
			
			//get character List
			characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
		}

		private void OnDisable() {
			input.MouseClicked -= ToggleIsSelected;
			targetTileEvent.OnEventRaised -= TargetTile;
		}

		private void Update() {
			//InputCache? || MousePosition
			//  get mouse pos in grid
			//  get mouse clicked (left, right)
			// write Pos && clickState into InputCache

			//Level Controller?
			//	- get grid data from tile
			//	- get grid data about whats on the tile

			inputCache.UpdateMouseInput(gridData);
			
			// Debug.Log($"inputCache.cursor.abovePos.gridPos\n{inputCache.cursor.abovePos.gridPos}");
			
			if ( gridData.IsIn3DGridBounds(inputCache.cursor.abovePos.gridPos) ) {
				Selectable selectable = GetPlayerAtPos(inputCache.cursor.abovePos.gridPos);
				
				if ( selectable ) {
					if (selectedPlayerCharacter && selectedPlayerCharacter.Equals(selectable)) {
						cursorDrawer.DrawCursorAt(inputCache.cursor.abovePos.tileCenter, CursorMode.Add);					
					}
					else {
						cursorDrawer.DrawCursorAt(inputCache.cursor.abovePos.tileCenter, CursorMode.Error);	
					}
				}
				else {
					cursorDrawer.DrawCursorAt(inputCache.cursor.abovePos.tileCenter, CursorMode.Select);
				}
				
				if ( inputCache.leftButton.started ) {
					if ( selectable ) {
						if (selectedPlayerCharacter) {
							selectedPlayerCharacter.Deselect();
						}
						selectedPlayerCharacter = selectable.gameObject.GetComponent<Selectable>();
						selectedPlayerCharacter.Select();
					}
				}
			}

			if ( inputCache.rightButton.started ) {
				if ( selectedPlayerCharacter ) {
					selectedPlayerCharacter.Deselect();
				}
			}
		}

		private void FixedUpdate() {
			//read from Input Cache

			// set selected
			// set target
		}

		private Selectable GetPlayerAtPos(Vector3Int gridPos) {
			
			foreach ( var playerCharObj in characterList.playerContainer ) {
				var gridTransform = playerCharObj.GetComponent<GridTransform>();
				if ( gridTransform.gridPosition.Equals(gridPos) ) {
					return playerCharObj.GetComponent<Selectable>();;
				}
			}
			return null;
		}
		
		private void SelectCharacter() {
			
		}

	// target Tile
		public void TargetTile(PathNode target) { }

		void ToggleIsSelected() {
			// if ( !abilitySelected && !abilityConfirmed ) {
			// 	Vector3 mousePos = Mouse.current.position.ReadValue();
			// 	Ray ray = Camera.main.ScreenPointToRay(mousePos);
			// 	RaycastHit rayHit;
			// 	if ( Physics.Raycast(ray, out rayHit, 100.0f) ) {
			// 		if ( rayHit.collider.gameObject == gameObject ) {
			// 			isSelected = !isSelected;
			// 		}
			// 	}
			// }
		}
	}
}
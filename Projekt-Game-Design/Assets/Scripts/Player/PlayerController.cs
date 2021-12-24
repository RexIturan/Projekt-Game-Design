using System;
using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Combat;
using Events.ScriptableObjects;
using Grid;
using Input;
using LevelEditor;
using UnityEngine;
using Util;
using CursorMode = LevelEditor.CursorMode;

namespace Player {
	public class PlayerController : MonoBehaviour {
		[Header("Receiving events on")]
		[SerializeField] private PathNodeEventChannelSO targetTileEvent;
		// when an ability is selected, depending on the ability, 
		// an event can tell the PlayerController when to recognize targets
		// if no ability is selected, there shouldn't be any targets taken on
		[SerializeField] private VoidEventChannelSO clearTargetCacheEvent;

		[Header("Input")] 
		[SerializeField] private InputReader input;
		//todo used here??
		[SerializeField] private InputCache inputCache;
		[SerializeField] private GridDataSO gridData;

		[SerializeField] private CursorDrawer cursorDrawer;

		[SerializeField] private Selectable selectedPlayerCharacter;
		[SerializeField] private EnemyCharacterSC selectedEnemyCharacter;
				
		[SerializeField] private Targetable target;

		private CharacterList characterList; 
		
		private void Awake() {
			input.MouseClicked += ToggleIsSelected;
			targetTileEvent.OnEventRaised += TargetTile;
			clearTargetCacheEvent.OnEventRaised += ClearTargetCache;
			
			//get character List
			characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
		}

		private void OnDisable() {
			input.MouseClicked -= ToggleIsSelected;
			targetTileEvent.OnEventRaised -= TargetTile;
			clearTargetCacheEvent.OnEventRaised -= ClearTargetCache;
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
				// selection
				//
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

				// targeting
				//
				if ( inputCache.leftButton.started ) {
		  		Attacker playerAttacker = selectedPlayerCharacter.gameObject.GetComponent<Attacker>();
					if(playerAttacker) {
						// targetable target
						Targetable newTarget = GetTargetAtPos(inputCache.cursor.abovePos.gridPos);
  					if(newTarget) { 
	  					target = newTarget;
							playerAttacker.SetTarget(newTarget);
						}

						// ground target
						playerAttacker.groundTargetSet = true;
						playerAttacker.SetGroundTarget(inputCache.cursor.abovePos.gridPos);
					}

					// movement target
					MovementController playerMovementController = selectedPlayerCharacter.gameObject.GetComponent<MovementController>();
					foreach(PathNode node in playerMovementController.reachableTiles) {
						if(node.pos.Equals(inputCache.cursor.abovePos.gridPos) )
						  playerMovementController.movementTarget = node;
					}
				}
			}

			if ( inputCache.rightButton.started ) {
				if ( selectedPlayerCharacter is { } ) {
					selectedPlayerCharacter.Deselect();
					selectedPlayerCharacter = null;
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
					return playerCharObj.GetComponent<Selectable>();
				}
			}
			return null;
		}
		
		// gets player or enemy character targetable component with grid position
		// todo: add world objects if they are targetable too!
		private Targetable GetTargetAtPos(Vector3Int gridPos) { 
			List<GameObject> targetableObjects = new List<GameObject>();
			targetableObjects.AddRange(characterList.playerContainer);
			targetableObjects.AddRange(characterList.enemyContainer);

		  foreach( GameObject charObj in targetableObjects)
			{
				Targetable objTarget = charObj.GetComponent<Targetable>();
				if( objTarget.GetGridPosition().Equals(gridPos))
					return objTarget;
			}
			return null;
    }

		private void SelectCharacter() {
			
		}

	// target Tile
		public void TargetTile(PathNode target) { }

		private void ClearTargetCache() {
			target = null;
		}

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
﻿using System;
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
using WorldObjects;
using CursorMode = LevelEditor.CursorMode;

namespace Player {
	public class PlayerController : MonoBehaviour {
		[Header("Receiving events on")]
		// when an ability is selected, depending on the ability, 
		// an event can tell the PlayerController when to recognize targets
		// if no ability is selected, there shouldn't be any targets taken on
		[SerializeField] private VoidEventChannelSO clearTargetCacheEvent;

		[SerializeField] private VoidEventChannelSO menuOpenedEvent;
		[SerializeField] private VoidEventChannelSO menuClosedEvent;
		[SerializeField] private VoidEventChannelSO abilitySelectedEvent;
		[SerializeField] private VoidEventChannelSO abilityUnselectedEvent;

		[Header("Mode Flags")]
		[SerializeField] private bool menuOpened;
		[SerializeField] private bool abilitySelected;

		[Header("Input")] 
		[SerializeField] private InputReader input;
		[SerializeField] private InputCache inputCache;
		[SerializeField] private GridDataSO gridData;

		[SerializeField] private CursorDrawer cursorDrawer;

		[SerializeField] private Selectable selectedPlayerCharacter;
				
		[SerializeField] private Targetable target;

		private CharacterList characterList; 
		private WorldObjectList worldObjectList;
		
		private void Awake() {
			abilitySelected = false;
			menuOpened = false;

			clearTargetCacheEvent.OnEventRaised += ClearTargetCache;

			menuOpenedEvent.OnEventRaised += HandleMenuOpened;
			menuClosedEvent.OnEventRaised += HandleMenuClosed;
			abilitySelectedEvent.OnEventRaised += HandleAbilitySelected;
			abilityUnselectedEvent.OnEventRaised += HandleAbilityDeselected;
			
			//get Lists
			characterList = CharacterList.FindInstant();
			worldObjectList = WorldObjectList.FindInstant();
		}

		private void OnDisable() {
			clearTargetCacheEvent.OnEventRaised -= ClearTargetCache;

			menuOpenedEvent.OnEventRaised -= HandleMenuOpened;
			menuClosedEvent.OnEventRaised -= HandleMenuClosed;
			abilitySelectedEvent.OnEventRaised -= HandleAbilitySelected;
			abilityUnselectedEvent.OnEventRaised -= HandleAbilityDeselected;
		}

		private void Update() {
			if(!menuOpened) { 
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
			  // player activation
				//
				if ( inputCache.leftButton.started )
					ActivatePlayerAtPos(inputCache.cursor.abovePos.gridPos);

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
				
				if ( inputCache.leftButton.started && !abilitySelected) {
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
				if ( inputCache.leftButton.started && selectedPlayerCharacter) {
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

			// deselecting
			//
			if ( inputCache.rightButton.started ) {
				if ( selectedPlayerCharacter is { } ) {
					selectedPlayerCharacter.Deselect();
					selectedPlayerCharacter = null;
				}
			}
			}
		}

		private void ActivatePlayerAtPos(Vector3Int gridPos) {
			List<PlayerCharacterSC> playersToActivate = new List<PlayerCharacterSC>();

			foreach ( var npcObj in characterList.friendlyContainer ) {
				PlayerCharacterSC playerSC = npcObj.GetComponent<PlayerCharacterSC>();
				if(playerSC && npcObj.GetComponent<GridTransform>().gridPosition.Equals(gridPos))
					playersToActivate.Add(playerSC);
			}

			foreach ( PlayerCharacterSC player in playersToActivate )
			  player.Activate();
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
			targetableObjects.AddRange(worldObjectList.doors);
			targetableObjects.AddRange(worldObjectList.junks);

		  foreach( GameObject targetObj in targetableObjects)
			{
				Targetable objTarget = targetObj.GetComponent<Targetable>();
				if( objTarget != null && objTarget.GetGridPosition().Equals(gridPos))
					return objTarget;
			}
			return null;
    }

		private void ClearTargetCache() {
			target = null;
		}

		private void HandleMenuOpened() {
			// Debug.Log("Menu opened. Disabling input. ");
			menuOpened = true;
		}

		private void HandleMenuClosed() {
			// Debug.Log("Menu closed. Enabling input. ");
			menuOpened = false;
		}

		private void HandleAbilitySelected() {
			abilitySelected = true;
		}

		private void HandleAbilityDeselected() {
			abilitySelected = false;
		}
	}
}
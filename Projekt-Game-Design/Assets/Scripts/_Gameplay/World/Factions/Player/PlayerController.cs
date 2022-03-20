using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Combat;
using GDP01.Characters.Component;
using GDP01.World.Components;
using Grid;
using Input;
using LevelEditor;
using Player;
using UnityEngine;
using Util;
using WorldObjects;

namespace GDP01.Player.Player {
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

		//drawer
		[SerializeField] private CursorDrawer cursorDrawer;

		[SerializeField] private Selectable selectedPlayerCharacter;
		[SerializeField] private Targetable target;

		// time to wait after ability selection to accept targeting input
		[SerializeField] private float abilityBlockingTime = 0.5f;

///// Private Variable
		private CharacterList characterList; 
		private WorldObjectList worldObjectList;
		
///// Properties

		private CharacterList CharList => characterList ??= CharacterList.FindInstant();
		public bool HasSelected => Selected != null;
		public Selectable Selected => selectedPlayerCharacter;

///// Private Functions

		private void ActivatePlayerAtPos(Vector3Int gridPos) {
			if ( CharList is null )
				return;
			
			List<PlayerCharacterSC> playersToActivate = new List<PlayerCharacterSC>();

			foreach ( var npcObj in CharList.friendlyContainer ) {
				PlayerCharacterSC playerSC = npcObj.GetComponent<PlayerCharacterSC>();
				if(playerSC && npcObj.GetComponent<GridTransform>().gridPosition.Equals(gridPos))
					playersToActivate.Add(playerSC);
			}

			foreach ( PlayerCharacterSC player in playersToActivate )
			  player.Activate();
		}

		private Selectable GetPlayerAtPos(Vector3Int gridPos) {
			if ( CharList is null )
				return null;
			
			foreach ( var playerCharObj in CharList.playerContainer ) {
				var gridTransform = playerCharObj.GetComponent<GridTransform>();
				if ( gridTransform.gridPosition.Equals(gridPos) ) {
					return playerCharObj.GetComponent<Selectable>();
				}
			}
			return null;
		}
		
		// gets player or enemy character targetable component with grid position
		private Targetable GetTargetAtPos(Vector3Int gridPos) {
			var targetableObjects = Targetable.GetAllInstances();

		  foreach( var targetable in targetableObjects)
			{
				if( targetable != null && targetable.GetGridPosition().Equals(gridPos))
					return targetable;
			}
			return null;
    }

		private void ClearTargetCache() {
			target = null;
		}

		
		//todo move to ui cache
		private void HandleMenuOpened() {
			Debug.Log("Menu opened. Disabling input. ");
			menuOpened = true;
		}

		private void HandleMenuClosed() {
			Debug.Log("Menu closed. Enabling input. ");
			menuOpened = false;
		}

		private void HandleAbilitySelected() {
			abilitySelected = true;
		}

		private void HandleAbilityDeselected() {
			abilitySelected = false;
		}

		public static PlayerController FindInstance() {
			return FindObjectOfType<PlayerController>();
		}
		
///// Public Functions

		public void DeselectSelectedCharacter() {
			selectedPlayerCharacter.Deselect();
		}

		public void SelectSelectedCharacter() {
			selectedPlayerCharacter.Select();
		}
		
		///// Monobehaviour Functions
	
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
			if ( inputCache.IsMouseOverUI ) return;
			//todo(vincent) i maybe destroyed this
			if ( menuOpened ) return;
			
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
				if ( inputCache.leftButton.started )
					ActivatePlayerAtPos(inputCache.cursor.abovePos.gridPos);

				// selection
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
							DeselectSelectedCharacter();
						}
						selectedPlayerCharacter = selectable.gameObject.GetComponent<Selectable>();
						SelectSelectedCharacter();
						selectedPlayerCharacter.Select();
					}
				}

				// targeting
				//
				if ( inputCache.leftButton.started && selectedPlayerCharacter && 
						selectedPlayerCharacter.GetComponent<Timer>().timeSinceTransition > abilityBlockingTime) {
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

					// if there is only one proper target, choose it as the target
					AbilityController abilityController = playerAttacker.GetComponent<AbilityController>();
					if(abilityController.singleTarget) {
						playerAttacker.SetTarget(Targetable.GetTargetsWithPosition(abilityController.singleTargetPos));
						playerAttacker.SetGroundTarget(abilityController.singleTargetPos);
					}
				}
			}

			// deselecting
			//
			if ( inputCache.rightButton.started && selectedPlayerCharacter ) {
				AbilityController abilityController = selectedPlayerCharacter.GetComponent<AbilityController>();
				if ( abilityController.abilitySelected ) {
					abilityController.abilitySelected = false;
					abilityController.SelectedAbilityID = -1;
					abilityController.LastSelectedAbilityID = -1;
				}
				else {
					selectedPlayerCharacter.Deselect();
					selectedPlayerCharacter = null;
				}
			}
		}
	}
}
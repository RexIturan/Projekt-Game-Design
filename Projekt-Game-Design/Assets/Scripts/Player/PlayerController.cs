using System;
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

		private void Awake() {
			input.MouseClicked += ToggleIsSelected;
			targetTileEvent.OnEventRaised += TargetTile;
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

			// select Selectable?? XD
			var tilePos = MousePosition.GetTilePositionFromMousePosition(gridData, true, out var hitBottom);
			cursorDrawer.DrawCursorAt(tilePos, CursorMode.Select);
		}

		private void FixedUpdate() {
			//read from Input Cache

			// set selected
			// set target
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
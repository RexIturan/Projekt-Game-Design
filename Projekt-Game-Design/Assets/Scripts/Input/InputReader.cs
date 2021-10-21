﻿using System;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input {
	[CreateAssetMenu(fileName = "InputReader", menuName = "Input/Input Reader", order = 0)]
	public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenuActions,
		GameInput.ICameraActions, GameInput.ILevelEditorActions, GameInput.IPathfindingDebugActions,
		GameInput.IInventoryActions, GameInput.ILoadingScreenActions {
		// todo rework event channels: move and rename them 
		[Header("Sending Events On")] [SerializeField]
		private BoolEventChannelSO visibilityMenu;

		[SerializeField] private BoolEventChannelSO visibilityInventory;
		[SerializeField] private BoolEventChannelSO visibilityGameOverlay;
		[SerializeField] private EFactionEventChannelSO endTurnEC;

		[Header("Receiving Events On")] [SerializeField]
		private VoidEventChannelSO enableMenuInput;

		[SerializeField] private VoidEventChannelSO enableGameplayInput;
		[SerializeField] private VoidEventChannelSO enableInventoryInput;
		[SerializeField] private VoidEventChannelSO enableLoadingScreenInput;

		// Gameplay
		// public event UnityAction inventoryEvent = delegate { };
		// public event UnityAction menuEvent = delegate { };
		public event UnityAction EndTurnEvent = delegate { };

		public event UnityAction MouseClicked = delegate { };

		//Camera 
		public event UnityAction<Vector2, bool> CameraMoveEvent = delegate { };
		public event UnityAction<float> CameraRotateEvent = delegate { };
		public event UnityAction<float> CameraZoomEvent = delegate { };

		// Mouse Testing
		public event UnityAction LeftClickEvent = delegate { };
		public event UnityAction RightClickEvent = delegate { };

		//Pathfinding Debug
		public event UnityAction StepEvent = delegate { };
		public event UnityAction ShowFullPathEvent = delegate { };

		//LoadingScreen Input
		public event Action AnyKeyEvent = delegate { };

		private GameInput _gameInput;

		// todo idk if we should expose _gameInput 
		public GameInput GameInput => _gameInput;

		private void OnEnable() {
			enableMenuInput.OnEventRaised += EnableMenuInput;
			enableGameplayInput.OnEventRaised += EnableGameplayInput;
			enableInventoryInput.OnEventRaised += EnableInventoryInput;
			enableLoadingScreenInput.OnEventRaised += EnableLoadingScreenInput;

			if ( _gameInput == null ) {
				_gameInput = new GameInput();
				_gameInput.Gameplay.SetCallbacks(this);
				_gameInput.Camera.SetCallbacks(this);
				_gameInput.Menu.SetCallbacks(this);
				_gameInput.LevelEditor.SetCallbacks(this);
				_gameInput.PathfindingDebug.SetCallbacks(this);
				_gameInput.Inventory.SetCallbacks(this);
				_gameInput.LoadingScreen.SetCallbacks(this);
			}

			// todo idk what the default input should be
			// EnableGameplayInput();
		}

		private void OnDisable() {
			enableMenuInput.OnEventRaised -= EnableMenuInput;
			enableGameplayInput.OnEventRaised -= EnableGameplayInput;
			enableInventoryInput.OnEventRaised -= EnableInventoryInput;
			enableLoadingScreenInput.OnEventRaised -= EnableLoadingScreenInput;

			DisableAllInput();
		}

		public void EnableLevelEditorInput() {
			_gameInput.Menu.Disable();

			_gameInput.PathfindingDebug.Enable();
			_gameInput.LevelEditor.Enable();
			_gameInput.Camera.Enable();
		}

		public void EnableGameplayInput() {
			// Debug.Log("enable Gameplay Input");
			_gameInput.LoadingScreen.Disable();
			_gameInput.Menu.Disable();

			_gameInput.PathfindingDebug.Enable();
			_gameInput.Gameplay.Enable();
			_gameInput.Camera.Enable();
		}

		// Für das Inventar
		public void EnableInventoryInput() {
			_gameInput.Menu.Disable();
			_gameInput.PathfindingDebug.Disable();
			_gameInput.Gameplay.Disable();
			_gameInput.Camera.Disable();
			_gameInput.LoadingScreen.Disable();

			_gameInput.Inventory.Enable();
		}

		public void EnableDebugInput() {
			_gameInput.PathfindingDebug.Enable();
		}

		public void EnableMenuInput() {
			_gameInput.Gameplay.Disable();
			_gameInput.Camera.Disable();
			_gameInput.PathfindingDebug.Disable();
			_gameInput.LoadingScreen.Disable();

			_gameInput.Menu.Enable();
		}

		public void EnableLoadingScreenInput() {
			_gameInput.Gameplay.Disable();
			_gameInput.Camera.Disable();
			_gameInput.PathfindingDebug.Disable();

			_gameInput.LoadingScreen.Enable();
		}

		public void DisableAllInput() {
			_gameInput.Gameplay.Disable();
			_gameInput.LevelEditor.Disable();
			_gameInput.Camera.Disable();
			_gameInput.Menu.Disable();
			_gameInput.PathfindingDebug.Disable();
			_gameInput.LoadingScreen.Disable();
		}

		#region Gameplay

		public void OnEndTurn(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed ) {
				EndTurnEvent.Invoke();
				endTurnEC.RaiseEvent(Faction.Player);
			}
		}

		public void OnMenu(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed ) {
				visibilityMenu.RaiseEvent(true);
			}
		}

		public void OnInventory(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed ) {
				visibilityInventory.RaiseEvent(true);
			}
		}

		public void OnMouseClicked(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed )
				MouseClicked.Invoke();
		}

		#endregion

		#region Camera

		public void OnMoveCamera(InputAction.CallbackContext context) {
			// todo mouse input
			var input = context.ReadValue<Vector2>();
			CameraMoveEvent.Invoke(input, false);
		}

		public void OnRotateCamera(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed ) {
				CameraRotateEvent.Invoke(context.ReadValue<float>());
			}
		}

		public void OnCameraZoom(InputAction.CallbackContext context) {
			CameraZoomEvent.Invoke(context.ReadValue<float>());
		}

		#endregion

		#region Menu

		public void OnConfirm(InputAction.CallbackContext context) {
			// TODO Implement
		}

		public void OnCancel(InputAction.CallbackContext context) {
			// TODO menuCancelEvent
			if ( context.phase == InputActionPhase.Performed ) {
				visibilityGameOverlay.RaiseEvent(true);
				visibilityMenu.RaiseEvent(false);
			}
		}

		#endregion

		#region Inventory

		public void OnCancelInventory(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed ) {
				Debug.Log("onCancel");
				visibilityGameOverlay.RaiseEvent(true);
			}
		}

		#endregion

		#region Pathfinding

		public void OnToggle(InputAction.CallbackContext context) {
			// TODO Implement should toggle pathfinding preview
		}

		public void OnStep(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed )
				StepEvent.Invoke();
		}

		public void OnShowCompletePath(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed )
				ShowFullPathEvent.Invoke();
		}

		#endregion

		#region Mouse

		public void OnLeftMouseClick(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed )
				LeftClickEvent.Invoke();
		}

		public void OnRightMouseClick(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed )
				RightClickEvent.Invoke();
		}

		#endregion

		#region LoadingScreen Input

		public void OnContinue(InputAction.CallbackContext context) {
			if ( context.phase == InputActionPhase.Performed ) {
				AnyKeyEvent.Invoke();
				Debug.Log("Any Key");
			}
		}

		#endregion
	}
}
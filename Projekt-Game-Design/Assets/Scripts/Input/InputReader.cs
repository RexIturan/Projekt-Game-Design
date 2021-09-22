using System;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input {
    [CreateAssetMenu(fileName = "InputReader", menuName = "Input/Input Reader", order = 0)]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenuActions, GameInput.ICameraActions, GameInput.ILevelEditorActions, GameInput.IPathfindingDebugActions, GameInput.IInventoryActions {
        
        [Header("Sending Events On")]
        [SerializeField] private BoolEventChannelSO visibilityMenu;
        [SerializeField] private BoolEventChannelSO visibilityInventory;
        [SerializeField] private BoolEventChannelSO visibilityGameOverlay;
        
        [Header("Receiving Events On")]
        [SerializeField] private VoidEventChannelSO enableMenuInput;
        [SerializeField] private VoidEventChannelSO enableGamplayInput;
        [SerializeField] private VoidEventChannelSO enableInventoryInput;
        
        // Gameplay
        public event UnityAction inventoryEvent = delegate { };
        public event UnityAction menuEvent = delegate { };
        public event UnityAction endTurnEvent = delegate { };
        
        //Camera 
        public event UnityAction<Vector2, bool> cameraMoveEvent = delegate {  };
        public event UnityAction<float> cameraRotateEvent = delegate {  };
        public event UnityAction<float> cameraZoomEvent = delegate {  };

        // Mouse Testing
        public event UnityAction leftClickEvent = delegate { };
        public event UnityAction rightClickEvent = delegate { };

        //Pathfinding Debug
        public event UnityAction stepEvent = delegate { };
        public event UnityAction showFullPathEvent = delegate { };
        
        private GameInput gameInput;

        public GameInput GameInput => gameInput;

        private void OnEnable() {

            enableMenuInput.OnEventRaised += EnableMenuInput;
            enableGamplayInput.OnEventRaised += EnableGameplayInput;
            enableInventoryInput.OnEventRaised += EnableInventoryInput;
            
            if (gameInput == null) {
                gameInput = new GameInput();
                gameInput.Gameplay.SetCallbacks(this);
                gameInput.Camera.SetCallbacks(this);
                gameInput.Menu.SetCallbacks(this);
                gameInput.LevelEditor.SetCallbacks(this);
                gameInput.PathfindingDebug.SetCallbacks(this);
                gameInput.Inventory.SetCallbacks(this);
            }
            EnableGameplayInput();
        }

        private void OnDisable() {
            DisableAllInput();
        }
        
        public void EnableLevelEditorInput()
        {
            gameInput.Menu.Disable();
            gameInput.PathfindingDebug.Enable();

            gameInput.LevelEditor.Enable();
            gameInput.Camera.Enable();
        }
        
        public void EnableGameplayInput()
        {
            gameInput.Menu.Disable();
            gameInput.PathfindingDebug.Enable();

            gameInput.Gameplay.Enable();
            gameInput.Camera.Enable();
        }
        // Für das Inventar
        public void EnableInventoryInput()
        {
            gameInput.Menu.Disable();
            gameInput.PathfindingDebug.Disable();
            gameInput.Gameplay.Disable();
            gameInput.Camera.Disable();
            gameInput.Inventory.Enable();
        }
        
        public void EnableDebugInput()
        {
            gameInput.PathfindingDebug.Enable();
        }
        
        public void EnableMenuInput()
        {
            gameInput.Gameplay.Disable();
            gameInput.Camera.Disable();
            gameInput.PathfindingDebug.Disable();
            
            gameInput.Menu.Enable();
        }
        
        public void DisableAllInput()
        {
            gameInput.Gameplay.Disable();
            gameInput.LevelEditor.Disable();
            gameInput.Camera.Disable();
            gameInput.Menu.Disable();
            gameInput.PathfindingDebug.Disable();
        }

        #region Gameplay
        
        public void OnEndTurn(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed)
                endTurnEvent.Invoke();
        }

        public void OnMenu(InputAction.CallbackContext context) {

            if (context.phase == InputActionPhase.Performed) {
                Debug.Log("onMenu");
                visibilityMenu.RaiseEvent(true);
            }
        }

        public void OnInventory(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                Debug.Log("onInventory");
                visibilityInventory.RaiseEvent(true);
            }
        }

        #endregion

        #region Camera

        public void OnMoveCamera(InputAction.CallbackContext context) {
            // todo mouse input
            var input = context.ReadValue<Vector2>();
            cameraMoveEvent.Invoke(input, false);
        }

        public void OnRotateCamera(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed)
            {
                cameraRotateEvent.Invoke(context.ReadValue<float>());
            }
        }

        public void OnCameraZoom(InputAction.CallbackContext context) {
            cameraZoomEvent.Invoke(context.ReadValue<float>());
        }

        #endregion

        #region Menu

        public void OnConfirm(InputAction.CallbackContext context) {
        }

        public void OnCancel(InputAction.CallbackContext context) {
            // TODO menuCancelEvent
            if (context.phase == InputActionPhase.Performed) {
                Debug.Log("onCancel");
                visibilityGameOverlay.RaiseEvent(true);
            }
        }

        #endregion
        
        #region Inventory

        public void OnCancelInventory(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                Debug.Log("onCancel");
                visibilityGameOverlay.RaiseEvent(true);
            }
        }

        #endregion

        #region Pathfinding


        public void OnToggle(InputAction.CallbackContext context) {
        }

        public void OnStep(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed)
                stepEvent.Invoke();
        }

        public void OnShowCompletePath(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed)
                showFullPathEvent.Invoke();
        }        

        #endregion
        
        #region Mouse

        public void OnLeftMouseClick(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed)
                leftClickEvent.Invoke();
        }

        public void OnRightMouseClick(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed)
                rightClickEvent.Invoke();
        }

        #endregion
    }
}
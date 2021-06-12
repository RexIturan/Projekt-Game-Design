using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input {
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader", order = 0)]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenusActions, GameInput.ITestingActions, GameInput.IPathfindingDebugActions {
        
        // Gameplay
        public event UnityAction attackEvent = delegate { };
        public event UnityAction pauseEvent = delegate { };
        public event UnityAction endTurnEvent = delegate { };
        public event UnityAction<Vector2> moveEvent = delegate { };
        public event UnityAction<Vector2, bool> cameraMoveEvent = delegate {  };
        public event UnityAction<float> cameraRotateEvent = delegate {  };
        public event UnityAction<float> cameraZoomEvent = delegate {  };

        public event UnityAction leftClickEvent = delegate { };
        public event UnityAction rightClickEvent = delegate { };
        
        public event UnityAction stepEvent = delegate { };
        public event UnityAction showFullPathEvent = delegate { };
        

        private GameInput gameInput;

        private void OnEnable() {
            if (gameInput == null) {
                gameInput = new GameInput();
                gameInput.Gameplay.SetCallbacks(this);
                gameInput.Menus.SetCallbacks(this);
                gameInput.Testing.SetCallbacks(this);
                gameInput.PathfindingDebug.SetCallbacks(this);
            }
            EnableGameplayInput();
        }

        private void OnDisable() {
            DisableAllInput();
        }
        
        public void EnableGameplayInput()
        {
            gameInput.Menus.Disable();
            
            gameInput.PathfindingDebug.Enable();
            gameInput.Testing.Enable();

            gameInput.Gameplay.Enable();
        }
        
        public void EnableDebugInput()
        {
            gameInput.PathfindingDebug.Enable();
        }
        
        public void EnableMenuInput()
        {
            gameInput.Gameplay.Disable();
            gameInput.Testing.Disable();
            gameInput.PathfindingDebug.Disable();
            
            gameInput.Menus.Enable();
        }
        
        public void DisableAllInput()
        {
            gameInput.Gameplay.Disable();
            gameInput.Menus.Disable();
            gameInput.Testing.Disable();
            gameInput.PathfindingDebug.Disable();
            // gameInput.Gameplay.Disable();
        }

        public void OnMove(InputAction.CallbackContext context) {
        }

        public void OnAttack(InputAction.CallbackContext context) {
        }

        public void OnEndTurn(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed)
                endTurnEvent.Invoke();
        }

        public void OnMoveCamera(InputAction.CallbackContext context) {
            // todo mouse input
            var input = context.ReadValue<Vector2>();
            cameraMoveEvent.Invoke(input, false);
        }

        public void OnRotateCamera(InputAction.CallbackContext context) {
            cameraRotateEvent.Invoke(context.ReadValue<float>());
        }

        public void OnCameraZoom(InputAction.CallbackContext context) {
            cameraZoomEvent.Invoke(context.ReadValue<float>());
        }

        public void OnConfirm(InputAction.CallbackContext context) {
        }

        public void OnCancel(InputAction.CallbackContext context) {
        }

        public void OnLeftMouseClick(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed)
                leftClickEvent.Invoke();
        }

        public void OnRightMouseClick(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed)
                rightClickEvent.Invoke();
        }

        public void OnMousePosition(InputAction.CallbackContext context) {
        }

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
    }
}
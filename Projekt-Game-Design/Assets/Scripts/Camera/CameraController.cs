using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using GDP01.Util.ScriptableObjects;

namespace DefaultNamespace.Camera {
    public class CameraController : MonoBehaviour {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private FloatReference movementSpeed;
        [SerializeField] private FloatReference rotateSpeed;
        [SerializeField] private FloatReference zoomSpeed;
        [SerializeField] private FloatReference movementTime;
        [SerializeField] private Vector3 newPosition;
        [SerializeField] private float panSpeed;
        [SerializeField] private int panBorderThickness;
        [SerializeField] private int minZoom;
        [SerializeField] private int maxZoom;

        public Transform cameraTransform;
        private Vector2 inputVector;
        private float rotateInputVector;
        private float zoomInput;
        
        private void Start() {
            newPosition = transform.position;
        }

        private void OnEnable() {
            // bind events
            inputReader.cameraMoveEvent += HandleCameraMoveEvent;
            inputReader.cameraRotateEvent += HandleCameraRotateEvent;
            inputReader.cameraZoomEvent += HandleCameraZoomEvent;
        }

        private void OnDisable() {
            // unbind events
            inputReader.cameraMoveEvent -= HandleCameraMoveEvent;
            inputReader.cameraRotateEvent -= HandleCameraRotateEvent;
            inputReader.cameraZoomEvent -= HandleCameraZoomEvent;
        }

        private void Update() {

            if (inputVector.y != 0 || inputVector.x != 0)
            {
                transform.position += (transform.forward * inputVector.y + transform.right * inputVector.x) * (movementSpeed.Value * Time.deltaTime);
            }
            else
            {
                // Für die Kamerabewegung durch den Bildschirmrand
                HandleCameraBorder();
            }
            
            //transform.rotation *= Quaternion.Euler(0, rotateInputVector * -1, 0);
            //transform.rotation *= Quaternion.Euler(0, rotateInputVector * -45, 0);

            var pos = new Vector3(
                
                0,
                // 0,
                -zoomInput * Time.deltaTime * zoomSpeed.Value,
                // zoomInput);
                zoomInput * Time.deltaTime * zoomSpeed.Value);

            // Test ob die Position außerhalb des zulässigen Bereiches ist
            if (pos.y + cameraTransform.localPosition.y <= maxZoom && pos.y + cameraTransform.localPosition.y >= minZoom)
            {
                cameraTransform.localPosition += pos;
            }
        }

        private void HandleCameraBorder()
        {
            Vector3 pos = transform.position;

            Vector2 mousePos =Mouse.current.position.ReadValue();

            bool outOfScreen = mousePos.y > Screen.height || mousePos.y < 0 || mousePos.x > Screen.width ||
                               mousePos.x < 0;

            if (mousePos.y >= Screen.height - panBorderThickness && !outOfScreen)
            {
                pos += transform.forward * (panSpeed * Time.deltaTime);
            }

            if (mousePos.x <= panBorderThickness && !outOfScreen)
            {
                pos -= transform.right * (panSpeed * Time.deltaTime);
            }

            if (mousePos.y <= panBorderThickness && !outOfScreen)
            {
                pos -= transform.forward * (panSpeed * Time.deltaTime);
            }

            if (mousePos.x >= Screen.width - panBorderThickness && !outOfScreen)
            {
                pos += transform.right * (panSpeed * Time.deltaTime);
            }
            
            //TODO: World Bounds müssen definiert werden
            //pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
            //pos.z = Mathf.Clamp(pos.z, -zLimit, zLimit);

            transform.position = pos;
        }

        private void HandleCameraMoveEvent(Vector2 movement, bool useMouse) {
            inputVector = movement;
        }

        private void HandleCameraRotateEvent(float rotate) {
            //rotateInputVector = rotate;
            //transform.rotation *= Quaternion.Euler(0, rotateInputVector * -1, 0);
            transform.rotation *= Quaternion.Euler(0, 45 * -rotate, 0);
        }
        
        private void HandleCameraZoomEvent(float zoom) {
            zoomInput = zoom;
        }
    }
}
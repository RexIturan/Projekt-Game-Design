using System;
using Input;
using UnityEngine;
using Util.ScriptableObjects;

namespace DefaultNamespace.Camera {
    public class CameraController : MonoBehaviour {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private FloatReference movementSpeed;
        [SerializeField] private FloatReference rotateSpeed;
        [SerializeField] private FloatReference zoomSpeed;
        [SerializeField] private FloatReference movementTime;
        [SerializeField] private Vector3 newPosition;

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
            transform.position += (transform.forward * inputVector.y + transform.right * inputVector.x) * (movementSpeed.Value * Time.deltaTime);
            transform.rotation *= Quaternion.Euler(0, rotateInputVector * -1, 0);

            var pos = new Vector3(
                
                0,
                // 0,
                -zoomInput * Time.deltaTime * zoomSpeed.Value,
                // zoomInput);
                zoomInput * Time.deltaTime * zoomSpeed.Value);
            cameraTransform.localPosition += pos;
        }

        private void HandleCameraMoveEvent(Vector2 movement, bool useMouse) {
            inputVector = movement;
        }

        private void HandleCameraRotateEvent(float rotate) {
            rotateInputVector = rotate;
        }
        
        private void HandleCameraZoomEvent(float zoom) {
            zoomInput = zoom;
        }
    }
}
﻿using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Util.ScriptableObjects;
using Events.ScriptableObjects;
using System;

namespace DefaultNamespace.Camera {
    public class CameraController : MonoBehaviour {
				[SerializeField] private Vector3EventChannelSO panCameraEC;
        [SerializeField] private InputReader inputReader;
        [SerializeField] private FloatReference movementSpeed;
        [SerializeField] private FloatReference zoomSpeed;
        [SerializeField] private float panSpeed;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private int panBorderThickness;
        [SerializeField] private int minZoom;
        [SerializeField] private int maxZoom;

        [SerializeField] private bool edgeScroll;
        
        public Transform cameraTransform;
        private Vector2 _inputVector;
        private float _zoomInput;
        private Quaternion target = Quaternion.Euler(0, 0, 0);
        
        private void OnEnable()
        {
	        // bind events
					panCameraEC.OnEventRaised += PanCamera;
	        inputReader.CameraMoveEvent += HandleCameraMoveEvent;
	        inputReader.CameraRotateEvent += HandleCameraRotateEvent;
	        inputReader.CameraZoomEvent += HandleCameraZoomEvent;

        }

        private void OnDisable() {
						// unbind events
						panCameraEC.OnEventRaised -= PanCamera;
						inputReader.CameraMoveEvent -= HandleCameraMoveEvent;
            inputReader.CameraRotateEvent -= HandleCameraRotateEvent;
            inputReader.CameraZoomEvent -= HandleCameraZoomEvent;
        }

        private void Update() {

            if (_inputVector.y != 0 || _inputVector.x != 0) {
	            var camTransform = transform; 
	            camTransform.position += (camTransform.forward * _inputVector.y + camTransform.right * _inputVector.x) * (movementSpeed.Value * Time.deltaTime);
            }
            else
            {
                // Für die Kamerabewegung durch den Bildschirmrand
                if (edgeScroll) {
                    HandleCameraBorder();    
                }
            }

            var pos = new Vector3(
                
                0,
                // 0,
                -_zoomInput * Time.deltaTime * zoomSpeed.Value,
                // zoomInput);
                _zoomInput * Time.deltaTime * zoomSpeed.Value);

            // Test ob die Position außerhalb des zulässigen Bereiches ist
            // if (edgeScroll) {
                if (pos.y + cameraTransform.localPosition.y <= maxZoom && pos.y + cameraTransform.localPosition.y >= minZoom)
                {
                    cameraTransform.localPosition += pos;
                }    
            // }
            
	            transform.rotation =
		            Quaternion.Slerp(transform.rotation, target, rotateSpeed * Time.deltaTime);
            
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
            _inputVector = movement;
        }

        private void HandleCameraRotateEvent(float rotate)
        {
          target *= Quaternion.Euler(0, 45 * -rotate, 0);
        }
        
        private void HandleCameraZoomEvent(float zoom) {
            _zoomInput = zoom;
        }

				private void PanCamera(Vector3 worldPos) {
						transform.position = new Vector3(worldPos.x, transform.position.y, worldPos.z);
				}
    }
}
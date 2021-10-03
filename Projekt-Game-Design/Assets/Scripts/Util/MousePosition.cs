using UnityEngine;
using UnityEngine.InputSystem;

namespace Util {
    public class MousePosition {

        public static Vector3 GetMouseWorldPosition() {
            
            Plane plane = new Plane(Vector3.up, 0);
            Vector3 worldPosition = new Vector3();
            float distance;
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }

            return worldPosition;
        }
        
        public static Vector3 GetMouseWorldPosition(Vector3 normal, float dist) {
            
            Plane plane = new Plane(normal, -dist);
            Vector3 worldPosition = new Vector3();
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (plane.Raycast(ray, out var distance))
            {
                worldPosition = ray.GetPoint(distance);
            }

            return worldPosition;
        }
        
    }
}
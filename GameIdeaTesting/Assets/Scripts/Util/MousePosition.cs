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
        
    }
}
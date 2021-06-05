using UnityEngine;

namespace Util {
    public class MousePosition {

        public static Vector3 GetMouseWorldPosition() {
            
            Plane plane = new Plane(Vector3.up, 0);
            Vector3 worldPosition = new Vector3();
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }

            return worldPosition;
        }
        
    }
}
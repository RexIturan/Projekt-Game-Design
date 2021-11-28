using Grid;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Util {
    public static class MousePosition {

        public static Vector3 GetMouseWorldPosition() {
            
            Plane plane = new Plane(Vector3.up, 0);
            Vector3 worldPosition = new Vector3();
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (plane.Raycast(ray, out float distance))
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

        public static Vector3 GetTilePositionFromMousePosition(GridDataSO gridData, bool above) {
	        // raycast against tile Mesh layer
	        // raycast against bottom
	        Plane bottomPlane = new Plane(Vector3.up, Vector3.zero);
	        Vector3 worldPosition = new Vector3();
	        float maxDistance = 1000;
	        int layer_mask = LayerMask.GetMask("Terrain");
	        
	        if ( Camera.main is { } ) {
		        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		        
		        if ( Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, maxDistance, layer_mask) ) {
			        var hitPos = hit.point;
			        Vector3 pos;
			        if ( above ) {
				        pos = hitPos + hit.normal * ( gridData.CellSize * 0.5f );  
			        }
			        else {
				        pos = hitPos - hit.normal * ( gridData.CellSize * 0.5f );
			        }
			        
			        worldPosition = gridData.GetGridPosWithoutOffsetFromWorldPos(pos );
			        
			        Debug.DrawLine(ray.origin, hitPos, Color.yellow, 1);
			        Debug.DrawLine(hitPos, pos, Color.blue, 1);
			        // DebugDrawRaycastHit(gridPos, Color.green);
		        }
		        else if(bottomPlane.Raycast(ray, out var distance)) {
			        var hitPos = ray.GetPoint(distance);
			        worldPosition = gridData.GetGridPosWithoutOffsetFromWorldPos(hitPos);
		        }
	        }
	        
	        DebugDrawRaycastHit(worldPosition + gridData.GetCellCenter(), Color.red);
	        
	        return worldPosition;
        }

        private static void DebugDrawRaycastHit(Vector3 pos, Color color) {
	        var duration = 1;
	        Debug.DrawLine(pos + new Vector3(0, -0.2f, 0), pos + new Vector3(0, 0.2f, 0), color, duration);
	        Debug.DrawLine(pos + new Vector3(-0.2f, 0, 0), pos + new Vector3(0.2f, 0, 0), color, duration);
	        Debug.DrawLine(pos + new Vector3(0, 0, -0.2f), pos + new Vector3(0, 0, 0.2f), color, duration);
        }
    }
}
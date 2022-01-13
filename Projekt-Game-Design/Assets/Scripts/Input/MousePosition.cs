using Grid;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Util {
	public static class MousePosition {
		public static Vector3 GetMouseWorldPosition() {
			Plane plane = new Plane(Vector3.up, 0);
			Vector3 worldPosition = new Vector3();
			// Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if ( Camera.main is { } ) {
				Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
				if ( plane.Raycast(ray, out float distance) ) {
					worldPosition = ray.GetPoint(distance);
				}
			}

			return worldPosition;
		}

		public static Vector3 GetMouseWorldPosition(Vector3 normal, float dist) {
			Plane plane = new Plane(normal, -dist);
			Vector3 worldPosition = new Vector3();
			// Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if ( Camera.main is { } ) {
				Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
				if ( plane.Raycast(ray, out var distance) ) {
					worldPosition = ray.GetPoint(distance);
				}
			}

			return worldPosition;
		}

		public static Vector3 GetTilePos(GridDataSO gridData, bool above, out bool hitBottom, bool showCenterPos = false, bool showMouseHit = false) {
			// raycast against tile Mesh layer
			// raycast against bottom
			Plane bottomPlane = new Plane(Vector3.up, Vector3.zero);
			Vector3 worldPosition = new Vector3();
			float maxDistance = 1000;
			int layer_mask = LayerMask.GetMask("Terrain");
			hitBottom = false;

			if ( Camera.main is { } ) {
				Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

				if ( Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, maxDistance,
					layer_mask) ) {
					var hitPos = hit.point;
					Vector3 pos;
					if ( above ) {
						pos = hitPos + hit.normal * ( gridData.CellSize * 0.5f );
						// Debug.DrawLine(hitPos, pos, Color.blue, 1);
					}
					else {
						pos = hitPos - hit.normal * ( gridData.CellSize * 0.5f );
						// Debug.DrawLine(hitPos, pos, Color.green, 1);
					}
					DebugDrawRaycastHit(pos, Color.yellow);
					worldPosition = pos;
				}
				else if ( bottomPlane.Raycast(ray, out var distance) ) {
					var hitPos = ray.GetPoint(distance);
					var hitPosPlusNormal = hitPos + bottomPlane.normal * ( gridData.CellSize * 0.5f );
					worldPosition = hitPosPlusNormal;
					hitBottom = true;
				}
			}
			else {
				Debug.LogError("Can't find a Camera with the Tag Main Camera.");
			}

			if ( showCenterPos ) {
				if ( above ) {
					DebugDrawRaycastHit(gridData.GetTileCenter3DFromWorldPos(worldPosition), Color.blue);
				}
				else {
					DebugDrawRaycastHit(gridData.GetTileCenter3DFromWorldPos(worldPosition), Color.green);
				}
			}

			if ( showMouseHit ) {
				DebugDrawRaycastHit(worldPosition, Color.red);
			}

			return worldPosition;
		}

		private static void DebugDrawRaycastHit(Vector3 pos, Color color) {
			var duration = .2f;
			Debug.DrawLine(pos + new Vector3(0, -0.2f, 0), pos + new Vector3(0, 0.2f, 0), color,
				duration);
			Debug.DrawLine(pos + new Vector3(-0.2f, 0, 0), pos + new Vector3(0.2f, 0, 0), color,
				duration);
			Debug.DrawLine(pos + new Vector3(0, 0, -0.2f), pos + new Vector3(0, 0, 0.2f), color,
				duration);
		}
	}
}
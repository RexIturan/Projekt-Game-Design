using UnityEngine;

namespace Util.Extensions {
    public static class VectorExtension {
        public static string ToString(this Vector3 vector3) {
            return $"[{vector3.x}, {vector3.y}, {vector3.z}]";
        }

        public static Vector3 Abs(this Vector3 vector3) {
	        return new Vector3(
		        x: Mathf.Abs(vector3.x), 
		        y: Mathf.Abs(vector3.y),
		        z: Mathf.Abs(vector3.z));
        }
        
        public static Vector3Int Abs(this Vector3Int vector3Int) {
	        return new Vector3Int(
		        x: Mathf.Abs(vector3Int.x), 
		        y: Mathf.Abs(vector3Int.y),
		        z: Mathf.Abs(vector3Int.z));
        }
        
        public static Vector2Int Abs(this Vector2Int vector2Int) {
	        return new Vector2Int(
		        x: Mathf.Abs(vector2Int.x), 
		        y: Mathf.Abs(vector2Int.y));
        }
    }
}
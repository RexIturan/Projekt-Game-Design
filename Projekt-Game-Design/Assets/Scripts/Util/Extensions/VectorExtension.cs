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
        
        public static Vector2Int XY(this Vector3Int vector3Int) {
	        return new Vector2Int(
		        x: vector3Int.x, 
		        y: vector3Int.y);
        }
        
        public static Vector2Int XZ(this Vector3Int vector3Int) {
	        return new Vector2Int(
		        x: vector3Int.x, 
		        y: vector3Int.z);
        }
        
        public static Vector3Int XZ(this Vector2Int vector2Int) {
	        return new Vector3Int(
		        x: vector2Int.x,
		        y: 0,
		        z: vector2Int.y);
        }
        
        public static Vector2Int Abs(this Vector2Int vector2Int) {
	        return new Vector2Int(
		        x: Mathf.Abs(vector2Int.x), 
		        y: Mathf.Abs(vector2Int.y));
        }


        public static Vector2 ZWInt(this Vector4 vec4) {
	        return new Vector2(vec4.z, vec4.w);
        }

        public static void SetZW(ref this Vector4 vec4, Vector2 vec2) {
	        vec4.z = vec2.x;
	        vec4.w = vec2.y;
        }
    }
}
using UnityEngine;

namespace Util.Extensions {
    public static class VectorExtension {
        public static string ToString(this Vector3 vector3) {
            return $"[{vector3.x}, {vector3.y}, {vector3.z}]";
        }
    }
}
using UnityEngine;

namespace Util.ScriptableObjects {
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Utils/FloatVariable", order = 0)]
    public class FloatVariable : ScriptableObject {
        public float value;
    }
}
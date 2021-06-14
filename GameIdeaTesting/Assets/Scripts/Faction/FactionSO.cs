using UnityEngine;

namespace DefaultNamespace.Faction {
    [CreateAssetMenu(fileName = "New Faction", menuName = "Game/New Faction", order = 0)]
    public class FactionSO : ScriptableObject {
        public string name;
        public Color color;
    }
}
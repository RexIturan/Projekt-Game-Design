using UnityEngine;

namespace Characters.EnemyCharacter.ScriptableObjects {
    [CreateAssetMenu(fileName = "New EnemySpawnData", menuName = "Character/Enemy/EnemySpawnData")]
    public class EnemySpawnDataSO : ScriptableObject {
        public int id;
        public Vector3Int gridPos;
        public int range;
        public int attack;
        public int movementpointsPerEnergy;
    }
}
using UnityEngine;

namespace Characters.PlayerCharacter.ScriptableObjects {
    [CreateAssetMenu(fileName = "NewPlayerSpawnData", menuName = "Character/PlayerSpawnData")]
    public class PlayerSpawnDataSO : ScriptableObject {
        public int id;
        public int level;
        public int experience;
        public int movementPointsPerEnergy;
        public Vector3Int gridPos;
        public int equipmentID;
    }
}
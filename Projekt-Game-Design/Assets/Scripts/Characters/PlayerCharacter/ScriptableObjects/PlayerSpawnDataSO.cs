using System.Collections.Generic;
using UnityEngine;

namespace Characters.PlayerCharacter.ScriptableObjects {
    [CreateAssetMenu(fileName = "NewPlayerSpawnData", menuName = "Character/PlayerSpawnData")]
    public class PlayerSpawnDataSO : ScriptableObject {
        public int id;
        
        public Vector3Int gridPos;
        public int equipmentID;
        public List<StatusValue> overrideStatusValues;
        
        //todo image id or generate on runtime
        public int imageID;
        public Sprite profilePicture;
    }
}
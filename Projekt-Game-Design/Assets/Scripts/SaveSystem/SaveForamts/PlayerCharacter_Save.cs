using System;
using UnityEngine;

namespace SaveSystem.SaveFormats {
    [Serializable]
    public class PlayerCharacter_Save {
        public int plyerTypeId;
        public int plyerSpawnDataId;
        public int equipmentInventoryId;
        public Vector3Int pos;
    }
}
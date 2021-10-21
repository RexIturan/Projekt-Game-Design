using System;
using UnityEngine;

namespace SaveSystem.SaveFormats {
    [Serializable]
    public class Enemy_Save {
        public int enemyTypeId;
        public int enemySpawnDataId;
        public Vector3Int pos;
    }
}
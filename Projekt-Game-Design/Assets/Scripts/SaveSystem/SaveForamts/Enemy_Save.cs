using System;
using UnityEngine;

namespace SaveSystem.SaveFormats {
    [Serializable]
    public class Enemy_Save {
        public int enemyTypeId;
        public Vector3Int pos;
				public int hitpoints;
				public int energy;
    }
}
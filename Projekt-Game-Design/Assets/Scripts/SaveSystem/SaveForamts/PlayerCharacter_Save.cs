using System;
using UnityEngine;

namespace SaveSystem.SaveFormats {
    [Serializable]
    public class PlayerCharacter_Save {
				public int id;
				public bool active;
        public int plyerTypeId;
        public Vector3Int pos;
				public int hitpoints;
				public int energy;
    }
}
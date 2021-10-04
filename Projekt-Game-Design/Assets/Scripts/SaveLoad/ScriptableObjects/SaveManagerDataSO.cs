﻿using UnityEngine;

namespace SaveLoad.ScriptableObjects {
    [CreateAssetMenu(fileName = "SaveManagerData", menuName = "Game/SaveManagerData", order = 0)]
    public class SaveManagerDataSO : ScriptableObject {
        public bool inputLoad;
        public bool inputSave;
        public bool inputNewGame;
        public bool loaded;
        public bool saved;
    }
}
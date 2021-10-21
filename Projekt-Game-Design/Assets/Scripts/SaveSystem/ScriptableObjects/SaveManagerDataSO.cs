using UnityEngine;

namespace SaveSystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "SaveManagerData", menuName = "Game/SaveManagerData", order = 0)]
    public class SaveManagerDataSO : ScriptableObject {
        public bool inputLoad;
        public bool inputSave;
        public bool inputNewGame;
        public bool loaded;
        public bool saved;

        // todo remove
        public string path;
        
        public void Reset() {
            inputLoad = false;
            inputSave = false;
            inputNewGame = false;
            loaded = false;
            saved = false;
        }
    }
}
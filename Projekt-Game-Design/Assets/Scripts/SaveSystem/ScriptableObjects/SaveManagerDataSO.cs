using UnityEngine;

namespace SaveSystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "SaveManagerData", menuName = "Game/SaveManagerData", order = 0)]
    public class SaveManagerDataSO : ScriptableObject {
        [SerializeField] private bool inputLoad;
        [SerializeField] private bool inputSave;
        [SerializeField] private bool inputNewGame;
        [SerializeField] private bool loaded;
        [SerializeField] private bool saved;

        // todo remove
        [SerializeField] private string path;
        
        public void Reset() {
            inputLoad = false;
            inputSave = false;
            inputNewGame = false;
            loaded = false;
            saved = false;
        }
    }
}
using SaveSystem;
using UnityEngine;

namespace SceneManagement {
    public class LocationLoader : MonoBehaviour {
        
        [Header("Sending Events On")]
        [SerializeField] private VoidEventChannelSO enableLoadingScreenInputEC;
        
        // get at runtime
        private SaveManager _saveSystem;
        
        private void Awake() {
            _saveSystem = FindObjectOfType<SaveManager>();
        }

        private void Start() {
            if (_saveSystem is { }) {
                _saveSystem.InitializeLevel();
                enableLoadingScreenInputEC.RaiseEvent();    
            }
        }
    }
}

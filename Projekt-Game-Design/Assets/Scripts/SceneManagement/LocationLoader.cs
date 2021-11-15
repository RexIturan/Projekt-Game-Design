using SaveSystem;
using UnityEngine;

namespace SceneManagement {
    public class LocationLoader : MonoBehaviour {
        
        [Header("Sending Events On")]
        [SerializeField] private VoidEventChannelSO enableLoadingScreenInputEC;
        [SerializeField] private VoidEventChannelSO enableGampleyInputEC;

        [SerializeField] private bool initializeFromSave;
        
        // get at runtime
        private SaveManager _saveSystem;
        
        private void Awake() {
            _saveSystem = FindObjectOfType<SaveManager>();
        }

        private void Start() {
	        if ( initializeFromSave ) {
		        // try to load level from save object
		        if (_saveSystem is { }) {
			        _saveSystem.InitializeLevel();
			        enableLoadingScreenInputEC.RaiseEvent();    
		        }    
	        }
	        else {
		        //todo load empty or default??
		        
		        // enable on Start
		        enableGampleyInputEC.RaiseEvent();
	        }
        }
    }
}

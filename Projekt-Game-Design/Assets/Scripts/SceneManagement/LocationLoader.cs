using System;
using SaveLoad;
using UnityEngine;

namespace SceneManagement {
    public class LocationLoader : MonoBehaviour {
        
        // [Header("recieving Events On")]
        // private VoidEventChannelSO onSceneReady_EC;
        
        [Header("Sending Events On")]
        [SerializeField] private VoidEventChannelSO enableLoadingScreenInput_EC;
        
        // get at runtime
        private SaveManager saveSystem;
        
        private void Awake() {
            saveSystem = GameObject.FindObjectOfType<SaveManager>();
        }

        private void Start() {
            if (saveSystem is { }) {
                saveSystem.InitializeLevel();
                enableLoadingScreenInput_EC.RaiseEvent();    
            }
        }
    }
}

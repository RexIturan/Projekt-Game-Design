using System;
using Events.ScriptableObjects;
using GameManager;
using SaveLoad.ScriptableObjects;
using UnityEngine;

namespace DebugTest.DebugSceneSetup {
    public class TestSceneSetup : MonoBehaviour {
        [TextArea] public string description;
        [SerializeField] private GameSC gameSc;
        [SerializeField] private SaveManagerDataSO saveManagerData;
        [SerializeField] private VoidEventChannelSO loadLevel;
        [SerializeField] private IntEventChannelSO loadGame;

        private void Awake() {
            loadLevel.OnEventRaised += HandleLoadLevel;
            loadGame.OnEventRaised += HandleLoadLevel;
        }

        private void HandleLoadLevel() {
            saveManagerData.inputLoad = true;
        }
        
        private void HandleLoadLevel(int value) {
            HandleLoadLevel();
        }

        private void Start() {
            gameSc.initializedGame = true;
            gameSc.initializedTactics = true;
            gameSc.isInTacticsMode = true;
        }
    }
}
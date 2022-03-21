using System.Collections;
using Events.ScriptableObjects;
using SaveSystem;
using SceneManagement.ScriptableObjects;
using UnityEngine;

namespace SceneManagement {
    public class StartGame : MonoBehaviour {
        public LoadEventChannelSO onPlayButtonPress;
        public GameSceneSO[] locationsToLoad;
        public bool showLoadScreen;

        //todo refactor i dont like that here
        public SaveManager saveSystem;

        private bool _hasSaveData;

        //todo refactor??
        
        private void Start()
        {
            // try loading save file
            Debug.Log("StartGame: try loading save file");
            
            _hasSaveData = saveSystem.LoadLevel("");
            
            if (_hasSaveData)
            {
                Debug.Log("StartGame: files read");


                // saveSystem.LoadGame();
                
                // load
                // saveManagerData.loaded = true;
                
            }
            else
            {
                // resetSaveDataButton.gameObject.SetActive(false);
            }
        }

        // public void OnPlayButtonPress()
        // {
        //     if (!_hasSaveData)
        //     {
        //         // saveSystem.WriteEmptySaveFile();
        //         //Start new game
        //         // onPlayButtonPress.RaiseEvent(locationsToLoad, showLoadScreen);
        //     }
        //     else
        //     {
        //         //Load Game
        //         StartCoroutine(LoadSaveGame());
        //     }
        // }
        
        public IEnumerator LoadSaveGame()
        {
            // yield return StartCoroutine(saveSystem.LoadSavedInventory());

            // var locationGuid = saveSystem.saveData._locationId;
            // var asyncOperationHandle = Addressables.LoadAssetAsync<LocationSO>(locationGuid);
            // yield return asyncOperationHandle;
            // if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            // {
            //     var locationSo = asyncOperationHandle.Result;
            //     onPlayButtonPress.RaiseEvent(new[] { (GameSceneSO)locationSo }, showLoadScreen);
            // }
            return null;
        }
    }
}
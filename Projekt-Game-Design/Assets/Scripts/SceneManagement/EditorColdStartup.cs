using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SceneManagement {

    /// <summary>
    /// Allows a "cold start" in the editor, when pressing Play and not passing from the Initialisation scene.
    /// </summary> 
    public class EditorColdStartup : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private GameSceneSO _thisSceneSO = default;
        [SerializeField] private GameSceneSO _startSceneSO = default;
        [SerializeField] private GameSceneSO _persistentManagersSO = default;
        [SerializeField] private AssetReference _loadSceneEventChannel = default;

        private void Start()
        {
            Debug.Log("cold startup: is persistant managers loaded?");
            if (!SceneManager.GetSceneByName(_persistentManagersSO.sceneReference.editorAsset.name).isLoaded)
            {
                Debug.Log("cold startup: persistant managers NOT loaded -> send loading event now loading now");
                _persistentManagersSO.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
            }
            else {
                Debug.Log("cold startup: persistant managers loaded!");
            }
        }

        private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
        {
            Debug.Log("cold startup: persistant managers loaded, load event channel");
            _loadSceneEventChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += ReloadScene;
        }

        private void ReloadScene(AsyncOperationHandle<LoadEventChannelSO> obj)
        {
            Debug.Log("cold startup: loadEC loaded, reload scene??");
            LoadEventChannelSO loadEventChannelSO = (LoadEventChannelSO)_loadSceneEventChannel.Asset;
            loadEventChannelSO.RaiseEvent(new GameSceneSO[] { _startSceneSO });

            SceneManager.UnloadSceneAsync(_thisSceneSO.sceneReference.editorAsset.name).completed += operation => {
                Debug.Log("cold startup: unloading of original scene complete");
            };
        }
#endif
    }

}

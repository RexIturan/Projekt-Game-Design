// ReSharper disable RedundantUsingDirective
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
        [SerializeField] private GameSceneSO thisSceneSO;
        [SerializeField] private GameSceneSO startSceneSO;
        [SerializeField] private GameSceneSO persistentManagersSO;
        [SerializeField] private AssetReference loadSceneEventChannel;

        private void Start() {
            if (persistentManagersSO is { }) {
                // Debug.Log("cold startup: is persistant managers loaded?");
                if (!SceneManager.GetSceneByName(persistentManagersSO.sceneReference.editorAsset.name).isLoaded)
                {
	                Debug.Log("EditorColdStartup > Start \ncold startup: persistant managers NOT loaded -> send loading event now loading now");
                    // Debug.Log("cold startup: persistant managers NOT loaded -> send loading event now loading now");
                    persistentManagersSO.sceneReference.LoadSceneAsync(LoadSceneMode.Additive).Completed += LoadEventChannel;
                }
                else {
                    Debug.Log("EditorColdStartup > Start \ncold startup: persistant managers loaded!");
                    //todo destroy this EditorColdStartup
                }
            }
        }

        private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
        {
            // Debug.Log("cold startup: persistant managers loaded, load event channel");
            loadSceneEventChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += ReloadScene;
        }

        private void ReloadScene(AsyncOperationHandle<LoadEventChannelSO> obj)
        {
            // Debug.Log("cold startup: loadEC loaded, reload scene??");
            LoadEventChannelSO loadEventChannelSO = (LoadEventChannelSO)loadSceneEventChannel.Asset;
            // loadEventChannelSO.RaiseEvent(new[] { startSceneSO });

            SceneManager.UnloadSceneAsync(thisSceneSO.sceneReference.editorAsset.name).completed += operation => {
                Debug.Log("EditorColdStartup > ReloadScene > UnloadSceneAsync(thisScene).completed\n cold startup: unloading of original scene complete \n-> load startSceneSO");
                loadEventChannelSO.RaiseEvent(new[] { startSceneSO });
            };
            
            //todo destroy this EditorColdStartup
        }
#endif
    }

}

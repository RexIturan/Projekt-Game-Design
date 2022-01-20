using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for starting the game by loading the persistent managers scene 
/// and raising the event to load the Main Menu
/// </summary>
public class InitializationLoader : MonoBehaviour {
	[Header("Persistent managers Scene")] [SerializeField]
	private GameSceneSO persistentManagersScene;

	[Header("Loading settings")] [SerializeField]
	private GameSceneSO[] menuToLoad;

	[Header("Broadcasting on")] [SerializeField]
	private AssetReference menuLoadChannel;

	private void Start() {
		//Load the persistent managers scene
		persistentManagersScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive)
			.Completed += LoadEventChannel;
	}

	private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj) {
		menuLoadChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += LoadMainMenu;
	}

	private void LoadMainMenu(AsyncOperationHandle<LoadEventChannelSO> obj) {
		LoadEventChannelSO loadEventChannelSO = ( LoadEventChannelSO )menuLoadChannel.Asset;
		loadEventChannelSO.RaiseEvent(menuToLoad);

		SceneManager.UnloadSceneAsync(0); 
		//Initialization is the only scene in BuildSettings, thus it has index 0
	}
}
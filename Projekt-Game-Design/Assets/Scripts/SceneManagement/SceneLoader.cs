using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the scene loading and unloading.
/// </summary>
public class SceneLoader : MonoBehaviour {
	[SerializeField] private GameSceneSO gameplayScene;

	[Header("Load Events")] [SerializeField]
	private LoadEventChannelSO loadLocation;

	[SerializeField] private LoadEventChannelSO loadMenu;

	[Header("Broadcasting on")] 
	[SerializeField] private BoolEventChannelSO toggleLoadingScreen;
	[SerializeField] private VoidEventChannelSO enableLoadingScreenInput;
	[SerializeField] private VoidEventChannelSO onSceneReady;


	private readonly List<AsyncOperationHandle<SceneInstance>> _loadingOperationHandles =
		new List<AsyncOperationHandle<SceneInstance>>();

	private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;

	//Parameters coming from scene loading requests
	private GameSceneSO[] _scenesToLoad;
	private GameSceneSO[] _currentlyLoadedScenes = new GameSceneSO[] { };
	private bool _showLoadingScreen;
	private bool _closeLoadingScreenOnSceneReady;

	private SceneInstance _gameplayManagerSceneInstance;

	private void OnEnable() {
		loadLocation.OnLoadingRequested += LoadLocation;
		loadMenu.OnLoadingRequested += LoadMenu;
	}

	private void OnDisable() {
		loadLocation.OnLoadingRequested -= LoadLocation;
		loadMenu.OnLoadingRequested -= LoadMenu;
	}

	/// <summary>
	/// This function loads the location scenes passed as array parameter
	/// </summary>
	private void LoadLocation(GameSceneSO[] locationsToLoad, bool showLoadingScreen,
		bool closeLoadingScreenOnSceneReady) {
		_scenesToLoad = locationsToLoad;
		_showLoadingScreen = showLoadingScreen;
		_closeLoadingScreenOnSceneReady = closeLoadingScreenOnSceneReady;

		//In case we are coming from the main menu, we need to load the persistent Gameplay manager scene first
		// ReSharper disable once ConditionIsAlwaysTrueOrFalse
		if ( _gameplayManagerSceneInstance.Scene == null
		     || !_gameplayManagerSceneInstance.Scene.isLoaded ) {
			//todo timeout condition??
			StartCoroutine(ProcessGameplaySceneLoading());
		}
		else {
			UnloadPreviousScenes();
		}
	}

	private IEnumerator ProcessGameplaySceneLoading() {
		_gameplayManagerLoadingOpHandle =
			gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);

		while ( _gameplayManagerLoadingOpHandle.Status != AsyncOperationStatus.Succeeded ) {
			// Debug.Log("SceneLoader#ProcessGameplaySceneLoading#WhileLoop Load Gameplay Scene");
			//todo timeout condition??
			yield return null;
		}

		_gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;

		UnloadPreviousScenes();
	}

	/// <summary>
	/// Prepares to load the main menu scene, first removing the Gameplay scene in case the game is coming back from gameplay to menus.
	/// </summary>
	private void LoadMenu(GameSceneSO[] menusToLoad, bool showLoadingScreen,
		bool closeLoadingScreenOnSceneReady) {
		_scenesToLoad = menusToLoad;
		_showLoadingScreen = showLoadingScreen;
		_closeLoadingScreenOnSceneReady = closeLoadingScreenOnSceneReady;

		//In case we are coming from a Location back to the main menu, we need to get rid of the persistent Gameplay manager scene
		if ( _gameplayManagerSceneInstance.Scene is { isLoaded: true } )
			Addressables.UnloadSceneAsync(_gameplayManagerLoadingOpHandle);

		UnloadPreviousScenes();
	}

	/// <summary>
	/// In both Location and Menu loading, this function takes care of removing previously loaded temporary scenes.
	/// </summary>
	private void UnloadPreviousScenes() {
		Debug.Log("SceneLoader > UnloadPreviousScenes:\nStart unload Previous Scene");

		for ( int i = 0; i < _currentlyLoadedScenes.Length; i++ ) {
			// Addressables.UnloadSceneAsync(_currentlyLoadedScenes[i].sceneReference.OperationHandle);
			_currentlyLoadedScenes[i].sceneReference.UnLoadScene().Completed += (handle) => {
				Debug.Log("SceneLoader > UnloadPreviousScenes\n unloaded previous scene");
			};
		}

		LoadNewScenes();
	}

	/// <summary>
	/// Kicks off the asynchronous loading of an array of scenes, either menus or Locations.
	/// </summary>
	private void LoadNewScenes() {
		Debug.Log("SceneLoader > LoadNewScenes:\nStart Load New Scene");

		if ( _showLoadingScreen ) {
			toggleLoadingScreen.RaiseEvent(true);
		}

		_loadingOperationHandles.Clear();
		//Build the array of handles of the temporary scenes to load
		for ( int i = 0; i < _scenesToLoad.Length; i++ ) {
			_loadingOperationHandles.Add(_scenesToLoad[i].sceneReference
				.LoadSceneAsync(LoadSceneMode.Additive, true, 0));
		}

		StartCoroutine(LoadingProcess());
	}

	private IEnumerator LoadingProcess() {
		bool done = _loadingOperationHandles.Count == 0;

		//todo timeout for this function
		//This while will exit when all scenes requested have been unloaded
		while ( !done ) {
			// Debug.Log("SceneLoader#LoadingProcess#WhileLoop");

			for ( int i = 0; i < _loadingOperationHandles.Count; ++i ) {
				if ( _loadingOperationHandles[i].Status != AsyncOperationStatus.Succeeded ) {
					break;
				}
				else {
					done = true;
				}
			}

			yield return null;
		}

		//Save loaded scenes (to be unloaded at next load request)
		_currentlyLoadedScenes = _scenesToLoad;
		SetActiveScene();

		if ( _showLoadingScreen ) {
			if ( _closeLoadingScreenOnSceneReady ) {
				toggleLoadingScreen.RaiseEvent(false);
			}
			else {
				// todo move to location initialiser
				enableLoadingScreenInput.RaiseEvent();
			}
		}
	}

	/// <summary>
	/// This function is called when all the scenes have been loaded
	/// </summary>
	private void SetActiveScene() {
		//All the scenes have been loaded, so we assume the first in the array is ready to become the active scene
		Scene s = ( _loadingOperationHandles[0].Result ).Scene;
		SceneManager.SetActiveScene(s);

		onSceneReady.RaiseEvent();
	}

	// TODO use
	// private void ExitGame() {
	//     Application.Quit();
	//     Debug.Log("Exit!");
	// }
}
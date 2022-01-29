using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using static SceneManagement.SceneLoadingManager;

/// <summary>
/// This class manages the scene loading and unloading.
/// Runtime Component, can acess the scene state and alter it
/// </summary>
public class SceneLoader : MonoBehaviour {
	
	[Serializable]
	private class SceneCache {
		public AsyncOperationHandle<SceneInstance> persistantManagers;
		public AsyncOperationHandle<SceneInstance> gameplay;
		public AsyncOperationHandle<SceneInstance> tactics;
		public List<AsyncOperationHandle<SceneInstance>> ui;
		public List<AsyncOperationHandle<SceneInstance>> level;
	}
	
///// Serialized Variables //////////////////////////////////////////////////////////////////////////
	
	[SerializeField] private GameSceneSO gameplayScene;
	[SerializeField] private GameSceneSO tacticsScene;

	[Header("Load Events")] 
	[SerializeField] private LoadEventChannelSO loadLocation;
	[SerializeField] private LoadEventChannelSO loadMenu;

	[Header("Broadcasting on")] 
	[SerializeField] private BoolEventChannelSO toggleLoadingScreen;
	[SerializeField] private VoidEventChannelSO enableLoadingScreenInput;
	[SerializeField] private VoidEventChannelSO onSceneReady;

///// Private Variable /////////////////////////////////////////////////////////////////////////////

	private List<AsyncOperationHandle<SceneInstance>> _loadingOperationHandles =
		new List<AsyncOperationHandle<SceneInstance>>();

	// private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;
	// private AsyncOperationHandle<SceneInstance> _unloadOpHandle;

	private GameSceneSO[] _currentlyLoadedScenes = new GameSceneSO[] { };

	// private SceneInstance _gameplayManagerSceneInstance;

	
	//scene cache
	private readonly SceneCache _sceneCache = new SceneCache();
	
	
	//Parameters coming from scene loading requests
	private GameSceneSO[] _scenesToLoad;
	private bool _showLoadingScreen;
	private bool _closeLoadingScreenOnSceneReady;

///// Private Functions ////////////////////////////////////////////////////////////////////////////
	
	/// <summary>
	/// This function loads the location scenes passed as array parameter
	/// </summary>
	private void LoadLocation(GameSceneSO[] locationsToLoad, bool showLoadingScreen,
		bool closeLoadingScreenOnSceneReady) {
		
		_scenesToLoad = locationsToLoad;
		_showLoadingScreen = showLoadingScreen;
		_closeLoadingScreenOnSceneReady = closeLoadingScreenOnSceneReady;
		
		//In case we are coming from the main menu, we need to load the persistent Gameplay manager scene first
		if ( !IsSceneLoaded(_sceneCache.gameplay) ) {
			// }
			// if ( _gameplayManagerSceneInstance is { Scene: null }
			//      || !_gameplayManagerSceneInstance.Scene.isLoaded ) {
 			ProcessGameplaySceneLoading();
		}
		else {
			UnloadPreviousScenes();
		}
	}

	private void ProcessGameplaySceneLoading() {
		_sceneCache.gameplay = StartLoadingScene(gameplayScene.sceneReference, true, true);

		_sceneCache.gameplay.Completed += handle => UnloadPreviousScenes();
		//OR
		//StartCoroutine(OnLoadingDone(_gameplayManagerLoadingOpHandle, UnloadPreviousScenes));

		
		
		///// OLD /////
		
		// gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);

		// yield return OnLoadingDone(_gameplayManagerLoadingOpHandle, UnloadPreviousScenes);

		// while ( _gameplayManagerLoadingOpHandle.Status != AsyncOperationStatus.Succeeded ) {
		// 	// Debug.Log("SceneLoader#ProcessGameplaySceneLoading#WhileLoop Load Gameplay Scene");
		// 	//todo timeout condition??
		// 	yield return null;
		// }
		//
		// _gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;
		//
		// UnloadPreviousScenes();
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
		// if ( _gameplayManagerSceneInstance.Scene is { isLoaded: true } )
		// 	StartUnloadScene(_gameplayManagerLoadingOpHandle);
			// Addressables.UnloadSceneAsync(_gameplayManagerLoadingOpHandle);
		
			
		if(IsSceneLoaded(_sceneCache.gameplay))
			StartUnloadScene(_sceneCache.gameplay);

		UnloadPreviousScenes();
	}

	/// <summary>
	/// In both Location and Menu loading, this function takes care of removing previously loaded temporary scenes.
	/// </summary>
	private void UnloadPreviousScenes() {
		//todo better unloading
		Debug.Log("SceneLoader > UnloadPreviousScenes:\nStart unload Previous Scene");

		if ( _sceneCache.level is { } ) {
			var unloadingHandles = StartUnloadingScenes(_sceneCache.level);
			StartCoroutine(OnHandleAllColplete(unloadingHandles, LoadNewScenes));
		}
		else {
			LoadNewScenes();
		}
		
		//
		// bool scenesUnloading = false;
		// for ( int i = 0; i < _currentlyLoadedScenes.Length; i++ ) {
		// 	scenesUnloading = true;
		// 	_unloadOpHandle = _currentlyLoadedScenes[i].sceneReference.UnLoadScene();
		// 	// Addressables.UnloadSceneAsync(_currentlyLoadedScenes[i].sceneReference.OperationHandle);
		// 	_unloadOpHandle.Completed += (handle) => {
		// 		Debug.Log("SceneLoader > UnloadPreviousScenes\n unloaded previous scene");
		// 	};
		// }

		// if ( scenesUnloading ) {
		// 	if ( unloadingHandles.All(handle => handle.IsDone) ) {
		// 		
		// 	}
		// 	_unloadOpHandle.Completed += handle => {
		// 		scenesUnloading = false;
		// 		LoadNewScenes();
		// 	};	
		// }
		// else {
		// 	LoadNewScenes();
		// }
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
		_loadingOperationHandles = StartLoadingScenes(
				_scenesToLoad.Select(so => so.sceneReference).ToList(), true);

		StartCoroutine(OnHandleAllColplete(_loadingOperationHandles, LoadingProcess));
		
		//Build the array of handles of the temporary scenes to load
		// for ( int i = 0; i < _scenesToLoad.Length; i++ ) {
		// 	_loadingOperationHandles.Add(_scenesToLoad[i].sceneReference
		// 		.LoadSceneAsync(LoadSceneMode.Additive, true, 0));
		// }

		// StartCoroutine(LoadingProcess());
	}

	private void LoadingProcess() {
		// bool done = _loadingOperationHandles.Count == 0;

		//todo timeout for this function
		//This while will exit when all scenes requested have been unloaded
		// while ( !done ) {
		// 	// Debug.Log("SceneLoader#LoadingProcess#WhileLoop");
		//
		// 	for ( int i = 0; i < _loadingOperationHandles.Count; ++i ) {
		// 		if ( _loadingOperationHandles[i].Status != AsyncOperationStatus.Succeeded ) {
		// 			done = false;
		// 			break;
		// 		}
		// 		else {
		// 			done = true;
		// 		}
		// 	}
		//
		// 	yield return null;
		// }

		_sceneCache.level = _loadingOperationHandles;
		
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
	
///// Unity Functions //////////////////////////////////////////////////////////////////////////////	
	
	private void OnEnable() {
		loadLocation.OnLoadingRequested += LoadLocation;
		loadMenu.OnLoadingRequested += LoadMenu;
	}

	private void OnDisable() {
		loadLocation.OnLoadingRequested -= LoadLocation;
		loadMenu.OnLoadingRequested -= LoadMenu;
	}
}
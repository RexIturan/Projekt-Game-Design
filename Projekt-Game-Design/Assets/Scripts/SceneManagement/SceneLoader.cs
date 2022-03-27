using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events.ScriptableObjects;
using GDP01.SceneManagement.EventChannels;
using SceneManagement;
using SceneManagement.ScriptableObjects;
using SceneManagement.Types;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static SceneManagement.SceneLoadingManager;

/// <summary>
/// This class manages the scene loading and unloading.
/// Runtime Component, can acess the scene state and alter it
/// </summary>
public class SceneLoader : MonoBehaviour {
	
	[Serializable]
	private class SceneDataOperationHandleMapping {
		public AsyncOperationHandle<SceneInstance> operationHandle;
		public GameSceneSO sceneData;
	}

	[Serializable]
	private struct LoadingQueueElement {
		public List<SceneDataOperationHandleMapping> data;
	}
	
	[Serializable]
	private class SceneCache {
		public List<SceneDataOperationHandleMapping> persistant = new List<SceneDataOperationHandleMapping>();
		public List<SceneDataOperationHandleMapping> ui = new List<SceneDataOperationHandleMapping>();
		public List<SceneDataOperationHandleMapping> main = new List<SceneDataOperationHandleMapping>();
		public List<LoadingQueueElement> loadingQueue = new List<LoadingQueueElement>();
		
		
		public AsyncOperationHandle<SceneInstance> gameplay;
		public List<AsyncOperationHandle<SceneInstance>> level;
	}
	
///// Serialized Variables //////////////////////////////////////////////////////////////////////////
	
	[SerializeField] private GameSceneSO gameplayScene;

	[Header("Load Events")] 
	[SerializeField] private SceneLoadingInfoEventChannelSO loadSceneEC;
	[SerializeField] private SceneLoadingInfoEventChannelSO unloadSceneEC;
	
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
	[SerializeField] private SceneCache _sceneCache = new SceneCache();
	
	
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
			StartCoroutine(OnAllHandlesColplete(unloadingHandles, LoadNewScenes));
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

		StartCoroutine(OnAllHandlesColplete(_loadingOperationHandles, LoadingProcess));
		
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

	private void ActivateNewScene(Scene scene) {
		SceneManager.SetActiveScene(scene);
		onSceneReady.RaiseEvent();
	}

	private void SwitchCurrentScenes() {
		if ( _sceneCache.loadingQueue?.Count > 0) {
			_sceneCache.main.Clear();
			var element = _sceneCache.loadingQueue.Last();
			_sceneCache.main = element.data;
			_sceneCache.loadingQueue.Remove(element);

			StringBuilder sb = new StringBuilder();
			_sceneCache.main.ForEach(mapping => sb.Append(mapping.sceneData.name));
			
			Debug.Log($"New Main: \n{sb}");
		}
	}

	private List<AsyncOperationHandle<SceneInstance>> UnloadCurrentScenes(List<SceneDataOperationHandleMapping> currentScenes) {
		return SceneLoadingManager.StartUnloadingScenes(
			currentScenes.Select(mapping => mapping.operationHandle).ToList()
			);
	}

	private AsyncOperationHandle<SceneInstance> LoadScene(SceneLoadingData loadingData) {
		GameSceneSO sceneDataToLoad = loadingData.MainSceneData;
		
		//start loading
		var opHandle = StartLoadingScene(sceneDataToLoad.sceneReference, true,
			loadingData.ActivateOnLoad);
		
		SceneDataOperationHandleMapping mapping = new SceneDataOperationHandleMapping {
			operationHandle = opHandle,
			sceneData = sceneDataToLoad
		};
		
		if ( loadingData.DontUnload ) {
			_sceneCache.persistant.Add(mapping);
		} else if ( sceneDataToLoad is TacticsLevelSO || sceneDataToLoad is MenuSO ) {
			_sceneCache.loadingQueue.Insert(0, new LoadingQueueElement {
				data = new List<SceneDataOperationHandleMapping>{ mapping }
			});
		}

		return opHandle;
	}
	
	private AsyncOperationHandle<SceneInstance> UnloadScene(SceneDataOperationHandleMapping mapping) {
		//start loading
		return StartUnloadScene(mapping.operationHandle);
	}

	private void LoadDependencies(List<SceneLoadingData.SceneDataDependencySettings> dependencies) {
		foreach ( var dependency in dependencies ) {
			var sceneData = dependency.sceneData;

			//check dependencies
			var foundScene = FindScneIfLoaded(sceneData);
			
			if ( !SceneIsLoaded(foundScene)) {
				//load dependency
				LoadDependency(sceneData);
			}
			else {
				//todo reset
				//foundScene.reset ??
			}
		}
	}

	private void LoadDependency(GameSceneSO sceneData) {
		SceneDataOperationHandleMapping mapping = new SceneDataOperationHandleMapping {
			operationHandle = SceneLoadingManager.StartLoadingScene(sceneData.sceneReference, true, true),
			sceneData = sceneData
		};
		
		if ( sceneData is UISceneSO ) {
			_sceneCache.ui.Add( mapping );
		}
		else if ( sceneData is PersistentManagersSO ) {
			_sceneCache.persistant.Add( mapping );
		}
		else if ( sceneData is ControlSceneSO ) {
			_sceneCache.persistant.Add( mapping );
		}
	}

	private bool SceneIsLoaded(SceneDataOperationHandleMapping foundScene) {
		return foundScene is {} && foundScene.operationHandle.IsValid() &&
		       foundScene is {
			operationHandle:
			{
				Status: AsyncOperationStatus.Succeeded, Result: {
					Scene: { isLoaded: true }
				}
			}
		};
	}

	private SceneDataOperationHandleMapping FindScneIfLoaded(GameSceneSO gameSceneSO) {
		return _sceneCache.persistant.FirstOrDefault(mapping => mapping.sceneData.Equals(gameSceneSO)) ??
		            _sceneCache.ui.FirstOrDefault(mapping => mapping.sceneData.Equals(gameSceneSO));
	}

	private AsyncOperationHandle<SceneInstance> LoadNewScene(SceneLoadingData loadingData) {
		Debug.Log($"Try Loading Scene:\n{loadingData.MainSceneData.name}");
		
		//load main scene
		var loadingHandle = LoadScene(loadingData);

		//wait for activation
		//swap scenes after new scene is loaded
		loadingHandle.Completed += handle => {
			//todo own flag or so
			if ( !loadingData.DontUnload ) {
				SwitchCurrentScenes();
				//activate new main scene
				ActivateNewScene(handle.Result.Scene);
			}
			else {
				//todo invoke new scene ready -> Activation Button ready or so
			}
		};
		return loadingHandle;
	}
	
///// Callbacks ////////////////////////////////////////////////////////////////////////////////////

	private void HandleLoadScene(SceneLoadingData loadingData) {
		
		
		//todo check if loading Data is valid
		
		//todo reload scene
		// scene already loaded?
		if ( _sceneCache.loadingQueue.Any(
			    element => element.data.Any(
				    mapping => mapping.sceneData == loadingData.MainSceneData))) {
			Debug.Log($"Scene Already Loading:\n{loadingData.MainSceneData.name}");
			return;
		} 
		if(_sceneCache.main.Any(mapping => mapping.sceneData == loadingData.MainSceneData)) {
			Debug.Log($"Scene Already Loaded:\n{loadingData.MainSceneData.name}");
			return;
		}
		
		if ( loadingData.Dependencies?.Count > 0 ) {
			LoadDependencies(loadingData.Dependencies);
		}
		
		//load main scene as dependency if, it shouldnt be unloaded
		if ( loadingData.DontUnload ) {
			LoadNewScene(loadingData);
			return;
		}

		if ( _sceneCache.main is { Count: > 0 } ) {
			//unload current
			var unloadHandles = UnloadCurrentScenes(_sceneCache.main);

			StartCoroutine(OnAllHandlesColplete(unloadHandles, () => {
				LoadNewScene(loadingData);
			}));
		}
		else {
			// todo all scenes must be main, check if sctive scene is in dependencies
			// Scene currentActiveScene = SceneManager.GetActiveScene();
			// if ( currentActiveScene is {  } ) {
			// 	AsyncOperation unloadHandle = SceneManager.UnloadSceneAsync(currentActiveScene);
			// 	unloadHandle.completed += operation => {
			// 		LoadNewScene(loadingData);	
			// 	};
			// }
			// else {
			// 	
			// }
			
			LoadNewScene(loadingData);
		}
	}

	private void HandleUnloadScene(SceneLoadingData loadingData) {
		GameSceneSO sceneData = loadingData.MainSceneData;

		SceneDataOperationHandleMapping mapping = null;
		
		if ( sceneData is UISceneSO ) {
			mapping = _sceneCache.ui.Find(handleMapping => handleMapping.sceneData == sceneData);
			_sceneCache.ui.Remove(mapping);
		}
		else if ( sceneData is PersistentManagersSO ) {
			mapping = _sceneCache.persistant.Find(handleMapping => handleMapping.sceneData == sceneData);
			_sceneCache.persistant.Remove(mapping);
		}
		else if ( sceneData is ControlSceneSO ) {
			mapping = _sceneCache.persistant.Find(handleMapping => handleMapping.sceneData == sceneData);
			_sceneCache.persistant.Remove(mapping);
		}

		UnloadScene(mapping);
	}
	
	///// Unity Functions //////////////////////////////////////////////////////////////////////////////	
	
	private void OnEnable() {
		loadLocation.OnLoadingRequested += LoadLocation;
		loadMenu.OnLoadingRequested += LoadMenu;
		
		loadSceneEC.OnLoadingRequested += HandleLoadScene;
		unloadSceneEC.OnLoadingRequested += HandleUnloadScene;
	}

	private void OnDisable() {
		loadLocation.OnLoadingRequested -= LoadLocation;
		loadMenu.OnLoadingRequested -= LoadMenu;
		
		loadSceneEC.OnLoadingRequested -= HandleLoadScene;
		unloadSceneEC.OnLoadingRequested -= HandleUnloadScene;
	}
}
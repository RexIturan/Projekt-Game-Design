using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using static UnityEngine.SceneManagement.LoadSceneMode;

namespace SceneManagement {
	public static class SceneLoadingManager {
///// Private Functions ////////////////////////////////////////////////////////////////////////////


///// Public Functions ////////////////////////////////////////////////////////////////////////////

		#region OpHandle Functions
		//todo could be extensionmethodes
		public static bool IsSceneLoaded(AsyncOperationHandle<SceneInstance> operationHandle) {
			return operationHandle.IsValid() && operationHandle.Result.Scene is { isLoaded: true };
		}

		#endregion

		#region Async UnLoading

		public static AsyncOperationHandle<SceneInstance> StartUnloadScene(
			AsyncOperationHandle<SceneInstance> operationHandle) {

			return Addressables.UnloadSceneAsync(operationHandle);
		}

		public static List<AsyncOperationHandle<SceneInstance>> StartUnloadingScenes(
			List<AsyncOperationHandle<SceneInstance>> operationHandles) {

			var unloadingHandles = new List<AsyncOperationHandle<SceneInstance>>();
			foreach ( var operationHandle in operationHandles ) {
				unloadingHandles.Add(StartUnloadScene(operationHandle));
			}

			return unloadingHandles;
		}

		#endregion

		#region Async Loading

		public static AsyncOperationHandle<SceneInstance> StartLoadingScene(
			AssetReference assetReference, bool additive, bool activeOnload) {
			
			return assetReference
				.LoadSceneAsync(additive ? Additive : LoadSceneMode.Single, activeOnload, 0);
		}
		
		public static List<AsyncOperationHandle<SceneInstance>> StartLoadingScenes(
			List<AssetReference> assetReferences, bool additive) {
			
			var loadingHandles = new List<AsyncOperationHandle<SceneInstance>>();
			foreach ( var assetReference in assetReferences ) {
				loadingHandles.Add(StartLoadingScene(assetReference, additive, true));
			}

			return loadingHandles;
		}	

		#endregion

		#region Wait For Completion

		public static IEnumerator OnLoadingDone(
			AsyncOperationHandle<SceneInstance> operationHandle, Action action) {
			
			while ( operationHandle.Status != AsyncOperationStatus.Succeeded ) {
				yield return null;
			}
			
			action.Invoke();
		}
		
		public static IEnumerator OnAllHandlesColplete(
			List<AsyncOperationHandle<SceneInstance>> operationHandles, Action action) {

			foreach ( var operationHandle in operationHandles ) {
				while ( !operationHandle.IsDone ) {
					yield return null;
				}	
			}
			
			action.Invoke();			
		} 
		
		public static IEnumerator OnHandleColplete(
			AsyncOperationHandle<SceneInstance> operationHandle, Action action) {

			while ( !operationHandle.IsDone ) {
				yield return null;
			}
			
			action.Invoke();			
		} 
		

		#endregion
		
		// public static IEnumerator UnloadScenes(IEnumerable<GameSceneSO> scenesToUnload) {
		// 	
		// 	// todo start unloading process
		// 	
		// 	// todo wait until they are unloaded
		// 	
		// 	yield return null; 
		// }
		//
		// public static IEnumerator LoadScenes(IEnumerable<GameSceneSO> scenesToLoad, 
		// 	IEnumerable<Scene> sceneInstances) {
		//
		// 	// todo start loading
		// 	
		// 	// todo wait for completion
		// 	
		// 	
		// 	yield return null;
		// }

	}
}
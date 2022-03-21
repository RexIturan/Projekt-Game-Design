using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace GDP01.Util.Singelton {
	public static class ProviderHelper {
		private static bool IsActiveScene(this Scene scene) {
			return SceneManager.GetActiveScene().Equals(scene);
		}
		
		public static T TryGetAndCacheComponentOfTypeInActiveScene<T>(ref T cache)
			where T : Component {
			
			try {
				if ( cache.gameObject == null || cache.gameObject.scene.IsActiveScene() ) {
					cache = null;
				}
			}
			catch ( Exception e ) {
				Console.WriteLine(e);
				cache = null;
			}
				
			return cache ??= GetComponentOfTypeInActiveScene<T>();
		}

		public static T TryGetAndCacheObjectOfType<T>(ref T cache) where T : MonoBehaviour {
			try {
				if ( cache.gameObject == null ) {
					cache = null;
				}
			}
			catch ( Exception e ) {
				Console.WriteLine(e);
				cache = null;
			}
				
			return cache ??= GetObjectOfType<T>();
		}
			
		public static T GetComponentOfTypeInActiveScene<T>() where T : Component {

			var activeVersion = UnityEngine.Object.FindObjectsOfType<T>()
				.FirstOrDefault(behaviour => behaviour.gameObject.scene.IsActiveScene());
			
			var obj = activeVersion;
			if ( obj is null ) {
				Debug.LogError($"{typeof(T)} not Found, but was requested from GetObjectOfType");
			}
			return obj;
		}
		
		public static T GetObjectOfType<T>() where T : UnityEngine.Object {
			var obj = UnityEngine.Object.FindObjectOfType<T>();
			if ( obj is null ) {
				Debug.LogWarning($"{typeof(T)} not Found, but was requested from GetObjectOfType");
			}
			return obj;
		}

		public static T LoadData<T>(string name) {
			return Addressables.LoadAssetAsync<T>(name).WaitForCompletion();
		}

		public static T LoadAndCache<T>(ref T cache) {
			return cache ??= LoadData<T>(typeof(T).Name);
		}
	}
}
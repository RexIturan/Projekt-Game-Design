using System.Collections.Generic;
using UnityEngine;

namespace Util.Extensions {
	public static class GameObjectExtensions {
		
		public static void ClearGameObjectReferences(this List<GameObject> componentList){
			if ( componentList is { Count: > 0} ) {
				componentList.ForEach(GameObject.Destroy);
				componentList.Clear();
			}
		}
		
		public static void ClearMonoBehaviourGameObjectReferences<T>(this List<T> componentList)
			where T : MonoBehaviour {

			if ( componentList is { Count: > 0} ) {
				componentList.ForEach(component => Object.Destroy(component.gameObject));
				componentList.Clear();
			}
		}
	}
}
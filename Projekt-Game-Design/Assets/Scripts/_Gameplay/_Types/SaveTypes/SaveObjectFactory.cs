using System;
using SaveSystem.V2.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GDP01.Gameplay.SaveTypes {
	public abstract class SaveObjectFactory<T, D> : MonoBehaviour where T : ISaveState<D> where D : SaveObjectCreatorData {
		public static T Create(D data) {
			return Object.Instantiate(data.Prefab).gameObject.GetComponent<T>();
		}

		public static T CreateAndLoad(D data) {
			T type;
			
			try {
				type = Create(data);
				type.Load(data);
			}
			catch ( Exception e ) {
				Console.WriteLine(e);
				throw;
			}
			
			return type;
		}
	}
}
	

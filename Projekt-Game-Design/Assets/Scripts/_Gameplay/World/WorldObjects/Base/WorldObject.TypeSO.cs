using GDP01.Util;
using UnityEngine;

namespace WorldObjects {
	public partial class WorldObject {
		public class TypeSO : SerializableScriptableObject {
			public int id;
			public GameObject prefab;
			
			protected T ToComponentData<T>() where T : WorldObject.Data, new() {
				return new T {
					Id = id,
					Prefab = prefab,
					ReferenceData = this.ToReferenceData(),
				};
			}
		}
	}
}
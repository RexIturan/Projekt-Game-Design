using System.Collections.Generic;
using System.Linq;
using SaveSystem.V2.Data;
using UnityEngine;
using Util.Extensions;

namespace SaveSystem.V2.TestComponents {
	public class CollectableManager : MonoBehaviour, ISaveState<List<CollectableData>> {
		[SerializeField] private List<Collectable> collectables;
		[SerializeField] private Collectable collectablePrefab;
		[SerializeField] private Transform collectableParent;
		
		private Collectable CreateNewCollectable(CollectableData data) {
			var collectable = Instantiate(collectablePrefab, collectableParent);
			collectable.Load(data);
			return collectable;
		}
		
		public List<CollectableData> Save() {
			return collectables.Select(c => c.Save()).ToList();
		}

		public void Load(List<CollectableData> data) {
			collectables.ClearGameObjectReferences();
			collectables = new List<Collectable>();
			foreach ( var collectableData in data ) {
				collectables.Add(CreateNewCollectable(collectableData));	
			}
		}
	}
}
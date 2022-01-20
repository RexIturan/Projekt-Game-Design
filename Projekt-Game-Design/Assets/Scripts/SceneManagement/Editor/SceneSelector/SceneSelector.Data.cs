using System;
using System.Collections.Generic;
using System.Linq;
using SceneManagement.ScriptableObjects;
using UnityEngine;

namespace Editor.SceneSelector {
	public partial class SceneSelector {
		[Serializable]
		private class Item {
			public string guid;
			public int order = int.MaxValue;
			public bool isVisible = true;
			public Color color = Color.clear;

			[NonSerialized] public GameSceneSO gameSceneSO;
		}

		private class Styles {
			public GUIStyle item;
		}

		// todo check -> remove
		// private class Textures
		// {
		// 	public Texture2D visibilityOn;
		// 	public Texture2D visibilityOff;
		// }

		[Serializable]
		private class Storage : ISerializationCallbackReceiver {
			[SerializeField] public List<Item> items = new List<Item>();

			[NonSerialized]
			public Dictionary<string, Item> itemsMap = new Dictionary<string, Item>();

			void ISerializationCallbackReceiver.OnBeforeSerialize() {
				for ( int i = 0, count = items.Count; i < count; ++i ) {
					var item = items[i];
					item.order = i;
				}
			}

			void ISerializationCallbackReceiver.OnAfterDeserialize() {
				var sortedItems =items.OrderBy(x => x.order);
				foreach ( var item in sortedItems ) {
					itemsMap.Add(item.guid, item);
				}
			}
		}
	}
}
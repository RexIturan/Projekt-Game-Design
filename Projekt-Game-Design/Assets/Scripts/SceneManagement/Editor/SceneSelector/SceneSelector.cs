using System.Collections.Generic;
using System.Linq;
using SceneManagement.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using Editor.SceneSelector.SceneSelectorInternal;
using SceneType = SceneManagement.ScriptableObjects.GameSceneSO.GameSceneType;

namespace Editor.SceneSelector {
	public partial class SceneSelector : IHasCustomMenu {
		private const string KPreferencesKey = "uop1.SceneSelector.Preferences";
		private const int KItemContentLeftPadding = 32;

		private static readonly GUIContent KOpenPreferencesItemContent =
			new GUIContent("Open Preferences");

		private Styles _styles;
		private Storage _storage;
		private PreferencesWindow _preferencesWindow;
		private Vector2 _windowScrollPosition;
		private bool _hasEmptyItems;

		private List<Item> Items => _storage.items;
		private Dictionary<string, Item> ItemsMap => _storage.itemsMap;

		[MenuItem("Tools/Scene Selector")]
		private static void Open() {
			GetWindow<SceneSelector>();
		}

		private void OnEnable() {
			if ( _storage == null || _storage.items.Count == 0 || _storage.items.Any(item => item == null)) {
				_storage = new Storage();
				EditorPrefs.SetString(KPreferencesKey, "");	
			}
			
			titleContent.text = "Scene Selector";
			
			wantsMouseMove = true;
			LoadStorage();
			PopulateItems();
		}

		private void OnDisable() {
			if ( _preferencesWindow != null )
				_preferencesWindow.Close();
			SaveStorage();
		}

		private void OnGUI() {
			EnsureStyles();
			Helper.RepaintOnMouseMove(this);
			RemoveEmptyItemsIfRequired();
			DrawWindow();
		}

		private void DrawWindow() {
			using ( var scrollScope = new EditorGUILayout.ScrollViewScope(_windowScrollPosition) ) {
				GUILayout.Space(4.0f);
				DrawItems();
				_windowScrollPosition = scrollScope.scrollPosition;
			}
			
			if ( GUILayout.Button("Reset list") ) {
				//Force deletion of the storage
				_storage = new Storage();
				EditorPrefs.SetString(KPreferencesKey, "");

				OnEnable(); //search the project and populate the scene list again
			}
		}

		private void DrawItems() {
			SceneType[] typeOrder = new[] {
				SceneType.Initialisation,
				SceneType.PersistentManagers,
				SceneType.Control,
				SceneType.TacticsLevel,
				SceneType.Menu,
				SceneType.Art,
				SceneType.UI,
				SceneType.Test,
			};

			// draws items
			// orders items by type
			foreach ( var type in typeOrder ) {
				foreach ( var item in Items ) {
					if ( item.gameSceneSO.sceneType == type ) {
						DrawItem(item);
					}
				}
			}

			// foreach (var item in items)
			// {
			// 	DrawItem(item);
			// }
		}

		private void DrawItem(Item item) {
			if ( item.isVisible ) {
				var gameSceneSO = item.gameSceneSO;
				if ( gameSceneSO != null ) {
					if ( GUILayout.Button(gameSceneSO.name, _styles.item) ) {
						Helper.OpenSceneSafe(gameSceneSO);
					}

					var colorMarkerRect = GUILayoutUtility.GetLastRect();
					colorMarkerRect.width = colorMarkerRect.height;
					colorMarkerRect.x +=
						( _styles.item.padding.left - colorMarkerRect.width ) * 0.5f;
					Helper.DrawColorMarker(colorMarkerRect, item.color);
				}
				else {
					// In case GameSceneSO was removed (see RemoveEmptyItemsIfRequired)
					_hasEmptyItems = true;
				}
			}
		}

		private void LoadStorage() {
			_storage = new Storage();
			if ( EditorPrefs.HasKey(KPreferencesKey) ) {
				var preferencesJSON = EditorPrefs.GetString(KPreferencesKey);
				EditorJsonUtility.FromJsonOverwrite(preferencesJSON, _storage);
			}
		}

		private void SaveStorage() {
			var preferencesJSON = EditorJsonUtility.ToJson(_storage);
			EditorPrefs.SetString(KPreferencesKey, preferencesJSON);
		}

		private void PopulateItems() {
			var gameSceneSOs = new List<GameSceneSO>();
			Helper.FindAssetsByType(gameSceneSOs);

			foreach ( var gameSceneSO in gameSceneSOs ) {
				if ( AssetDatabase.TryGetGUIDAndLocalFileIdentifier(gameSceneSO, out var guid,
					out long _) ) {
					if ( ItemsMap.TryGetValue(guid, out var item) ) {
						item.gameSceneSO = gameSceneSO;
					}
					else {
						item = new Item() {
							gameSceneSO = gameSceneSO,
							guid = guid,
							color = Helper.GetDefaultColor(gameSceneSO)
						};

						Items.Add(item);
						ItemsMap.Add(guid, item);
					}
				}
			}
		}

		private void RemoveEmptyItemsIfRequired() {
			if ( _hasEmptyItems ) {
				for ( int i = Items.Count - 1; i >= 0; --i ) {
					var sceneItem = Items[i];
					if ( sceneItem == null || sceneItem.gameSceneSO == null ) {
						Items.RemoveAt(i);
						if ( sceneItem != null ) ItemsMap.Remove(sceneItem.guid);
					}
				}
			}

			_hasEmptyItems = false;
		}

		private void EnsureStyles() {
			if ( _styles == null ) {
				_styles = new Styles();

				_styles.item = "MenuItem";
				_styles.item.padding.left = KItemContentLeftPadding;
			}
		}

		private void OpenPreferences() {
			_preferencesWindow = PreferencesWindow.Open(this);
		}

		void IHasCustomMenu.AddItemsToMenu(GenericMenu menu) {
			menu.AddItem(KOpenPreferencesItemContent, false, OpenPreferences);
		}
	}
}
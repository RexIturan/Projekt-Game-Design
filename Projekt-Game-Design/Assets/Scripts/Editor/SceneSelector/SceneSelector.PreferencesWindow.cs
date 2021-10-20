using System.Collections.Generic;
using Editor.SceneSelector.SceneSelectorInternal;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Editor.SceneSelector {
	public partial class SceneSelector : EditorWindow {
		private class PreferencesWindow : EditorWindow {
			private class PreferencesWindowStyles {
				public GUIStyle itemBorder;
				public GUIStyle buttonVisibilityOn;
				public GUIStyle buttonVisibilityOff;
			}

			private const string KWindowCaption = "Scene Selector Preferences";
			private const float KHeaderHeight = 0.0f;
			private const float KItemHeight = 24.0f;
			private const float KVisibilityButtonSize = 16.0f;

			public static float kColorMarkerFieldSize =
				Mathf.Ceil(Helper.KColorMarkerNormalSize * 1.41f + 8.0f);

			private static readonly Color KItemBorderColor = new Color(1.0f, 1.0f, 1.0f, 0.16f);

			private SceneSelector _owner;
			private ColorSelectorWindow _colorSelectorWindow;
			private ReorderableList _itemsReorderableList;
			private PreferencesWindowStyles _styles;
			private Vector2 _windowScrollPosition;

			private List<Item> Items => _owner._storage.items;

			public static PreferencesWindow Open(SceneSelector owner) {
				var window = GetWindow<PreferencesWindow>(true, KWindowCaption, true);
				window.Init(owner);
				return window;
			}

			private void OnEnable() {
				wantsMouseMove = true;
			}

			private void OnDisable() {
				_owner.SaveStorage();
				if ( _colorSelectorWindow != null )
					_colorSelectorWindow.Close();
			}

			private void OnGUI() {
				EnsureStyles();
				Helper.RepaintOnMouseMove(this);
				DrawWindow();
			}

			public void RepaintAll() {
				RepaintOwner();
				Repaint();
			}

			private void Init(SceneSelector owner) {
				_owner = owner;
				CreateReorderableList();
			}

			private void CreateReorderableList() {
				_itemsReorderableList =
					new ReorderableList(Items, typeof(Item), true, true, false, false);
				_itemsReorderableList.drawElementCallback = DrawItem;
				_itemsReorderableList.drawElementBackgroundCallback = DrawItemBackground;
				_itemsReorderableList.onReorderCallback = OnReorder;
				_itemsReorderableList.headerHeight = KHeaderHeight;
				_itemsReorderableList.elementHeight = KItemHeight;
			}

			private void DrawWindow() {
				using ( var scrollScope =
					new EditorGUILayout.ScrollViewScope(_windowScrollPosition) ) {
					GUILayout.Space(4.0f);
					_itemsReorderableList.DoLayoutList();
					_windowScrollPosition = scrollScope.scrollPosition;
				}
			}

			private void DrawItem(Rect rect, int index, bool isActive, bool isFocused) {
				var item = Items[index];
				var gameScene = item.gameSceneSO;
				if ( gameScene != null ) {
					var colorMarkerRect = rect;
					colorMarkerRect.width = colorMarkerRect.height;

					if ( Helper.DrawColorMarker(colorMarkerRect, item.color, true, true) ) {
						var colorSelectorRect = GUIUtility.GUIToScreenRect(colorMarkerRect);
						_colorSelectorWindow =
							ColorSelectorWindow.Open(colorSelectorRect, this, item);
					}

					var itemLabelRect = rect;
					itemLabelRect.x += colorMarkerRect.width;
					itemLabelRect.width -= KVisibilityButtonSize + colorMarkerRect.width;

					GUI.Label(itemLabelRect, gameScene.name);

					var visibilityButtonRect = new Rect(rect);
					visibilityButtonRect.width = KVisibilityButtonSize;
					visibilityButtonRect.height = KVisibilityButtonSize;
					visibilityButtonRect.x = itemLabelRect.x + itemLabelRect.width;
					visibilityButtonRect.y += ( rect.height - visibilityButtonRect.height ) * 0.5f;

					var visibilityStyle = item.isVisible
						? _styles.buttonVisibilityOn
						: _styles.buttonVisibilityOff;

					if ( GUI.Button(visibilityButtonRect, GUIContent.none, visibilityStyle) ) {
						item.isVisible = !item.isVisible;
						RepaintOwner();
					}
				}
			}

			private void DrawItemBackground(Rect rect, int index, bool isActive, bool isFocused) {
				ReorderableList.defaultBehaviours.DrawElementBackground(rect, index, isActive,
					isFocused, true);
				using ( Helper.ReplaceColor.With(KItemBorderColor) ) {
					GUI.Box(rect, GUIContent.none, _styles.itemBorder);
				}
			}

			private void OnReorder(ReorderableList _) {
				RepaintOwner();
			}

			private void RepaintOwner() {
				_owner.Repaint();
			}

			private void EnsureStyles() {
				if ( _styles == null ) {
					_styles = new PreferencesWindowStyles {
						itemBorder = new GUIStyle(GUI.skin.GetStyle("HelpBox")),
						buttonVisibilityOn = new GUIStyle(GUI.skin.label) {
							padding = new RectOffset(0, 0, 0, 0),
							normal = {
								background = EditorGUIUtility.FindTexture("d_scenevis_visible")
							},
							hover = {
								background = EditorGUIUtility.FindTexture("d_scenevis_visible_hover")
							}
						},
						buttonVisibilityOff = new GUIStyle(GUI.skin.label) {
							padding = new RectOffset(0, 0, 0, 0),
							normal = {
								background = EditorGUIUtility.FindTexture("d_scenevis_hidden")
							},
							hover = {
								background = EditorGUIUtility.FindTexture("d_scenevis_hidden_hover")
							}
						}
					};
				}
			}
		}
	}
}
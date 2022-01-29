using System;
using System.Collections.Generic;
using SceneManagement.ScriptableObjects;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor.SceneSelector {
	namespace SceneSelectorInternal {
		internal static class KeyValuePairExtension {
			public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key,
				out T2 value) {
				key = tuple.Key;
				value = tuple.Value;
			}
		}

		internal static class Helper {
			public const float KColorMarkerNormalSize = 4.0f;
			public const float KColorMarkerHoveredSize = 6.0f;
			public static readonly Color KColorMarkerDarkTint = Color.gray;
			public static readonly Color KColorMarkerLightTint = new Color(1.0f, 1.0f, 1.0f, 0.32f);

			public struct ReplaceColor : IDisposable {
				public static ReplaceColor With(Color color) => new ReplaceColor(color);

				private Color _oldColor;

				private ReplaceColor(Color color) {
					_oldColor = GUI.color;
					GUI.color = color;
				}

				void IDisposable.Dispose() => GUI.color = _oldColor;
			}

			private static readonly Dictionary<Type, Color> KDefaultMarkerColors =
				new Dictionary<Type, Color>() {
					{ typeof(PersistentManagersSO), Color.white },
					{ typeof(ControlSceneSO), new Color(0.1f,0.7f, 0.4f) },
					{ typeof(TacticsSO), new Color(0.9f,0.2f, 0.2f) },
					{ typeof(MenuSO), new Color(0.25f,0.25f, 1) },
					{ typeof(UISceneSO), new Color(1,0.5f,0) },
					{ typeof(TestSceneSO), Color.black },
				};

			public static void RepaintOnMouseMove(EditorWindow window) {
				if ( Event.current.type == EventType.MouseMove )
					window.Repaint();
			}

			public static bool DrawColorMarker(Rect rect, Color color, bool isClickable = false,
				bool isHoverable = false) {
				bool isClicked = false;
				if ( isClickable )
					isClicked = GUI.Button(rect, GUIContent.none, GUIStyle.none);

				var currentEvent = Event.current;
				var isHovered = isHoverable && rect.Contains(currentEvent.mousePosition);
				var targetSize = isHovered ? KColorMarkerHoveredSize : KColorMarkerNormalSize;

				var size = rect.size;
				rect.size = new Vector2(targetSize, targetSize);
				rect.position += ( size - rect.size ) * 0.5f;

				Rect shadowRect = rect;
				shadowRect.position -= Vector2.one;
				shadowRect.size += Vector2.one;
				Rect lightRect = rect;
				lightRect.size += Vector2.one;

				GUIUtility.RotateAroundPivot(45.0f, rect.center);
				EditorGUI.DrawRect(shadowRect, color * KColorMarkerDarkTint);
				EditorGUI.DrawRect(lightRect, KColorMarkerLightTint);
				EditorGUI.DrawRect(rect, color);
				GUIUtility.RotateAroundPivot(-45.0f, rect.center);

				return isClicked;
			}

			public static void OpenSceneSafe(GameSceneSO gameSceneSO) {
				if ( EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() ) {
					EditorSceneManager.OpenScene(
						AssetDatabase.GetAssetPath(gameSceneSO.sceneReference.editorAsset));

					foreach ( var additionalScene in gameSceneSO.loadAdditional ) {
						EditorSceneManager.OpenScene(
							AssetDatabase.GetAssetPath(additionalScene.sceneReference.editorAsset), OpenSceneMode.Additive);
					}
				}
			}

			public static Color GetDefaultColor(GameSceneSO gameScene) {
				var type = gameScene.GetType();
				if ( KDefaultMarkerColors.TryGetValue(type, out var color) )
					return color;
				return Color.red;
			}

			public static int FindAssetsByType<T>(List<T> assets) where T : UnityEngine.Object {
				int foundAssetsCount = 0;
				var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
				for ( int i = 0, count = guids.Length; i < count; ++i ) {
					var path = AssetDatabase.GUIDToAssetPath(guids[i]);
					T asset = AssetDatabase.LoadAssetAtPath<T>(path);
					if ( asset != null ) {
						assets.Add(asset);
						foundAssetsCount += 1;
					}
				}

				return foundAssetsCount;
			}
		}
	}
}
using Editor.SceneSelector.SceneSelectorInternal;
using UnityEditor;
using UnityEngine;

namespace Editor.SceneSelector {
	public partial class SceneSelector {
		private class ColorSelectorWindow : EditorWindow {
			private static readonly float
				KCellSize = PreferencesWindow.kColorMarkerFieldSize * 2.0f;

			private static readonly Color KCellBackColor = new Color(0.0f, 0.0f, 0.0f, 0.1f);
			private static readonly Vector2 KCellOffset = new Vector2(1.0f, 1.0f);
			private static readonly Vector2Int KCount = new Vector2Int(5, 5);

			private PreferencesWindow _owner;
			private Color[,] _colors;
			private Item _item;


			public static ColorSelectorWindow Open(Rect rect, PreferencesWindow owner, Item item) {
				var window = CreateInstance<ColorSelectorWindow>();
				window.Init(rect, owner, item);
				return window;
			}


			private void Init(Rect rect, PreferencesWindow owner, Item item) {
				var size = ( Vector2 )KCount * KCellSize;
				ShowAsDropDown(rect, size);
				_owner = owner;
				_item = item;
			}


			private void OnEnable() {
				wantsMouseMove = true;
				InitColors();
			}


			private void OnGUI() {
				Helper.RepaintOnMouseMove(this);
				DrawMarkers();
			}


			private void DrawMarkers() {
				var size = new Vector2(KCellSize, KCellSize);
				for ( int x = 0; x < KCount.x; ++x ) {
					for ( int y = 0; y < KCount.y; ++y ) {
						var color = _colors[x, y];
						var rectPos = size * new Vector2(x, y);
						var rect = new Rect(rectPos, size);
						{
							var cellBackRect = rect;
							cellBackRect.position += KCellOffset;
							cellBackRect.size -= KCellOffset * 2.0f;
							EditorGUI.DrawRect(cellBackRect, KCellBackColor);
						}
						if ( Helper.DrawColorMarker(rect, color, true, true) ) {
							_item.color = color;
							_owner.RepaintAll();
							Close();
						}
					}
				}
			}


			private void InitColors() {
				var count = KCount.x * KCount.y;
				_colors = new Color[KCount.x, KCount.y];
				for ( int x = 0; x < KCount.x; ++x ) {
					var h = x * KCount.y;
					for ( int y = 0; y < KCount.y; ++y ) {
						float hue = ( float )( h + y ) / count;
						_colors[x, y] = Color.HSVToRGB(hue, 1.0f, 1.0f);
					}
				}
			}
		}
	}
}
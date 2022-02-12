using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UOP1.StateMachine.ScriptableObjects;

namespace UOP1.StateMachine.Editor
{
	internal class TransitionTableEditorWindow : EditorWindow
	{
		private static TransitionTableEditorWindow _window;
		// private static readonly string _uxmlPath = "Assets/Scripts/StateMachine/Editor/TransitionTableEditorWindow.uxml";
		// private static readonly string _ussPath = "Assets/Scripts/StateMachine/Editor/TransitionTableEditorWindow.uss";
		private static readonly string _ussFilter = "TransitionTableEditorWindow t:StyleSheet";
		private static readonly string _uxmlFilter = "TransitionTableEditorWindow t:VisualTreeAsset";
		private bool _doRefresh;

		private UnityEditor.Editor _transitionTableEditor;

		[MenuItem("Transition Table Editor", menuItem = "Tools/Transition Table Editor")]
		internal static void Display()
		{
			if (_window == null)
				_window = GetWindow<TransitionTableEditorWindow>("Transition Table Editor");

			_window.Show();
		}

		private void OnEnable() {
			var ussGUID = AssetDatabase.FindAssets(_ussFilter);
			var uxmlGUID = AssetDatabase.FindAssets(_uxmlFilter);

			var uxmlPath = AssetDatabase.GUIDToAssetPath(uxmlGUID.Length > 0 ? uxmlGUID[0] : "");
			var ussPath = AssetDatabase.GUIDToAssetPath(ussGUID.Length > 0 ? ussGUID[0] : "");
			
			var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
			var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(ussPath);

			if (visualTree is null || styleSheet is null) {
				Debug.LogError("Transition Table Editopr cant find umxl file or uss file");
				return;
			}
			
			rootVisualElement.Add(visualTree.CloneTree());

			string labelClass = $"label-{(EditorGUIUtility.isProSkin ? "pro" : "personal")}";
			rootVisualElement.Query<Label>().Build().ForEach(label => label.AddToClassList(labelClass));

			rootVisualElement.styleSheets.Add(styleSheet);

			minSize = new Vector2(360, 260);

			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private void OnDisable()
		{
			EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
		}

		private void OnPlayModeStateChanged(PlayModeStateChange obj)
		{
			if (obj == PlayModeStateChange.EnteredPlayMode)
				_doRefresh = true;
		}

		/// <summary>
		/// Update list every time we gain focus
		/// </summary>
		private void OnFocus()
		{
			// Calling CreateListView() from here when the window is docked
			// throws a NullReferenceException in UnityEditor.DockArea:OnEnable().
			if (_doRefresh == false)
				_doRefresh = true;
		}

		private void OnLostFocus()
		{
			ListView listView = rootVisualElement.Q<ListView>(className: "table-list");
			// listView.onSelectionChanged -= OnListSelectionChanged;
			listView.onSelectionChange -= OnListSelectionChanged;
		}

		private void Update() {
			if (!_doRefresh)
				return;

			CreateListView();
			_doRefresh = false;
		}

		private void CreateListView()
		{
			var assets = FindAssets();
			ListView listView = rootVisualElement.Q<ListView>(className: "table-list");

			listView.makeItem = null;
			listView.bindItem = null;

			listView.itemsSource = assets;
			listView.itemHeight = 16;
			string labelClass = $"label-{(EditorGUIUtility.isProSkin ? "pro" : "personal")}";
			listView.makeItem = () =>
			{
				var label = new Label();
				label.AddToClassList(labelClass);
				return label;
			};
			listView.bindItem = (element, i) => {
				( ( Label )element ).text = assets[i].name;
				if ( NullFieldFinderHelper.checkForNullValues(assets[i]) ) {
					element.style.color = new StyleColor(Color.red);
				}
			};
			listView.selectionType = SelectionType.Single;

			listView.onSelectionChange -= OnListSelectionChanged;
			listView.onSelectionChange += OnListSelectionChanged;

			if (_transitionTableEditor && _transitionTableEditor.target)
				listView.selectedIndex = System.Array.IndexOf(assets, _transitionTableEditor.target);
		}

		private void OnListSelectionChanged(IEnumerable<object> enumerable)
		{
			IMGUIContainer editor = rootVisualElement.Q<IMGUIContainer>(className: "table-editor");
			editor.onGUIHandler = null;
			if ( enumerable is List<object> list ) {
				if (list.Count == 0)
					return;

				var table = (TransitionTableSO)list[0];
				if (table == null)
					return;
				
				if (_transitionTableEditor == null)
					_transitionTableEditor = UnityEditor.Editor.CreateEditor(table, typeof(TransitionTableEditor));
				else if (_transitionTableEditor.target != table)
					UnityEditor.Editor.CreateCachedEditor(table, typeof(TransitionTableEditor), ref _transitionTableEditor);
			}
			
			//todo move this in the if as well?
			
			editor.onGUIHandler = () =>
			{
				if (!_transitionTableEditor.target)
				{
					editor.onGUIHandler = null;
					return;
				}

				ListView listView = rootVisualElement.Q<ListView>(className: "table-list");
				if ((Object)listView.selectedItem != _transitionTableEditor.target)
				{
					var i = listView.itemsSource.IndexOf(_transitionTableEditor.target);
					listView.selectedIndex = i;
					if (i < 0)
					{
						editor.onGUIHandler = null;
						return;
					}
				}

				_transitionTableEditor.OnInspectorGUI();
			};
		}


		private TransitionTableSO[] FindAssets()
		{
			var guids = AssetDatabase.FindAssets($"t:{nameof(TransitionTableSO)}");
			var assets = new TransitionTableSO[guids.Length];
			for (int i = 0; i < guids.Length; i++)
				assets[i] = AssetDatabase.LoadAssetAtPath<TransitionTableSO>(AssetDatabase.GUIDToAssetPath(guids[i]));

			return assets;
		}
	}
}

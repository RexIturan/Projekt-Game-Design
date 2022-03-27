using System;
using Events.ScriptableObjects;
using GDP01.UI.Components;
using LevelEditor.EventChannels;
using UnityEngine;
using UnityEngine.UIElements;
using static LevelEditor.LevelEditor;

namespace UI {
	public class LevelEditorUIController : MonoBehaviour {
		[Serializable]
		private struct LevelEditorUIGraphicsData {
			public Sprite Select;
			public Sprite Paint;
			public Sprite Box;
			
			public Sprite Tiles;
			public Sprite Character_Player;
			public Sprite Character_Enemy;
			public Sprite Item;
			public Sprite Door;
			public Sprite Switch;
			public Sprite Effect;
		}
		
		private const string layerActionBarName = "ActionBar-Right";
		private const string modeActionBarName = "ActionBar-Left";
			
		// [SerializeField] private LevelEditor.LevelEditor levelEditor;

		[Header("Receiving Events On")] [SerializeField]
		private BoolEventChannelSO visibilityMenuEventChannel;
		[SerializeField] private LevelEditorStateEventChannel levelEditorStateUpdateEC; 

		[Header("Sending Events On")]
		//input
		[SerializeField]
		private VoidEventChannelSO enableMenuInput;

		[SerializeField] private VoidEventChannelSO enableGamplayInput;

		// save manager
		[SerializeField] private VoidEventChannelSO saveLevel;

		[SerializeField] private VoidEventChannelSO loadLevel;

		//level editor
		[SerializeField] private LevelEditorLayerEventChannel levelEditorLayerEC;
		[SerializeField] private LevelEditorModeEventChannel levelEditorModeEC;

		[Header("Graphics"), SerializeField] private LevelEditorUIGraphicsData _graphicsData; 
		
///// Private VAriables ////////////////////////////////////////////////////////////////////////////

		// container
		private VisualElement _levelEditorContainer;
		
		private Button _selectModeButton;
		private Button _paintModeButton;
		private Button _boxModeButton;

		private Button _menuButton;

		private ActionBar _layerSelectionActionBar;
		private ActionBar _modeSelectionActionBar;

///// Private Functions ////////////////////////////////////////////////////////////////////////////
		
		private void SetLevelEditorMode(object[] obj) {
			SetLevelEditorMode(( EditorMode )obj[0]);
		}
		
		private void SetLevelEditorLayer(object[] obj) {
			SetLevelEditorLayer(( LayerType )obj[0]);
		}

		private void SetLevelEditorLayer(LayerType layerType) {
			levelEditorLayerEC.RaiseEvent(layerType);
		} 

		private void SetLevelEditorMode(EditorMode editorMode) {
			levelEditorModeEC.RaiseEvent(editorMode);
		}

		// private void OnDisable() {
		//     selectModeButton.clicked -= HandleSelectModusButtonClicked;
		//     paintModeButton.clicked -= HandlePaintModusButtonClicked;
		//     boxModeButton.clicked -= HandleBoxModusButtonClicked;
		//     fillModeButton.clicked -= HandleFillModusButtonClicked;
		// }

		// void MainMenuButtonPressed()
		// {
		//     // Szene laden
		//     SceneManager.LoadScene("MainMenu");
		// }
		//
		// void QuitGame()
		// {
		//     // Spiel beenden
		//     Application.Quit();
		// }

		void SetMenuVisibility(bool value) {
			if ( value ) {
				ShowMenu();
			}
			else {
				HideMenu();
			}
		}

		void ShowMenu() {
			enableMenuInput.RaiseEvent();
			// Einstellungen ausblenden und Menü zeigen
			_levelEditorContainer.style.display = DisplayStyle.None;
			
			//todo use screen manager
		}

		void HideMenu() {
			enableGamplayInput.RaiseEvent();
			// Einstellungen ausblenden und Menü zeigen
			_levelEditorContainer.style.display = DisplayStyle.Flex;
			//todo use screen manager
		}

		private void BindElements() {
			var root = GetComponent<UIDocument>().rootVisualElement;
			_levelEditorContainer = root.Q<VisualElement>("LevelEditorContainer");
			_selectModeButton = root.Q<Button>("select");
			_paintModeButton = root.Q<Button>("paint");
			_boxModeButton = root.Q<Button>("box");
			// _menuButton = root.Q<Button>("menuButton");
			
			_selectModeButton.clicked += HandleSelectModusButtonClicked;
			_paintModeButton.clicked += HandlePaintModusButtonClicked;
			_boxModeButton.clicked += HandleBoxModusButtonClicked;
			// _menuButton.clicked += ShowMenu;

			_layerSelectionActionBar = root.Q<ActionBar>(layerActionBarName);
			_modeSelectionActionBar = root.Q<ActionBar>(modeActionBarName);
		}

		private void UnbindElements() {
			_selectModeButton.clicked -= HandleSelectModusButtonClicked;
			_paintModeButton.clicked -= HandlePaintModusButtonClicked;
			_boxModeButton.clicked -= HandleBoxModusButtonClicked;
			// _menuButton.clicked -= ShowMenu;

			_levelEditorContainer = null;
			_selectModeButton = null;
			_paintModeButton = null;
			_boxModeButton = null;
			// _menuButton = null;
			
			_layerSelectionActionBar = null;
			_modeSelectionActionBar = null;
		}

		private void UpdateActionBars(EditorState editorState) {
			
			//Mode
			SetupActionBarButton(_modeSelectionActionBar, 
				0, "Select", _graphicsData.Select, 
				SetLevelEditorMode, new object[]{EditorMode.Select});

			SetupActionBarButton(_modeSelectionActionBar, 
				1, "Paint", _graphicsData.Paint, 
				SetLevelEditorMode, new object[]{EditorMode.Paint});
			
			SetupActionBarButton(_modeSelectionActionBar, 
				2, "Box", _graphicsData.Box, 
				SetLevelEditorMode, new object[]{EditorMode.Box});
			
			//Layer
			SetupActionBarButton(_layerSelectionActionBar, 
				0, "Tiles", _graphicsData.Tiles, 
				SetLevelEditorLayer, new object[]{LayerType.Tile});
			
			SetupActionBarButton(_layerSelectionActionBar, 
				1, "Player Char", _graphicsData.Character_Player, 
				SetLevelEditorLayer, new object[]{LayerType.Character_Player});
			
			SetupActionBarButton(_layerSelectionActionBar, 
				2, "Enemy Char", _graphicsData.Character_Enemy, 
				SetLevelEditorLayer, new object[]{LayerType.Character_Enemy});
			
			SetupActionBarButton(_layerSelectionActionBar, 
				3, "Item", _graphicsData.Item, 
				SetLevelEditorLayer, new object[]{LayerType.Item});
			
			SetupActionBarButton(_layerSelectionActionBar, 
				4, "Door", _graphicsData.Door, 
				SetLevelEditorLayer, new object[]{LayerType.Door});
			
			SetupActionBarButton(_layerSelectionActionBar, 
				5, "Switch", _graphicsData.Switch, 
				SetLevelEditorLayer, new object[]{LayerType.Switch});
			
			SetupActionBarButton(_layerSelectionActionBar, 
				6, "Effect", _graphicsData.Effect, 
				SetLevelEditorLayer, new object[]{LayerType.Effect});
		}

		private void SetupActionBarButton(ActionBar actionBar, int i, string name, Sprite image, Action<object[]> callback, object[] args) {
			var buttons = actionBar.actionButtons;
			buttons[i].UnbindOnClickedAction();
			buttons[i].ActionText = name;
			buttons[i].ImageData = image;
			buttons[i].BindOnClickedAction(callback, args);
			
			buttons[i].UpdateComponent();
		}

///// Callbacks ////////////////////////////////////////////////////////////////////////////////////

		void HandleSelectModusButtonClicked() {
			SetLevelEditorMode(EditorMode.Select);
		}

		void HandlePaintModusButtonClicked() {
			SetLevelEditorMode(EditorMode.Paint);
		}

		void HandleBoxModusButtonClicked() {
			SetLevelEditorMode(EditorMode.Box);
		}

		private void HandleSaveGame() {
			saveLevel.RaiseEvent();
		}

		private void HandleLoadGame() {
			loadLevel.RaiseEvent();
		}
		
		private void HandleLevelEditorStateChaged(EditorState editorState) {
			//update action bars
			UpdateActionBars(editorState);
		}

///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void OnEnable() {
			BindElements();

			// subscribe to input events
			visibilityMenuEventChannel.OnEventRaised += SetMenuVisibility;
			levelEditorStateUpdateEC.OnEventRaised += HandleLevelEditorStateChaged;
		}

		private void OnDisable() {
			UnbindElements();

			visibilityMenuEventChannel.OnEventRaised -= SetMenuVisibility;
			levelEditorStateUpdateEC.OnEventRaised -= HandleLevelEditorStateChaged;
		}
	}
}
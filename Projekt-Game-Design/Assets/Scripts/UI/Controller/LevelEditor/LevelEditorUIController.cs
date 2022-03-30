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
		
		[Serializable]
		private struct SaveControlButtonNames {
			public string createNew;
			public string saveV1;
			public string saveV2;
			public string loadV1;
			public string loadV2;
		}
		
		

		[Header("Receiving Events On")] 
		[SerializeField] private LevelEditorStateEventChannel levelEditorStateUpdateEC; 

		[Header("Sending Events On")]
		//old
		//input todo remove
		[SerializeField] private VoidEventChannelSO enableMenuInput;
		[SerializeField] private VoidEventChannelSO enableGamplayInput;
		// save manager
		[SerializeField] private VoidEventChannelSO saveLevel;
		[SerializeField] private VoidEventChannelSO loadLevel;

		//NEW
		//level editor
		[SerializeField] private LevelEditorLayerEventChannel levelEditorLayerEC;
		[SerializeField] private LevelEditorModeEventChannel levelEditorModeEC;

		[Header("Graphics"), SerializeField] private LevelEditorUIGraphicsData _graphicsData; 
		
		
///// Private VAriables ////////////////////////////////////////////////////////////////////////////

		private readonly SaveControlButtonNames saveControlButtonNames = new SaveControlButtonNames {
			createNew = "CreateNew",
			saveV1 = "SaveV1",
			saveV2 = "SaveV2",
			loadV1 = "LoadV1",
			loadV2 = "LoadV2",
		};

		private const string layerActionBarName = "ActionBar-Right";
		private const string modeActionBarName = "ActionBar-Left";

		//UI Elements
		private Button createNewLevelButton;
		private Button saveV1Button;
		private Button saveV2Button;
		private Button loadV1Button;
		private Button loadV2Button;

		private ActionBar _layerSelectionActionBar;
		private ActionBar _modeSelectionActionBar;

///// Private Functions ////////////////////////////////////////////////////////////////////////////
		
		private void UnbindButton(ref Button button, Action action) {
			if ( button is { } ) {
				button.clicked -= action;
				button = null;
			} else {
				Debug.LogError($"UnbindButton\n{name} Button not Found.");
			}
		}

		private void BindButton(ref Button button, VisualElement root, string name, Action action) {
			button = root.Q<Button>(name);
			if ( button is { } ) {
				button.clicked += action;	
			} else {
				Debug.LogError($"BindButton\n{name} Button not Found.");
			}
		} 

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

		private void BindElements() {
			var root = GetComponent<UIDocument>().rootVisualElement;
			
			//bind buttons
			BindButton(ref createNewLevelButton, root, saveControlButtonNames.createNew, HandleCreateNew);
			BindButton(ref saveV1Button, root, saveControlButtonNames.saveV1, HandleSaveV1);
			BindButton(ref saveV2Button, root, saveControlButtonNames.saveV2, HandleSaveV2);
			BindButton(ref loadV1Button, root, saveControlButtonNames.loadV1, HandleLoadV1);
			BindButton(ref loadV2Button, root, saveControlButtonNames.loadV2, HandleLoadV2);
			
			
			_layerSelectionActionBar = root.Q<ActionBar>(layerActionBarName);
			_modeSelectionActionBar = root.Q<ActionBar>(modeActionBarName);
		}

		private void UnbindElements() {
			_layerSelectionActionBar = null;
			_modeSelectionActionBar = null;
			
			UnbindButton(ref createNewLevelButton, HandleCreateNew);
			UnbindButton(ref saveV1Button, HandleSaveV1);
			UnbindButton(ref saveV2Button, HandleSaveV2);
			UnbindButton(ref loadV1Button, HandleLoadV1);
			UnbindButton(ref loadV2Button, HandleLoadV2);
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

		private void HandleCreateNew() {
			Debug.Log("Handle CreateNew");
			//TODO implement
			throw new NotImplementedException();
		}
		
		private void HandleSaveV1() {
			Debug.Log("Handle SaveV1");
			saveLevel.RaiseEvent();
			//TODO specific level
		}
		
		private void HandleSaveV2() {
			Debug.Log("Handle SaveV2");
			//TODO implement
			throw new NotImplementedException();
		}
		
		private void HandleLoadV1() {
			Debug.Log("Handle LoadV1");
			loadLevel.RaiseEvent();
			//TODO specific level
		}
		
		private void HandleLoadV2() {
			Debug.Log("Handle LoadV2");
			//TODO implement
			throw new NotImplementedException();
		}

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
			levelEditorStateUpdateEC.OnEventRaised += HandleLevelEditorStateChaged;
		}

		private void OnDisable() {
			UnbindElements();

			levelEditorStateUpdateEC.OnEventRaised -= HandleLevelEditorStateChaged;
		}
	}
}
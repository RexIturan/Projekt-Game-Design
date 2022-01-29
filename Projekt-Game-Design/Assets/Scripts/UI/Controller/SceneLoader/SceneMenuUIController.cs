using System;
using System.Collections.Generic;
using System.Linq;
using GDP01.Util.Util.UI;
using SaveSystem;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Util.Logger;

namespace GDP01.UI.Controller.SceneLoader {
	public class SceneMenuUIController : MonoBehaviour {
		[Serializable]
		private class SceneElement {
			public string name;
			public VisualElement element;
		}
		
		[Serializable]
		private class SceneLoaderElements {
			public SceneElement titleLabel = new SceneElement {
				name = "SceneMenuTitleLabel",
				element = null
			};
			
			public SceneElement buttonContainer = new SceneElement {
				name = "ButtonContainer",
				element = null
			};
		}
		
///// Serialised Fields ////////////////////////////////////////////////////////////////////////////		
		[Header("Scene EC")] 
		[SerializeField] private LoadEventChannelSO loadLocationEC;
		[SerializeField] private GameSceneSO[] sceneToLoad;
		
		[Header("Settings")]
		[SerializeField] private bool localRoot = false;

		[Header("Element Settings")] 
		[SerializeField] private SceneLoaderElements elements;
		
///// Private Variables ////////////////////////////////////////////////////////////////////////////
		
		private SaveManager _saveSystem;
		private CustomLogger logger;

		private VisualElement root;

		private List<Button> loadButtons;
		private Dictionary<Button, string> buttonFilenameDict;
		private Dictionary<Button, Action> buttonActionDict;

///// Properties ///////////////////////////////////////////////////////////////////////////////////

		private Dictionary<Button, string> ButtonFilenameDict => buttonFilenameDict ??= new Dictionary<Button, string>();
		private Dictionary<Button, Action> ButtonActionDict => buttonActionDict ??= new Dictionary<Button, Action>();

		private VisualElement Root {
			get {
				if ( root == null ) {
					if ( localRoot ) {
						root = GetComponent<UIDocument>().rootVisualElement;
					} else {
						var insertPanel = GetComponent<InsertPanel>();
						root = insertPanel.Root;
					}

					if ( root == null ) {
						Debug.LogError("No Root Element Found");
					}
				}
				return root;
			}
		}

///// Private Functions ////////////////////////////////////////////////////////////////////////////
		
		private void UnbindButton(Button button, Action action) {
			if ( button is { } ) {
				button.clicked -= action;	
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

		private void BindElement(ref SceneElement sceneElement, VisualElement root) {
			sceneElement.element = root.Q(sceneElement.name);
			if ( sceneElement.element is null ) {
				Debug.LogError($"BindElement\n{sceneElement.name} Element not Found.");
			}
		}
		
		private void BindStaticElements() {
			BindElement(ref elements.buttonContainer, Root);
			BindElement(ref elements.titleLabel, Root);
		}

		private void UnbindStaticElements() {
			
		}

		private void BindLoadButtonActions() {
			ButtonActionDict.Clear();
			foreach ( var button in loadButtons ) {
				ButtonActionDict.Add(button, () => HandleTryLoadLevel(ButtonFilenameDict[button], sceneToLoad));
				button.clicked += ButtonActionDict[button];
			}
		}

		private void UnbindLoadButtonActions() {
			foreach ( var button in loadButtons ) {
				button.clicked -= ButtonActionDict[button];
			}
			ButtonActionDict.Clear();
		}

		private void CreateLoadLevelButtons(string[] filenames) {

			logger.NewLog("SceneMenuUIController \u27A4 CreateLoadLevelButtons");
	        
			loadButtons = new List<Button>();
			
			foreach (var filename in filenames) {
				var saveSlotButton = new Button { name = $"Load-{filename}-Button"};
				loadButtons.Add(saveSlotButton);
				
				saveSlotButton.SetEnabled(false);
				saveSlotButton.text = filename;

				ButtonFilenameDict.Add(saveSlotButton, filename);
			}
		}
		
		private void CheckSaveFiles(Dictionary<Button, string> dictionary, SaveManager saveSystem) {
			// saveSystem.LoadTextAssetsAsSaves();
			foreach ( var buttonFilename in dictionary ) {
				if ( saveSystem.LoadLevel(buttonFilename.Value) ) {
					buttonFilename.Key.SetEnabled(true);
				}
			}
		}
		
///// Callbacks ////////////////////////////////////////////////////////////////////////////////////

		private void HandleTryLoadLevel(string filename, GameSceneSO[] locationsToLoad) {
			//try load savefile
			try {
				logger.Log($"try to load \"{filename}\"");
				logger.PrintDebugLog();
				
				bool levelLoaded = _saveSystem.LoadLevel(filename);
				        
				if ( levelLoaded ) {
					logger.Log($"\"{filename}\" Loaded");
					logger.PrintDebugLog();
					
					loadLocationEC.RaiseEvent(locationsToLoad, false, true);
					// _saveSystem.saveManagerData.inputLoad = true;
					// _saveSystem.saveManagerData.loaded = true;
				}
			}
			catch ( Exception e ) {
				logger.Log($"Could not load level: \"{filename}\", with Exeption: {e}");
				logger.PrintDebugLog();
				throw;
			}
		}

///// Public Functions /////////////////////////////////////////////////////////////////////////////
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void OnEnable() {
			//this is a problem, needs to be here
			root = null;
			
			BindStaticElements();

			if ( elements.titleLabel.element is Label l ) {
				Debug.Log("Set Title Text");
				l.text = "Scene Menu";
			}
			
			_saveSystem = GameObject.FindObjectOfType<SaveManager>();
			logger = new CustomLogger();

			var fileNames = FileManager.GetFileNames();
			
			CreateLoadLevelButtons(fileNames);
			elements.buttonContainer.element.AddAll(loadButtons);

			if ( _saveSystem is { } ) {
				BindLoadButtonActions();
				CheckSaveFiles(ButtonFilenameDict, _saveSystem);
			}
		}

		


		private void OnDisable() {
			if ( elements.buttonContainer.element is { } ) {
				elements.buttonContainer.element.RemoveAll(loadButtons);
				UnbindLoadButtonActions();
			}
			//
			UnbindStaticElements();
			
			root = null;
		}
	}
}
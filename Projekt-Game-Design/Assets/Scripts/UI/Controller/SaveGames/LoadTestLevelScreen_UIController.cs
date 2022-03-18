using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using SaveSystem;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Util.Logger;

namespace UI.SaveGames {
    public class LoadTestLevelScreen_UIController : MonoBehaviour {
        // basic ui injector

        // UI Refs
        // ref auf uxml
        // ref auf uss
        // [SerializeField] private StyleSheet styleSheet;
        [SerializeField] private VisualTreeAsset visualTree;

        // UI Document
        [SerializeField] private UIDocument uiDocument;

        // inject uxml tree into [master UI Document]
        
        // todo load with assetdatabase
        [SerializeField] private VisualTreeAsset saveSlotTemplateContainer;
        private ScrollView _saveSlotContainer;

        [Header("Sending Events On")]
        [SerializeField] private LoadEventChannelSO loadLocation;

        [Header("Location Scene To Load")]
        [SerializeField] private GameSceneSO[] locationsToLoad;  
        
        private SaveManager _saveSystem;
        //debug
        private CustomLogger logger;
        
        private void Awake() {
            _saveSystem = GameObject.FindObjectOfType<SaveManager>();
            logger = new CustomLogger();
            
            var tree = visualTree.CloneTree("LoadTestLevelScreen");
            tree.name = "LoadTestLevelScreen";
            tree.visible = false;
            
            tree.style.height = new StyleLength(Length.Percent(100)); 
            uiDocument.rootVisualElement.Add(tree);

            _saveSlotContainer = uiDocument.rootVisualElement.Q<ScrollView>();

            // get save fiels from directory
            var fileNames = FileManager.GetFileNames();
            CreateLoadLevelButtons(fileNames, _saveSlotContainer);
            
            if ( _saveSystem != null ) {
	            GetAllTestSaveFiles();    
            }
        }

        private void CreateLoadLevelButtons(string[] filenames, ScrollView container) {

	        logger.NewLog("LoadTestLevelScreen_UIController \u27A4 CreateLoadLevelButtons \u27A4 saveSlotButton.clicked");
	        
	        List<Button> saveSlotButtons = new List<Button>(); 
	        List<Label> saveSlotLabels = new List<Label>();
	        List<TemplateContainer> saveSlots = new List<TemplateContainer>();
	        
	        foreach (var filename in filenames) {
		        // Debug.Log(placeholder);
		        var saveSlot = saveSlotTemplateContainer.CloneTree();
		        saveSlots.Add(saveSlot);
		        container.Add(saveSlot);
                
		        var saveSlotButton = saveSlot.Q<Button>("SaveSlotButton");
		        saveSlotButtons.Add(saveSlotButton);
		        // saveSlotButton.SetEnabled(false);
                
		        var saveSlotLabel = saveSlot.Q<Label>("SaveSlotLabel");
		        saveSlotLabels.Add(saveSlotLabel);
		        saveSlotLabel.text = filename;

		        saveSlotButton.clicked += () => {
			        //try load savefile
			        try {
				        logger.Log("try level Loaded");
				        logger.PrintDebugLog();
				        bool levelLoaded = _saveSystem.LoadLevel(filename);
				        
				        if ( levelLoaded ) {
					        logger.Log("level Loaded");
					        logger.PrintDebugLog();
					        loadLocation.RaiseEvent(locationsToLoad, false, true);
					        // _saveSystem.saveManagerData.inputLoad = true;
					        // _saveSystem.saveManagerData.loaded = true;
				        }
			        }
			        catch ( Exception e ) {
				        // Debug.Log($"Could not load level: {filename}, with Exeption: {e}");
				        //todo does this work?
				        Console.WriteLine($"Could not load level: {filename}, with Exeption: {e}");
				        throw;
			        }
		        };
	        }
        }
        
        public void GetAllTestSaveFiles() {
            
            // get all savefile names
            // -> create placeholder with names
            var placeholderFilenames = _saveSystem.GetAllTestLevelNames();

            List<Button> saveSlotButtons = new List<Button>(); 
            List<Label> saveSlotLabels = new List<Label>();
            List<TemplateContainer> saveSlots = new List<TemplateContainer>();
            
            foreach (var placeholder in placeholderFilenames) {
                // Debug.Log(placeholder);
                var saveSlot = saveSlotTemplateContainer.CloneTree();
                saveSlots.Add(saveSlot);
                _saveSlotContainer.Add(saveSlot);
                
                var saveSlotButton = saveSlot.Q<Button>("SaveSlotButton");
                saveSlotButtons.Add(saveSlotButton);
                saveSlotButton.SetEnabled(false);
                
                var saveSlotLabel = saveSlot.Q<Label>("SaveSlotLabel");
                saveSlotLabels.Add(saveSlotLabel);
                saveSlotLabel.text = placeholder;
            }
            
            //todo move most of this logic to the savesystem or so
            _saveSystem.LoadTextAssetsAsSaves((filename, valid, index) => {
                if (valid) {
                    // Debug.Log($"Load {index} {filename}" );

                    saveSlotLabels[index].text = filename;
                    var button = saveSlotButtons[index];
                    button.SetEnabled(true);
                    button.clicked += () => {
                        // Debug.Log($"Load {filename}" );
                        _saveSystem.SetCurrentSaveTo(index);
                        loadLocation.RaiseEvent(locationsToLoad, true, false);
                        // _saveSystem.saveManagerData.inputLoad = true;
                        // _saveSystem.saveManagerData.loaded = true;
                    };
                }
                else {
                    _saveSlotContainer.Remove(saveSlots[index]);
                }
            }, _saveSystem.GetValidTestLevel());
        }
    }
}

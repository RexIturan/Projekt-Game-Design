using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Events.ScriptableObjects;
using SaveLoad;
using SaveLoad.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

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
        private ScrollView saveSlotContainer;

        [Header("Sending Events On")]
        [SerializeField] private LoadEventChannelSO _loadLocation = default;

        [Header("Location Scene To Load")]
        [SerializeField] private GameSceneSO[] _locationsToLoad;  
        
        private SaveManager saveSystem;
        
        private void Awake() {
            saveSystem = GameObject.FindObjectOfType<SaveManager>();
            
            var tree = visualTree.CloneTree("LoadTestLevelScreen");
            tree.name = "LoadTestLevelScreen";
            tree.visible = false;
            
            tree.style.height = new StyleLength(Length.Percent(100)); 
            uiDocument.rootVisualElement.Add(tree);

            saveSlotContainer = uiDocument.rootVisualElement.Q<ScrollView>();
            
            GetAllTestSaveFiles();
        }

        // -------------------------------------

        // LoadTestLevelScreen
        
        // event channel LoadLevelAt <path>
        // string path
        // VisualElement Scroll View

        // [SerializeField] private StringEventChannelSO loadGameFromPath;
        [SerializeField] private SaveManagerDataSO saveManagerDataSo;
        
        
        public void GetAllTestSaveFiles() {
            
            // get all savefile names
            // -> create placeholder with names
            var placeholderFilenames = saveSystem.GetAllTestLevelNames();
            List<Action<string, bool, int>> callbacks = new List<Action<string, bool, int>>();
            foreach (var placeholder in placeholderFilenames) {
                var saveSlot = saveSlotTemplateContainer.CloneTree();
                var saveSlotButton = saveSlot.Q<Button>("SaveSlotButton");
                saveSlotButton.SetEnabled(false);
                var saveSlotLabel = saveSlot.Q<Label>("SaveSlotLabel");
                saveSlotLabel.text = placeholder;
                saveSlotContainer.Add(saveSlot);
                
                callbacks.Add((filename, valid, index) => {
                    if (valid) {
                        saveSlotLabel.text = filename;
                        saveSlotButton.SetEnabled(true);
                        saveSlotButton.clicked += () => {
                            Debug.Log($"Load {filename}" );
                            saveSystem.SetCurrentSaveTo(index);
                            _loadLocation.RaiseEvent(_locationsToLoad, true, false);
                            saveSystem.saveManagerData.inputLoad = true;
                            saveSystem.saveManagerData.loaded = true;
                        };
                    }
                    else {
                        saveSlotContainer.Remove(saveSlot);
                    }
                });                
            }
            saveSystem.LoadTextAssetsAsSaves(callbacks, saveSystem.testLevel);
        }
    }
}

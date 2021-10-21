using System.Collections.Generic;
using Events.ScriptableObjects;
using SaveSystem;
using SceneManagement.ScriptableObjects;
using UnityEngine;
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
        private ScrollView _saveSlotContainer;

        [Header("Sending Events On")]
        [SerializeField] private LoadEventChannelSO loadLocation;

        [Header("Location Scene To Load")]
        [SerializeField] private GameSceneSO[] locationsToLoad;  
        
        private SaveManager _saveSystem;
        
        private void Awake() {
            _saveSystem = GameObject.FindObjectOfType<SaveManager>();
            
            var tree = visualTree.CloneTree("LoadTestLevelScreen");
            tree.name = "LoadTestLevelScreen";
            tree.visible = false;
            
            tree.style.height = new StyleLength(Length.Percent(100)); 
            uiDocument.rootVisualElement.Add(tree);

            _saveSlotContainer = uiDocument.rootVisualElement.Q<ScrollView>();
            
            GetAllTestSaveFiles();
        }

        // -------------------------------------

        // LoadTestLevelScreen
        
        // event channel LoadLevelAt <path>
        // string path
        // VisualElement Scroll View

        // [SerializeField] private StringEventChannelSO loadGameFromPath;
        // [SerializeField] private SaveManagerDataSO saveManagerDataSo;
        
        
        public void GetAllTestSaveFiles() {
            
            // get all savefile names
            // -> create placeholder with names
            var placeholderFilenames = _saveSystem.GetAllTestLevelNames();
            // List<Action<string, bool, int>> callbacks = new List<Action<string, bool, int>>();
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
                
                
                // callbacks.Add((filename, valid, index) => {
                //     if (valid) {
                //         Debug.Log($"Load {index} {filename}" );
                //
                //         saveSlotLabels[index].text = filename;
                //         var button = saveSlotButtons[index];
                //         button.SetEnabled(true);
                //         button.clicked += () => {
                //             Debug.Log($"Load {filename}" );
                //             saveSystem.SetCurrentSaveTo(index);
                //             _loadLocation.RaiseEvent(_locationsToLoad, true, false);
                //             saveSystem.saveManagerData.inputLoad = true;
                //             saveSystem.saveManagerData.loaded = true;
                //         };
                //     }
                //     else {
                //         saveSlotContainer.Remove(saveSlots[index]);
                //     }
                // });                
            }
            
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
                        _saveSystem.saveManagerData.inputLoad = true;
                        _saveSystem.saveManagerData.loaded = true;
                    };
                }
                else {
                    _saveSlotContainer.Remove(saveSlots[index]);
                }
            }, _saveSystem.GetValidTestLevel());
        }
    }
}

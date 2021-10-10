using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Events.ScriptableObjects;
using SaveLoad;
using SaveLoad.ScriptableObjects;
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
        private ScrollView saveSlotContainer;

        private SaveManager saveSystem;
        
        private void Awake() {
            saveSystem = GameObject.FindObjectOfType<SaveManager>();
            
            fileFilter = $"*{fileEnding}";
            
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
        
        // [Header("IO settings")]
        private static string path = "TestLevel";
        private static string fileEnding = ".json";
        private string fileFilter;
        
        // todo could be moved to savemanager
        public async Task GetAllTestSaveFiles() {
            StringBuilder str = new StringBuilder("ui:\n");
            var filenames = await saveSystem.GetAllTestSaveNames();

            foreach (var filename in filenames) {
                
                var saveSlot = saveSlotTemplateContainer.CloneTree();
                saveSlot.Q<Button>("SaveSlotButton").clicked += () => {
                    saveManagerDataSo.inputLoad = true;
                    saveManagerDataSo.path = $"{path}/{filename}{fileEnding}";
                    
                    //loadGameFromPath.RaiseEvent($"{path}/{filename}{fileEnding}");
                };

                var saveSlotLabel = saveSlot.Q<Label>("SaveSlotLabel");
                saveSlotLabel.text = filename;
                saveSlotContainer.Add(saveSlot);
            }
        }
    }
}

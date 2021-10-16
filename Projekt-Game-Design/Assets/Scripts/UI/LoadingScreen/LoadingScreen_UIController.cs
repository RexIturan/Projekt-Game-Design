using System;
using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.LoadingScreen {
    public class LoadingScreen_UIController : MonoBehaviour {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private VisualTreeAsset loadingScreenTreeAsset;
        [SerializeField] private UIDocument uiDocument;

        [Header("Recieving Event On")] 
        [SerializeField] private BoolEventChannelSO _toggleLoadingScreen;

        [Header("Sending Event On")]
        [SerializeField] private VoidEventChannelSO enableGamplayInput_EC;
        
        private TemplateContainer loadingScreenRoot;
        
        private void Awake() {
            _toggleLoadingScreen.OnEventRaised += ToggleLoadingScreen;
            inputReader.anyKeyEvent += ToggleLoadingScreen;
        }

        private void OnDisable() {
            _toggleLoadingScreen.OnEventRaised -= ToggleLoadingScreen;
            inputReader.anyKeyEvent -= ToggleLoadingScreen;
        }

        private void Start() {
            var loadingScreenName = "LoadingScreenContainer";
            loadingScreenRoot = loadingScreenTreeAsset.CloneTree(loadingScreenName);
            loadingScreenRoot.name = loadingScreenName;
            loadingScreenRoot.visible = false;
            loadingScreenRoot.style.height = new StyleLength(Length.Percent(100));
            uiDocument.rootVisualElement.Add(loadingScreenRoot);
        }

        void ToggleLoadingScreen(bool show) {
            loadingScreenRoot.visible = show;
        }

        void ToggleLoadingScreen() {
            loadingScreenRoot.visible = !loadingScreenRoot.visible;
            enableGamplayInput_EC.RaiseEvent();
        }
    }
}

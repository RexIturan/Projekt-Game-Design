﻿using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.LoadingScreen {
    public class LoadingScreen_UIController : MonoBehaviour {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private VisualTreeAsset loadingScreenTreeAsset;
        [SerializeField] private UIDocument uiDocument;

        [Header("Recieving Event On")] 
        [SerializeField] private BoolEventChannelSO toggleLoadingScreen;

        [Header("Sending Event On")]
        [SerializeField] private VoidEventChannelSO enableGamplayInputEC;

				// private TemplateContainer _loadingScreenRoot;
				private VisualElement loadingScreenContainer;

				private void Awake() {
            toggleLoadingScreen.OnEventRaised += ToggleLoadingScreen;
            inputReader.AnyKeyEvent += ToggleLoadingScreen;
        }

        private void OnDisable() {
            toggleLoadingScreen.OnEventRaised -= ToggleLoadingScreen;
            inputReader.AnyKeyEvent -= ToggleLoadingScreen;
        }

        private void Start() {
						/*
            var loadingScreenName = "LoadingScreenContainer";
            _loadingScreenRoot = loadingScreenTreeAsset.CloneTree(loadingScreenName);
            _loadingScreenRoot.name = loadingScreenName;
            _loadingScreenRoot.visible = false;
            _loadingScreenRoot.style.height = new StyleLength(Length.Percent(100));
            uiDocument.rootVisualElement.Add(_loadingScreenRoot);
						*/
						loadingScreenContainer = uiDocument.rootVisualElement.Q<VisualElement>("LoadingScreenContainer");
						ToggleLoadingScreen(false);
				}

        void ToggleLoadingScreen(bool show) {
						loadingScreenContainer.visible = show;
						loadingScreenContainer.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
				}

        void ToggleLoadingScreen() {
						/*
            _loadingScreenRoot.visible = !_loadingScreenRoot.visible;
            enableGamplayInputEC.RaiseEvent();
						*/
        }
    }
}

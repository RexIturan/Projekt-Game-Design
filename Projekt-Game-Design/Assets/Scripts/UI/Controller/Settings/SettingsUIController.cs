using GDP01.Gameplay.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

namespace GDP01.UI.Controller.Settings {
	public class SettingsUIController : MonoBehaviour {
		[Serializable]
		private struct SettingsButtonNames {
			public string back;
		}

		[Header("Button Settings")]
		[SerializeField] private SettingsButtonNames buttonNames =
			new SettingsButtonNames { back = "BackButton" };
		[SerializeField] private bool showDebugMessages = false;
		
		[Header("Screen Handeling")] 
		[SerializeField] private ScreenManager screenManager;
		[SerializeField] private ScreenController mainMenuScreen;
		
		[SerializeField] private List<MixerGroupSettingsSO> groupSettings;

///// Private Variables ////////////////////////////////////////////////////////////////////////////

		private Button _backButton;

///// Properties ///////////////////////////////////////////////////////////////////////////////////		

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void UnbindButton(Button button, Action action) {
			if ( button is { } ) {
				button.clicked -= action;
			}
			else {
				Debug.LogError($"UnbindButton\n{name} Button not Found.");
			}
		}

		private void BindButton(ref Button button, VisualElement root, string name, Action action) {
			button = root.Q<Button>(name);
			if ( button is { } ) {
				button.clicked += action;
			}
			else {
				Debug.LogError($"BindButton\n{name} Button not Found.");
			}
		}

		private void BindButtons() {
			var root = GetComponent<UIDocument>().rootVisualElement;
			BindButton(ref _backButton, root, buttonNames.back, HandleBackButton);
		}

		private void UnbindButtons() {
			UnbindButton(_backButton, HandleBackButton);
		}

		private void CreateAudioSlider() {
			var root = GetComponent<UIDocument>().rootVisualElement;

			VisualElement SoundSettingsContainer = root.Q<VisualElement>("SoundSettingsContainer");
			foreach(MixerGroupSettingsSO settings in groupSettings) {
				SoundSettingsContainer.Add(new AudioSlider(settings));
			}
		}

///// Callbacks ////////////////////////////////////////////////////////////////////////////////////

		private void HandleBackButton() {
			if ( showDebugMessages ) {
				Debug.Log("Back Button Pressed");	
			}
			
			screenManager.SetScreenVisibility(mainMenuScreen, true);
		}
		
///// Public Functions /////////////////////////////////////////////////////////////////////////////

///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void OnEnable() {
			BindButtons();

			CreateAudioSlider();
		}
		
		private void OnDisable() {
			UnbindButtons();
		}
	}
}
using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Help {
	/// <summary>
	/// Controls a Help Box, 
	/// Toggles Visibility on Input Event, default: H
	/// HelpBox Text can be acessed by a string event channel
	/// </summary>
	public class HelpUI : MonoBehaviour {
		
		[TextArea] public string description;
		
		// input
		[SerializeField] private InputReader inputReader;
		
		// ui for the help box
		[SerializeField] private VisualTreeAsset helpBoxTreeAsset;
		// ui root
		[SerializeField] private UIDocument uiDocument;
		
		
		[Header("Recieving Event On")]
		[SerializeField] private StringEventChannelSO setHelpTextEC;
		
		//todo add maybe later
		// [SerializeField] private VoidEventChannelSO toggleHelpEC;
		
/////////////////////////////////////// Private Variables //////////////////////////////////////////
		
		private TemplateContainer _helpBoxRoot;
		private Label _helpText;

/////////////////////////////////////// Private Functions //////////////////////////////////////////
		
		void ToggleHelpBox() {
			_helpBoxRoot.visible = !_helpBoxRoot.visible;
		}
		
/////////////////////////////////////// Public Functions ///////////////////////////////////////////
		
		private void Awake() {
			inputReader.HelpEvent += ToggleHelpBox;
			setHelpTextEC.OnEventRaised += SetHelpText;
		}

		private void OnDestroy() {
			inputReader.HelpEvent -= ToggleHelpBox;
			setHelpTextEC.OnEventRaised -= SetHelpText;
		}

		private void Start() {
			_helpBoxRoot = helpBoxTreeAsset.CloneTree("HelpBoxContainer");
			_helpBoxRoot.visible = false;	
			_helpBoxRoot.style.height = new StyleLength(Length.Percent(100));

			_helpText = _helpBoxRoot.Q<Label>("Help_Text");
			_helpText.text = "test\n1\n2";
			
			uiDocument.rootVisualElement.Add(_helpBoxRoot);
			
			ToggleHelpBox();
		}

		public void SetHelpText(List<string> textList) {
			var s = "";
			
			foreach ( var str in textList ) {
				s += str + "\n";
			}

			SetHelpText(s);
		}
		
		public void SetHelpText(string text) {
			_helpText.text = text;
		}
	}
}
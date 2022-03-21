using System;
using GDP01.UI.Types;
using UnityEngine;
using static GDP01.UI.Types.ScreenType;

namespace GDP01.UI {
	[Serializable]
	public class ScreenController : MonoBehaviour {
		[SerializeField] private bool active;
		// [SerializeField] private GameObject root;
		[SerializeField] private ScreenType screenType;

///// Properties ///////////////////////////////////////////////////////////////////////////////////

		public event Action<ScreenController> OnActivate = delegate { }; 
		public event Action<ScreenController> OnDeactivate = delegate { };

		public bool Active {
			get => active;
			set => active = value;
		}
		
		public GameObject RootObject {
			get => gameObject;
		}

		public ScreenType Type => screenType;
	
///// Properties ///////////////////////////////////////////////////////////////////////////////////

		public void Activate() {
			Active = true;
		}

		public void Deactivate() {
			Active = false;
		}

		public void Enable() {
			if ( !RootObject.activeInHierarchy ) {
				RootObject.SetActive(true);
				OnActivate.Invoke(this);	
			}
		}

		public void Disable() {
			if ( RootObject.activeInHierarchy ) {
				RootObject.SetActive(false);
				OnDeactivate.Invoke(this);
			}
		}

		public void UpdateScreen() {
			if ( Active ) {
				Enable();
			}
			else {
				Disable();
			}
		}

		public void HandleScreenChange(ScreenController activeScreen) {
			if ( activeScreen == this )
				return;
			
			if(!activeScreen.Active)
				return;
			
			switch ( activeScreen.Type, Type ) {
				//disable self if
				case (Background, Background):
				case (Menu, Menu):
					Active = false;
					UpdateScreen();
					break;
				
				case (Background, Menu):
				case (Menu, Background):
				default:
					//do nothing
					break;
			}
		}
	}
}
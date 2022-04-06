using System;
using System.Collections.Generic;
using GDP01.UI.Types;
using UI.Components.Tooltip;
using UnityEngine;

namespace GDP01.UI {
	public class ScreenManager : MonoBehaviour {
		[SerializeField] private List<ScreenController> screens;
		[SerializeField] private TooltipLayer tooltipLayer;

		private event Action<ScreenController> OnScreenChanged = delegate(ScreenController controller) {  }; 

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void BindScreenCallbacks() {
			screens.ForEach(screen => {
				OnScreenChanged += screen.HandleScreenChange;
				screen.OnActivate += NotifyScreenChange;
				screen.OnDeactivate += NotifyScreenChange;
			});
		}

		private void UnbindScreenCallbacks() {
			screens.ForEach(screen => {
				OnScreenChanged -= screen.HandleScreenChange;
				screen.OnActivate -= NotifyScreenChange;
				screen.OnDeactivate -= NotifyScreenChange;
			});
		}

		private void NotifyScreenChange(ScreenController screenController) {
			OnScreenChanged?.Invoke(screenController);
			// screens.ForEach(screen => screen.HandleScreenChange(screenController));
		}
		
///// Private Functions ////////////////////////////////////////////////////////////////////////////

		public void ResetScreens() {
			screens.ForEach(screen => screen.Disable());
		}
		
		public void UpdateScreens() {
			foreach ( var screen in screens ) {
				screen.UpdateScreen();
			}
		}

		public void SetScreenVisibility(ScreenController screen, bool visibile) {
			if ( screens.Contains(screen) ) {
				screen.Active = visibile;
				if(screen != null)
					screen.UpdateScreen();
			}
			HideAllTooltips();
		}

		public void HideAllTooltips() {
			tooltipLayer.HideTooltips();
		}

///// Unity Functions //////////////////////////////////////////////////////////////////////////////		
		
		private void OnEnable() {
			BindScreenCallbacks();
			ResetScreens();
			UpdateScreens();
		}

		private void OnDisable() {
			UnbindScreenCallbacks();
			ResetScreens();
		}
	}
}
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.ActionButton {
	public class GroupedButton : Button {
		private bool dontBlur;

		protected override void ExecuteDefaultAction(EventBase evt) {
			// Debug.Log($"GB:\n event:{evt}");	
			base.ExecuteDefaultAction(evt);
			// if ( evt is FocusEvent || evt is BlurEvent || evt is PointerDownEvent 
			// 	    || evt is FocusOutEvent || evt is FocusInEvent) {
			// 	// Debug.Log($"GB:\n event:{evt}");	
			// }
		}

		protected override void ExecuteDefaultActionAtTarget(EventBase evt) {
			// Debug.Log($"GB at Target:\n event:{evt}");
			// if ( evt is FocusOutEvent focusOutEvt) {
			// 	if ( focusOutEvt.relatedTarget == null ) {
			// 		// dontBlur = true;
			// 		evt
			// 	}
			// }

			base.ExecuteDefaultActionAtTarget(evt);
			
			// if ( evt is FocusEvent || evt is BlurEvent || evt is PointerDownEvent
			//      || evt is FocusOutEvent || evt is FocusInEvent
			//      || evt is MouseDownEvent) {
			// 		
			// }
		}

		public override void Blur() {
			base.Blur();
		}
	}
}
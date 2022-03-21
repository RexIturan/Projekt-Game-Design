using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
		[CreateAssetMenu(fileName = "newActionEC", menuName = "Events/Basic/Action Event Channel")]
		public class ActionEventChannelSO : EventChannelBaseSO {
        
			public event EventHandler<Action> EventRaised;
			
			public void OnEventRaised(object sender, Action action) {
				EventRaised?.Invoke(sender, action);
			}
		}
}
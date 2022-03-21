using System;
using Events.ScriptableObjects.Core;
using UI.Gameplay.Types;
using UnityEngine;

namespace Events.ScriptableObjects {
	
	/// <summary>
	/// This class is used for Events that have no arguments (Example: Exit game event)
	/// </summary>
	[CreateAssetMenu(menuName = "Events/UI/Screeen Event Channel")]
	public class ScreenEventChannelSO : EventChannelBaseSO {
		public event Action<GameplayScreen> OnEventRaised;

		public void RaiseEvent(GameplayScreen screen) {
			OnEventRaised?.Invoke(screen);
		}
	}
}

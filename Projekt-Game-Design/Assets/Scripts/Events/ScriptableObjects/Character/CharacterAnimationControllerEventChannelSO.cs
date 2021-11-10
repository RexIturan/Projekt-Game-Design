using System;
using System.Collections.Generic;
using Events.ScriptableObjects.Core;
using UnityEngine;
using Util;

namespace Events.ScriptableObjects
{
	/// <summary>
	/// eventchannel for animations
	/// contains reference to animation controller
	/// </summary>
	[CreateAssetMenu(menuName = "Events/CharacterAnimationControllerEventChannel")]
	public class CharacterAnimationControllerEventChannelSO : EventChannelBaseSO
	{

		public event Action<CharacterAnimationController> OnEventRaised;

		public void RaiseEvent(CharacterAnimationController controller)
		{
			OnEventRaised?.Invoke(controller);
		}
	}
}
using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

/// <summary>
/// This class is used for Events that have no arguments (Example: Exit game event)
/// </summary>
[CreateAssetMenu(menuName = "Events/Basic/Void Event Channel")]
public class VoidEventChannelSO : EventChannelBaseSO {
		public event Action BeforeEventRaised;
    public event Action OnEventRaised;
    public event Action AfterEventRaised;

    public void RaiseEvent() {
	    BeforeEventRaised?.Invoke();
	    OnEventRaised?.Invoke();
	    AfterEventRaised?.Invoke();
    }
}
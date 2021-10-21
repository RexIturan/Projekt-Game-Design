using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

/// <summary>
/// This class is used for Events that have no arguments (Example: Exit game event)
/// </summary>
[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : EventChannelBaseSO
{
    public event Action OnEventRaised;

    public void RaiseEvent() {
	    OnEventRaised?.Invoke();
    }
}
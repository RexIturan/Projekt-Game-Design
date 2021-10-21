using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/GameObject/Action Event Channel")]
    public class GameObjActionEventChannelSO : EventChannelBaseSO {
        
        public event Action<GameObject, Action<int>> OnEventRaised;

        public void RaiseEvent(GameObject value, Action<int> callBackAction) {
	        OnEventRaised?.Invoke(value, callBackAction);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/GameObject/Action Event Channel")]
    public class GameObjActionEventChannelSO : EventChannelBaseSO {
        
        public UnityAction<GameObject, Action<int>> OnEventRaised;

        public void RaiseEvent(GameObject value, Action<int> callBackAction)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value, callBackAction);
        }
        
    }
}
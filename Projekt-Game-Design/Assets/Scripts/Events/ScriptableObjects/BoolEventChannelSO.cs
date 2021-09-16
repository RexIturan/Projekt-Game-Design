﻿using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/Bool Event Channel")]
    public class BoolEventChannelSO : EventChannelBaseSO {
        
        public UnityAction<bool> OnEventRaised;

        public void RaiseEvent(bool value)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value);
        }
        
    }
}
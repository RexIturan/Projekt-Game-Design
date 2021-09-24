﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntList Event Channel")]
    public class IntListEventChannelSO : EventChannelBaseSO {
        
        public event Action<List<int>> OnEventRaised;

        public void RaiseEvent(List<int> value)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value);
        }
    }
}
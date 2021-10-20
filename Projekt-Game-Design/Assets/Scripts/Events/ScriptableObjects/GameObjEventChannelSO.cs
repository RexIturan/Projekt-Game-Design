using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/GameObject/Gameobject Event Channel")]
    public class GameObjEventChannelSO : EventChannelBaseSO {
        
        public event Action<GameObject> OnEventRaised;

        public void RaiseEvent(GameObject value) {
	        OnEventRaised?.Invoke(value);
        }
    }
}
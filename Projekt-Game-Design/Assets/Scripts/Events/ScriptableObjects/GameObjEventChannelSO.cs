using Events.ScriptableObjects.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/GameObject/Gameobject Event Channel")]
    public class GameObjEventChannelSO : EventChannelBaseSO {
        
        public UnityAction<GameObject> OnEventRaised;

        public void RaiseEvent(GameObject value)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value);
        }
        
    }
}
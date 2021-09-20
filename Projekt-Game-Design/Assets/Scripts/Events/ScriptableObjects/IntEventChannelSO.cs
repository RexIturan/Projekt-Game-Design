using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/Int Event Channel")]
    public class IntEventChannelSO : EventChannelBaseSO {
        
        public UnityAction<int> OnEventRaised;

        public void RaiseEvent(int value)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value);
        }
    }
}
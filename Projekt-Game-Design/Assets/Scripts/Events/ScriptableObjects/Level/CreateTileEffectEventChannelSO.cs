using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/Level/CreateTileEffectEventChannel")]
    public class CreateTileEffectEventChannelSO : EventChannelBaseSO
    {
        public event Action<GameObject, Vector3Int> OnEventRaised;

        public void RaiseEvent(GameObject effectPrefab, Vector3Int position)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(effectPrefab, position);
        }

    }
}

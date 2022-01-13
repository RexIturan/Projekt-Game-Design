using System;
using Events.ScriptableObjects.Core;
using UnityEngine;


namespace Events.ScriptableObjects
{
	/// <summary>
	/// Makes pathfinding drawer draw the pattern onto the tilemap
	/// </summary>
    // TODO rename
    [CreateAssetMenu(menuName = "Events/Level/DrawPatternEventChannel")]
    public class DrawPatternEventChannelSO : EventChannelBaseSO
    {
        public event Action<Vector3Int, bool[,], Vector2Int> OnEventRaised;

        public void RaiseEvent(Vector3Int patternOrigin, bool[,] pattern, Vector2Int anchor)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(patternOrigin, pattern, anchor);
        }

    }
}

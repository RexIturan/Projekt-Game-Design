using System;
using Events.ScriptableObjects.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Events.ScriptableObjects
{
		/// <summary>
		/// To draw a single tile onto a tilemap. 
		/// </summary>
    [CreateAssetMenu(menuName = "Events/Level/DrawTileEventChannel")]
    public class DrawTileEventChannelSO : EventChannelBaseSO
    {
        public event Action<Vector3Int, TileBase> OnEventRaised;

        public void RaiseEvent(Vector3Int gridPos, TileBase tile)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(gridPos, tile);
        }

    }
}

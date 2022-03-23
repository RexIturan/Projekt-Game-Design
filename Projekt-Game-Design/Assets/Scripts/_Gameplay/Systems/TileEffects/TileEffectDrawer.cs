using Events.ScriptableObjects;
using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GDP01.TileEffects
{
    public class TileEffectDrawer : MonoBehaviour
		{
				[SerializeField] private VoidEventChannelSO clearTilemapEC;
				[SerializeField] private DrawTileEventChannelSO drawTileEC;
				[SerializeField] private Tilemap tileEffectTilemap;
				[SerializeField] private GridDataSO gridData;

				public void Awake() {
						drawTileEC.OnEventRaised += DrawTileEffects;
						clearTilemapEC.OnEventRaised += tileEffectTilemap.ClearAllTiles;
				}

				public void OnDestroy()
				{
						drawTileEC.OnEventRaised -= DrawTileEffects;
						clearTilemapEC.OnEventRaised -= tileEffectTilemap.ClearAllTiles;
				}

				private void DrawTileEffects(Vector3Int pos, TileBase tile) {
						tileEffectTilemap.SetTile(gridData.GetTilePosFromGridPos(pos), tile);
				}
    }
}

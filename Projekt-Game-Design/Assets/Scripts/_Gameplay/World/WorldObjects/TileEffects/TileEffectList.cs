using Characters;
using Events.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace GDP01.TileEffects
{
		/// <summary>
		/// Component for the Tile Effect List. Could also be called WorldEffects. 
		/// It contains all current effects that are set on tiles. 
		/// </summary>
    public class TileEffectList : MonoBehaviour
    {
				[SerializeField] private List<GameObject> tileEffects;
				[SerializeField] private GameObject baseTileEffect;

				[SerializeField] private VoidEventChannelSO handleTileEffects;
				[SerializeField] private VoidEventChannelSO clearTilemapEC;
				[SerializeField] private DrawTileEventChannelSO drawTileEC;

				public void Awake() {
						handleTileEffects.OnEventRaised += HandleTileEffects;
				}

				public void OnDestroy () {
						handleTileEffects.OnEventRaised -= HandleTileEffects;
				}

				public void Start()
				{
						GameObject tileEffect = Instantiate(baseTileEffect, Vector3.zero, Quaternion.identity, transform);
						tileEffects.Add(tileEffect);
						tileEffect.GetComponent<GridTransform>().gridPosition = new Vector3Int(8, 1, 23);
						tileEffect.GetComponent<TileEffectController>().SetEternal(true);
				}

				/// <summary>
				/// Removes and destroys all tile effects. 
				/// </summary>
				public void Clear() {
						foreach(GameObject tileEffect in tileEffects) {
								Destroy(tileEffect);
						}
						tileEffects.Clear();
				}

				public void Add(GameObject tileEffect) {
						tileEffects.Add(tileEffect);
				}

				public void HandleTileEffects()
				{
						// evaluate effects etc. 
						foreach(GameObject tileEffect in tileEffects) {
								tileEffect.GetComponent<TileEffectController>().OnAction();
						}

						// destroy the dead ones
						for(int i = 0; i < tileEffects.Count;) {
								if( tileEffects[i].GetComponent<TileEffectController>().GetDestroy() ) {
										Destroy(tileEffects[i]);
										tileEffects.RemoveAt(i);
								}
								else {
										i++;
								}
						}

						DrawTileEffects();
				}

				/// <summary>
				/// Draws tiles of tile effects onto tilemap. 
				/// </summary>
				private void DrawTileEffects() {
						clearTilemapEC.RaiseEvent();

						foreach(GameObject tileEffect in tileEffects) {
								GridTransform tilePos = tileEffect.GetComponent<GridTransform>();
								TileEffectController effectController = tileEffect.GetComponent<TileEffectController>();

								drawTileEC.RaiseEvent(tilePos.gridPosition, effectController.GetActiveTileBase());
						}
				}

		}
}

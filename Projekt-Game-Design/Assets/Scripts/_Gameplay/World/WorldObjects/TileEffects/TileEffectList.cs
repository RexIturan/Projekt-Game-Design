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
				/// <summary>
				/// List of effects that will be added upon next HandleTileEffects
				/// </summary>
				[SerializeField] private List<GameObject> scheduledEffects;
				[SerializeField] private GameObject baseTileEffect;

				[SerializeField] private CreateTileEffectEventChannelSO createTileEffectEC;
				[SerializeField] private VoidEventChannelSO handleTileEffects;
				[SerializeField] private VoidEventChannelSO clearTilemapEC;
				[SerializeField] private DrawTileEventChannelSO drawTileEC;

				public void Awake() {
						createTileEffectEC.OnEventRaised += CreateTileEffect;
						handleTileEffects.OnEventRaised += HandleTileEffects;
				}

				public void OnDestroy () {
						createTileEffectEC.OnEventRaised -= CreateTileEffect;
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

				/// <summary>
				/// For game initialization. Is called by SaveLoader. 
				/// </summary>
				/// <param name="tileEffect">Tile effect that is added </param>
				public void Add(GameObject tileEffect) {
						scheduledEffects.Add(tileEffect);
				}

				private void CreateTileEffect(GameObject tileEffect, Vector3Int position) {
						// only add if such a tile effect doesn't already exist
						if ( !ExistsTileEffect(tileEffect, position) ) {
								GameObject newTileEffect = Instantiate(tileEffect, Vector3.zero, Quaternion.identity, transform);
								newTileEffect.GetComponent<GridTransform>().gridPosition = position;
								Add(newTileEffect);
						}
				}

				private bool ExistsTileEffect(GameObject newTileEffect, Vector3Int pos) {
						int id = newTileEffect.GetComponent<TileEffectController>().id;

						List<GameObject> allEffects = new List<GameObject>();
						allEffects.AddRange(tileEffects);
						allEffects.AddRange(scheduledEffects);

						return allEffects.Exists(tileEffect => {
								return tileEffect.GetComponent<TileEffectController>().id == id && 
										tileEffect.GetComponent<GridTransform>().gridPosition.Equals(pos);
						});
				}

				private void AddScheduledEffects() {
						foreach(GameObject tileEffect in scheduledEffects) {
								tileEffects.Add(tileEffect);
						}
						scheduledEffects.Clear();
				}

				public void HandleTileEffects()
				{
						AddScheduledEffects();

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

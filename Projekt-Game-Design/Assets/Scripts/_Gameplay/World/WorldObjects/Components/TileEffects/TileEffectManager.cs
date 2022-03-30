using Characters;
using Events.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using GDP01._Gameplay.Provider;
using SaveSystem.V2.Data;
using UnityEngine;
using WorldObjects;

namespace GDP01.TileEffects
{
		/// <summary>
		/// Component for the Tile Effect List. Could also be called WorldEffects. 
		/// It contains all current effects that are set on tiles. 
		/// </summary>
    public class TileEffectManager : MonoBehaviour, ISaveState<TileEffectManager.Data>
    {
	    public class Data {
		    public List<TileEffectController.Data> TileEffectData;
	    }

				[SerializeField] private List<GameObject> tileEffects;
				/// <summary>
				/// List of effects that will be added upon next HandleTileEffects
				/// </summary>
				[SerializeField] private List<GameObject> scheduledEffects;
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

				/// <summary>
				/// Removes and destroys all tile effects. 
				/// </summary>
				public void Clear() {
						foreach(GameObject tileEffect in tileEffects) {
								Destroy(tileEffect);
						}
						tileEffects.Clear();
				}

				public void AddTileEffectAt(int id, Vector3Int gridPos) {
					if ( !ExistsTileEffect(id, gridPos) ) {
						var tileEffectObj = CreateTileEffectObject(id);
						tileEffectObj.GetComponent<GridTransform>().MoveTo(gridPos);	
					}
				}

				private GameObject CreateTileEffectObject(int id) {
					GameObject prefab = GameplayDataProvider.Current.TileEffectContainerSO.tileEffects[id];
					return Instantiate(prefab, transform, true);
				}
				
				/// <summary>
				/// Add TileEffect from id
				/// </summary>
				/// <param name="id">Tileeffect id</param>
				public void AddTileEffect(int id) {
					Add(CreateTileEffectObject(id));
				}
				
				/// <summary>
				/// For game initialization. Is called by SaveLoader. 
				/// </summary>
				/// <param name="tileEffect">Tile effect that is added </param>
				public void Add(GameObject tileEffect) {
						scheduledEffects.Add(tileEffect);
				}

				public GameObject GetTileEffectAt(Vector3Int gridPos) {
					List<GameObject> allEffects = new List<GameObject>();
					allEffects.AddRange(tileEffects);
					allEffects.AddRange(scheduledEffects);

					return allEffects.FirstOrDefault(effect =>
						effect.GetComponent<GridTransform>().gridPosition.Equals(gridPos));
				}
				
				public void RemoveTileEffectAt(Vector3Int gridPos) {
					var effect = GetTileEffectAt(gridPos);
					if ( effect is { } ) {
						tileEffects.Remove(effect);
						scheduledEffects.Remove(effect);
						Destroy(effect);
					}
				}
				
				private void CreateTileEffect(GameObject tileEffect, Vector3Int position) {
						// only add if such a tile effect doesn't already exist
						if ( !ExistsTileEffect(tileEffect.GetComponent<TileEffectController>().id, position) ) {
								GameObject newTileEffect = Instantiate(tileEffect, Vector3.zero, Quaternion.identity, transform);
								newTileEffect.GetComponent<GridTransform>().gridPosition = position;
								Add(newTileEffect);
						}
				}

				private bool ExistsTileEffect(int id, Vector3Int pos) {

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


				public TileEffectManager.Data Save() {
					return new Data {
						TileEffectData = tileEffects
							.Select(obj => obj.GetComponent<TileEffectController>().Save()).ToList()
					};
				}

				public void Load(TileEffectManager.Data managerData) {
					managerData.TileEffectData.ForEach(data => {
						var tileEffectObj = CreateTileEffectObject(data.id);
						tileEffectObj.GetComponent<TileEffectController>().Load(data);
					});
				}
    }
}

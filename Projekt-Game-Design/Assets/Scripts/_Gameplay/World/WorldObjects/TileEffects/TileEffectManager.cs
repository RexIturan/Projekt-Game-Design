using Characters;
using Events.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using GDP01._Gameplay.Provider;
using SaveSystem.V2.Data;
using UnityEngine;
using WorldObjects;
using Characters.Types;
using Grid;

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
				[SerializeField] private FactionEventChannelSO handleTileEffects;
				[SerializeField] private VoidEventChannelSO clearTilemapEC;
				[SerializeField] private DrawTileEventChannelSO drawTileEC;

				private bool isHandlingEffects;

				/// <summary>
				/// Time it takes to destroy a tile effect when it's queued for destruction. 
				/// </summary>
				private static readonly float TIME_FOR_DESTROY = 1.5f;

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
						Add(tileEffectObj);
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
						TileEffectController tileEffectController = tileEffect.GetComponent<TileEffectController>();

						// only add if such a tile effect doesn't already exist
						// and if the requirements for the tile types are met
						if (TileRequirementsAreMet(tileEffectController, position)) {
								RemoveExistingTileEffect(tileEffectController.id, position);

								GameObject newTileEffect = Instantiate(tileEffect, Vector3.zero, Quaternion.identity, transform);
								newTileEffect.GetComponent<GridTransform>().gridPosition = position;
								Add(newTileEffect);

								DrawTileEffects();

								if ( !isHandlingEffects )
										DestroyDeadEffects(0);
						}
				}

				private bool TileRequirementsAreMet(TileEffectController tileEffect, Vector3Int tilePos) {
						bool canBePlaced = false;
						GridController gridController = GridController.FindGridController();

						if ( gridController ) {
								TileTypeSO tile = gridController.GetTileAt(tilePos);
								TileTypeSO groundTile = gridController.GetTileAt(tilePos - new Vector3Int(0, 1, 0));
								Debug.Log($"Checking for tile effect, ground {groundTile}, top {tile}");

								canBePlaced = tile && groundTile &&
										HasAllFlags(( int )tileEffect.creationRequirementsTop, ( int )tile.properties) &&
										HasAllFlags(( int )tileEffect.creationRequirementsGround, ( int )groundTile.properties);
						}
						else
								Debug.LogError("Could not find grid controller. Cannot expand tile effect. ");

						return canBePlaced;
				}

				private bool HasAllFlags(int requiredFlags, int actualFlags) {
						return (requiredFlags & actualFlags).Equals(requiredFlags);
				}

				public bool ExistsTileEffect(int id, Vector3Int pos) {

						List<GameObject> allEffects = new List<GameObject>();
						allEffects.AddRange(tileEffects);
						allEffects.AddRange(scheduledEffects);

						return allEffects.Exists(tileEffect => {
								return tileEffect.GetComponent<TileEffectController>().id == id && 
										tileEffect.GetComponent<GridTransform>().gridPosition.Equals(pos);
						});
				}

				/// <summary>
				/// Removes all scheduled and existing TileEffects at given position with given id. 
				/// Is used when creating new TileEffects to avoid duplicates. 
				/// </summary>
				/// <param name="id">ID of TileEffect that is to remove</param>
				/// <param name="pos">Only TileEffects at this grid position will be removed </param>
				private void RemoveExistingTileEffect(int id, Vector3Int pos) {
						foreach (GameObject tileEffect in tileEffects) {
								if ( tileEffect.GetComponent<TileEffectController>().id == id &&
										tileEffect.GetComponent<GridTransform>().gridPosition.Equals(pos) ) {
										tileEffect.GetComponent<TileEffectController>().SetDestroyTrue();
								}
						}

						for ( int i = 0; i < scheduledEffects.Count; ) {
								if ( scheduledEffects[i].GetComponent<TileEffectController>().id == id &&
										scheduledEffects[i].GetComponent<GridTransform>().gridPosition.Equals(pos) ) {
										Destroy(scheduledEffects[i]);
										scheduledEffects.RemoveAt(i);
								}
								else
										i++;
						}
				}

				private void AddScheduledEffects() {
						foreach(GameObject tileEffect in scheduledEffects) {
								tileEffects.Add(tileEffect);
						}
						scheduledEffects.Clear();
				}

				public void HandleTileEffects(Faction newTurnFaction)
				{
						isHandlingEffects = true;

						AddScheduledEffects();

						// evaluate effects etc. 
						foreach(GameObject tileEffect in tileEffects) {
								TileEffectController tileEffectController = tileEffect.GetComponent<TileEffectController>();
								if(tileEffectController.updatedOnFaction.Equals(newTurnFaction))
										tileEffectController.OnAction();
						}

						// destroy the dead ones
						DestroyDeadEffects(TIME_FOR_DESTROY);

						DrawTileEffects();

						isHandlingEffects = false;
				}

				private void DestroyDeadEffects(float delay) {
						for(int i = 0; i < tileEffects.Count;) {
								if( tileEffects[i].GetComponent<TileEffectController>().GetDestroy() ) {
										Destroy(tileEffects[i], delay);
										tileEffects.RemoveAt(i);
								}
								else {
										i++;
								}
						}
				}

				/// <summary>
				/// Draws tiles of tile effects onto tilemap. 
				/// </summary>
				private void DrawTileEffects() {
						List<GameObject> allEffects = new List<GameObject>();
						allEffects.AddRange(tileEffects);
						allEffects.AddRange(scheduledEffects);

						clearTilemapEC.RaiseEvent();

						foreach(GameObject tileEffect in allEffects ) {
								GridTransform tilePos = tileEffect.GetComponent<GridTransform>();
								TileEffectController effectController = tileEffect.GetComponent<TileEffectController>();

								drawTileEC.RaiseEvent(tilePos.gridPosition, effectController.GetActiveTileBase());
						}
				}

				public static TileEffectManager FindInstant() {
						return FindObjectOfType<TileEffectManager>();
				}

				public TileEffectManager.Data Save() {
					var tileEffectsData = tileEffects
						.Select(obj => obj.GetComponent<TileEffectController>().Save()).ToList();
					tileEffectsData.AddRange(scheduledEffects.Select(obj => obj.GetComponent<TileEffectController>().Save()));
					
					return new Data {
						TileEffectData = tileEffectsData
					};
				}

				public void Load(TileEffectManager.Data managerData) {
					managerData.TileEffectData.ForEach(data => {
						var tileEffectObj = CreateTileEffectObject(data.id);
						var tileEffectController = tileEffectObj.GetComponent<TileEffectController>();
						tileEffectController.Load(data);
						Add(tileEffectController.gameObject);
					});
				}
    }
}

using System;
using Characters;
using Events.ScriptableObjects.GameState;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SaveSystem.V2.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldObjects;
using Characters.Types;
using Level.Grid;

namespace GDP01.TileEffects
{
		/// <summary>
		/// Defines the properties of all effects 
		/// that a tile has at the moment. 
		/// </summary>
    public class TileEffectController : MonoBehaviour, ISaveState<TileEffectController.Data> {

			public int id;

				public string effectName;
				public string description;

				[SerializeField] private List<TileEffectSO> effects;
				[SerializeField] private List<TileEffectSO> preActivationEffects;
				[SerializeField] private List<TileEffectSO> startEffects;
				[SerializeField] private List<TileEffectSO> removeSchedule;

				[SerializeField] private TileBase inactiveTile;
				[SerializeField] private TileBase activeTile;

				/// <summary>
				/// Updates til activation. If it is set to 1, 
				/// the next update will activate the effect. 
				/// That update will also trigger the active actions already. 
				/// </summary>
				[SerializeField] private int timeUntilActivation;
				/// <summary>
				/// Amount of times the effect lasts after activation, 
				/// i.e. if set to 1, the action will be taken one time after activation 
				/// and then the tile effect is queued for destroy 
				/// </summary>
				[SerializeField] private int timeToLive;
				/// <summary>
				/// If true, the effect will not decrease time to live field or set destroy flag
				/// </summary>
				[SerializeField] private bool eternal;
				/// <summary>
				/// The controller takes tile effect actions when active
				/// </summary>
				[SerializeField] private bool active;
				/// <summary>
				/// Flag is set when the effect is over, 
				/// a tile effect that's set to destroy doesn't take actions
				/// </summary>
				[SerializeField] private bool destroy;
				
				public Faction updatedOnFaction;

				[Header("Requirements for placement: ")]
				/// <summary>
				/// Requirements for the tile in which the effect will be placed 
				/// </summary>
				public TileProperties creationRequirementsTop;

				/// <summary>
				/// Requirements for the tile beneath the tile in which the effect will be placed 
				/// </summary>
				public TileProperties creationRequirementsGround;

				[Header("For AI: ")]
				public int worthAgainstPlayerPerTarget;
				public int worthForEnemyPerTarget;

				[Header("Receiving events on: ")]
				[SerializeField] private PositionGameObjectEventChannelSO onTileEnterEC;
				[SerializeField] private PositionGameObjectEventChannelSO onTileExitEC;

				#region Getter and setter

				public TileBase GetActiveTileBase() {
						return active ? activeTile : inactiveTile;
				}

				public void SetTimeUntilActivation(int turns) {
						timeUntilActivation = turns;

						if ( timeUntilActivation > 0 )
								active = false;
				}

				public int GetTimeUntilActivation() {
						return timeUntilActivation;
				}

				public int GetTimeToLive() {
						return timeToLive;
				}

				public void SetTimeToLive(int turns) {
						timeToLive = turns;
				}

				public void SetEternal(bool eternal) {
						this.eternal = eternal;
				}

				public bool GetEternal()
				{
						return eternal;
				}

				public bool GetActive() {
						return active;
				}

				public bool GetDestroy() {
						return destroy;
				}

				public void SetDestroyTrue() {
						destroy = true;
				}

				#endregion
				
				public void AddEffect(TileEffectSO effect) {
						effects.Add(effect);
				}

				public void RemoveEffect(TileEffectSO effect) {
						removeSchedule.Add(effect);
				}

				public void RemoveScheduledEffects() {
						foreach ( TileEffectSO effect in removeSchedule ) {
								effects.Remove(effect);
								preActivationEffects.Remove(effect);
						}
						removeSchedule.Clear();
				}

				private void Awake()
				{
						onTileEnterEC.OnEventRaised += HandleOnEnter;
						onTileExitEC.OnEventRaised += HandleOnExit;
				}

				private void OnDestroy()
				{
						onTileEnterEC.OnEventRaised -= HandleOnEnter;
						onTileExitEC.OnEventRaised -= HandleOnExit;
				}

				public void Start()
				{
						if(timeUntilActivation <= 0 && !destroy) {
								active = true;
						}

						foreach(TileEffectSO effect in startEffects) {
								effect.OnAction(this);
						}
				}

				private void HandleOnEnter(Vector3Int pos, GameObject obj) {
						RemoveScheduledEffects();

						if(active && pos.Equals(GetComponent<GridTransform>().gridPosition)) {
								foreach ( TileEffectSO effect in effects ) {
										if(effect.actionOnEnter)
												effect.OnAction(this);
								}
						}
				}

				private void HandleOnExit(Vector3Int pos, GameObject obj) {
						RemoveScheduledEffects();

						if ( active && pos.Equals(GetComponent<GridTransform>().gridPosition) ) {
								foreach ( TileEffectSO effect in effects ) {
										if(effect.actionOnExit)
												effect.OnAction(this);
								}
						}
				}

				/// <summary>
				/// Updates the tile effect. Counts up and down the timers 
				/// and, if active, takes actions. 
				/// </summary>
				public void OnAction() {
						RemoveScheduledEffects();

						// only do anything if the effect is not queued to destruction 
						if ( !destroy ) {
								// pre-active phase
								if ( !active ) {
										timeUntilActivation--;

										if ( timeUntilActivation <= 0 )
												active = true;

										// actions for every update before activation
										foreach ( TileEffectSO effect in preActivationEffects ) {
												effect.OnAction(this);
										}
								}

								// active phase
								if( active ) {
										if ( timeToLive > 0 || eternal ) {
												foreach ( TileEffectSO effect in effects ) {
														if(effect.actionOnEvaluate)
																effect.OnAction(this);
												}

												timeToLive--;
										}
										if ( timeToLive <= 0 && !eternal) {
												destroy = true;
												active = false;
										}
								}
						}
				}

				public TileEffectController() {
						effects = new List<TileEffectSO>();
						destroy = false;
				}

				[Serializable]
				public class Data {
					public int id;
					public bool active;
					// public List<int> currentEffects;
					// public List<int> effectsToRemove;
					public Vector3Int gridPosition;
					public int timeUntilActivation;
					public int timeToLive;
				}

				public Data Save() {
					return new Data
					{
						id = id,
						timeUntilActivation = timeUntilActivation,
						timeToLive = timeToLive,
						gridPosition = GetComponent<GridTransform>().gridPosition
					};
				}

				public void Load(Data data) {
					SetTimeUntilActivation(data.timeUntilActivation);
					SetTimeToLive(data.timeToLive);
					GetComponent<GridTransform>().MoveTo(data.gridPosition);
				}
		}
}

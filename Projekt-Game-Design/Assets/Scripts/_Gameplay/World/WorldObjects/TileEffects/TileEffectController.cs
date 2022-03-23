using Characters;
using Events.ScriptableObjects.GameState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GDP01.TileEffects
{
		/// <summary>
		/// Defines the properties of all effects 
		/// that a tile has at the moment. 
		/// </summary>
    public class TileEffectController : MonoBehaviour
    {
				[SerializeField] private List<TileEffectSO> effects;

				[SerializeField] private TileBase inactiveTile;
				[SerializeField] private TileBase activeTile;

				/// <summary>
				/// Amount of times the effect actions are not taken, 
				/// i.e. if set to 1, the next action will not be taken, but the one after it
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

				[SerializeField] private PositionGameObjectEventChannelSO onTileEnterEC;
				[SerializeField] private PositionGameObjectEventChannelSO onTileExitEC;

				#region Getter and setter

				public TileBase GetActiveTileBase() {
						return active ? activeTile : inactiveTile;
				}

				public void AddEffect(TileEffectSO effect) {
						effects.Add(effect);
				}

				public void SetTimeUntilActivation(int turns) {
						timeUntilActivation = turns;

						if ( timeUntilActivation > 0 )
								active = false;
				}

				public void SetTimeToLive(int turns) {
						timeToLive = turns;
				}

				public void SetEternal(bool eternal) {
						this.eternal = eternal;
				}

				public bool GetDestroy() {
						return destroy;
				}

				#endregion

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
				}

				private void HandleOnEnter(Vector3Int pos, GameObject obj) {
						if(active && pos.Equals(GetComponent<GridTransform>().gridPosition)) {
								foreach ( TileEffectSO effect in effects ) {
										if(effect.actionOnEnter)
												effect.OnAction(this);
								}
						}
				}

				private void HandleOnExit(Vector3Int pos, GameObject obj)
				{
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
						// only do anything if the effect is not queued to destruction 
						if ( !destroy ) {
								// pre-active phase
								if ( !active ) {
										timeUntilActivation--;

										if ( timeUntilActivation <= 0 )
												active = true;
								}
								// active phase
								else {
										if ( timeToLive > 0 || eternal ) {
												foreach ( TileEffectSO effect in effects ) {
														if(effect.actionOnEvaluate)
																effect.OnAction(this);
												}

												timeToLive--;
										}
										else {
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
		}
}

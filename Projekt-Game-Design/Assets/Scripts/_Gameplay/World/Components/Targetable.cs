using System.Collections.Generic;
using Characters;
using Characters.Types;
using UnityEngine;
using Visual.Healthbar;

namespace Combat {
	//todo Targatsble Or Damageable
	/// <summary>
	/// Game Object with this component can be targeted
	/// </summary>
	[RequireComponent(typeof(Statistics))]
	public class Targetable : MonoBehaviour {
		// reciving damage -> attack
		// reciving effect? -> healing / debuf
		// is dead?
		// on die / dying
		// droptable ??
		// effects

		[SerializeField] private Statistics statistics;
		[SerializeField] private GridTransform gridTransform;
		[SerializeField] private HealthbarController healthbarController;
		[SerializeField] private VoidEventChannelSO updateFOV_EC;
		
///// Properties ///////////////////////////////////////////////////////////////////////////////////
 	
		public bool IsAlive => !IsDead;
		public bool IsDead => statistics.StatusValues.HitPoints.IsMin();
		

///// Public Functions /////////////////////////////////////////////////////////////////////////////
		
		public void Initialise() {
			statistics = gameObject.GetComponent<Statistics>();
			gridTransform = gameObject.GetComponent<GridTransform>();
			
			if ( healthbarController is { } ) {
				var hitPoints = statistics.StatusValues.HitPoints;
				//todo unsubscribe somewhere ?
				hitPoints.OnValueChanged += () => healthbarController.UpdateVisuals(hitPoints);
				healthbarController.UpdateVisuals(hitPoints);
			}
		}
		
		public void ReceivesDamage(int damage) {
			bool invulnerable = false;
			if ( statistics.Faction == Faction.Player ) {
				invulnerable = PlayerPrefs.GetInt("invulnerable", 0) > 0;
			}

			if ( !invulnerable ) {
				statistics.StatusValues.HitPoints.Decrease(damage);
			}
			
			if ( IsDead ) {
				var healthbar = GetComponentInChildren<HealthbarController>();
				if( healthbar) { 
					healthbar.UpdateVisuals(statistics.StatusValues.HitPoints);
					healthbar.StartHideAfterDelay();
				}

				if ( statistics.Faction == Faction.Player ) {
					updateFOV_EC.RaiseEvent();
				}
			}
		}

		public Vector3Int GetGridPosition() {
			if(gridTransform)
				return gridTransform.gridPosition;
			else {
				Debug.LogError("Targetable without grid position! ");
				return Vector3Int.zero;
			}
		}
		
		public static Targetable[] GetAllInstances() {
			return FindObjectsOfType<Targetable>();
		}
		
		public static List<Targetable> GetTargetsWithPositions(
			List<Targetable> targetables, List<Vector3Int> targetPositons) {

			return targetables.FindAll(target => { 
				var gridTransform = target.GetComponent<GridTransform>();
				if(gridTransform is null) return false;
				return targetPositons.Contains(gridTransform.gridPosition);
			});
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////
		
				
		public static Targetable GetTargetsWithPosition(Vector3Int targetPositon) {
			return new List<Targetable>(GetAllInstances()).Find(target => { 
				var gridTransform = target.GetComponent<GridTransform>();
				if(gridTransform is null) return false;
				return targetPositon.Equals(gridTransform.gridPosition);
			});
		}
	}
}
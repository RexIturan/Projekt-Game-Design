﻿using System.Collections.Generic;
using Characters;
using UnityEngine;
using Visual.Healthbar;

namespace Combat {
	//todo Targatsble Or Damageable
	/// <summary>
	/// Game Object with this component can be targeted
	/// </summary>
	public class Targetable : MonoBehaviour {
		// reciving damage -> attack
		// reciving effect? -> healing / debuf
		// is dead?
		// on die / dying
		// droptable ??
		// effects

		[SerializeField] private Statistics statistics;
		[SerializeField] private GridTransform gridTransform;
		
		public bool IsAlive => !IsDead;
		public bool IsDead => statistics.StatusValues.HitPoints.IsMin();

		public void Initialise() {
			statistics = gameObject.GetComponent<Statistics>();
			gridTransform = gameObject.GetComponent<GridTransform>();
		}
		
		public void ReceivesDamage(int damage) {
			statistics.StatusValues.HitPoints.Decrease(damage);
			
			if ( IsDead ) {
				var healthbar = GetComponentInChildren<HealthbarController>();
				healthbar.UpdateVisuals();
				healthbar.StartHideAfterDelay();	
			}
		}

		public Vector3Int GetGridPosition() {
			return gridTransform.gridPosition;
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
				
		public static Targetable GetTargetsWithPosition(Vector3Int targetPositon) {
			return new List<Targetable>(GetAllInstances()).Find(target => { 
				var gridTransform = target.GetComponent<GridTransform>();
				if(gridTransform is null) return false;
				return targetPositon.Equals(gridTransform.gridPosition);
			});
		}
	}
}
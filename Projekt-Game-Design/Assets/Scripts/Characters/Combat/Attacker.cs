using Characters;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Combat {
	public class Attacker : MonoBehaviour {
		//todo useful
		[SerializeField] private Targetable target;
		[SerializeField] private Vector3Int groundTarget;
		public bool groundTargetSet;
		
		[SerializeField] private List<Vector3Int> tileInRangeOfTarget;
		//todo propertys
		public List<PathNode> tilesInRange;
		public bool waitForAttackToFinish = false;
	
		public float attackRange;
		public int attackDamage;

		public void ClearTilesInRange() {
			tilesInRange.Clear();
		}

		// setter and getter
		public Targetable GetTarget() { return target; }
		public void SetTarget(Targetable target) { this.target = target; }
				
		public Vector3Int GetGroundTarget() { return groundTarget; }
		public void SetGroundTarget(Vector3Int groundTarget) { this.groundTarget = groundTarget; }

		public Vector3Int GetTargetPosition() {
			if(target)
				return target.GetGridPosition();
			else
				return groundTarget;
		}

		/**
		 * Calculates the number of 90 degree rotations 
		 * an attacker has to make to face tile
		 * if the attacker is facing towards the positive x-axis
		 * Important to rotate patterns of abilities
		 */
		public int GetRotationsToTarget(Vector3Int targetPos) { 
			Vector3 vec = targetPos - gameObject.GetComponent<GridTransform>().gridPosition;
			float angle = Vector3.Angle(vec, new Vector3(1, 0, 0));
			
			// if the attacker faces the x-axis, the angle should be below 45 degrees
			if(angle < 45)
				return 1;
			// if the attacker faces the z-axis, the angle from the x-axis should be between 45 and 135 degrees
			else if (angle < 45 + 90) {
				if(vec.z <= 0)
					return 4;
				else
					return 2;
			}
			// if the angle is greater than 135 degrees, the attacker faces negative x-axis, two rotations necessary
			else
				return 3;
    }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using Grid;
using UnityEngine;
using Util;

namespace Characters.Movement {
	public class MovementController : MonoBehaviour {
		
		//todo ref OR find on awake?
		[SerializeField] private Statistics statistics;
		[SerializeField] private GridTransform gridTransform;
		
		//get grid data to snap to grid and calculate movement correctly
		[SerializeField] private GridDataSO gridData;
		
		//Movement Settings
		public float moveSpeed = 5; // Standardwert 5
		public float rotationSpeed = 360.0f;

		//todo transform.rotation.forward?
		public float facingDirection = 0;
		
		//todo settings OR stat value??
		//todo should be in tile distance
		public int movementPointsPerEnergy = 20; // Standardwert 20
		public int movementCostPerTile = 2; // Standardwert 20

		//pathfinding cache
		public List<PathNode> reachableTiles;
		public PathNode movementTarget;

///// Properties ///////////////////////////////////////////////////////////////////////////////////		
		
		//todo animation? 
		public bool MovementDone { get; set; } = true;
		public List<PathNode> PreviewPath { get; set; }

		[SerializeField] private bool isMoving = false;
		
////////////////////////////////////////////////////////////////////////////////////////////////////		

		private GameObject model;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void Move() {
			//todo enter exit cell/tile EC here?
			
			// Move character to position smoothly
			var newPos = 
				Vector3.MoveTowards(
					current: transform.position, 
					target:gridData.GetWorldPosFromGridPos(gridTransform.gridPosition), 
					maxDistanceDelta:moveSpeed * Time.deltaTime);
			
			transform.position = newPos;
		}

		private void RotateModel() {
			// Rotate character to facing position smoothly
			Quaternion target = Quaternion.Euler(0, facingDirection, 0);
			Transform t = model.transform;
			//
			t.rotation = Quaternion.RotateTowards(
				t.rotation,
				target,
				Time.deltaTime * rotationSpeed);
		}
		
////////////////////////////////////////////////////////////////////////////////////////////////////

		#region Monobehaviour

		private void Start() {
			
			//test if references are set
			if(statistics is null)
				Debug.LogError($"MovementController#Start\n statistics is null!");
			
			if(gridData is null)
				Debug.LogError($"MovementController#Start\n gridData is null!");
			
			// set position of gameobject   
			model = GetComponent<ModelController>().Model;
		}

		private void FixedUpdate() {
 
			Move();

			RotateModel();
		}

		#endregion
		
////////////////////////////////////////////////////////////////////////////////////////////////////

		public int GetEnergyUseUpFromMovement() {
			if ( movementTarget is {} ) {
				return GetEnergyUseUpFromMovement(movementTarget);	
			}
			else if( PreviewPath is {} ) {
				var last = PreviewPath.LastOrDefault(); 
				if ( last != null ) {
					return GetEnergyUseUpFromMovement(last);
				} 
			}

			return 0;
		}
		
		public int GetEnergyUseUpFromMovement(PathNode node) {
			if ( node is {} ) {
				return Mathf.CeilToInt(( float )node.dist / movementPointsPerEnergy) * movementCostPerTile;	
			}
			return 0;
		}
		
		public int GetMaxMoveDistance() {
			return statistics.StatusValues.GetValue(StatusType.Energy).Value / movementCostPerTile * movementPointsPerEnergy;
		}
		
		public int GetMaxTileMoveDistance() {
			return statistics.StatusValues.GetValue(StatusType.Energy).Value / movementCostPerTile;
		}

		public void FaceMovingDirection() {
			float minDifference = 0.1f; // the point that's being moved to 
			// has to be at least this far away from the player

			// difference between the "where the player is" and the "where the player goes"
			Vector3 movingDirection = gridData.GetWorldPosFromGridPos(gridTransform.gridPosition) - gameObject.transform.position;

			if ( movingDirection.magnitude > minDifference ) {
				FaceDirection(GetDirectionFromVector(movingDirection));
			}
		}
		
		public void FaceDirection(float direction) {
			facingDirection = direction;
		}
		
		public void FaceDirection(Vector3 vector) {
			FaceDirection(GetDirectionFromVector(vector));
		}

		private static float GetDirectionFromVector(Vector3 vector) { 
			float angle = Vector3.Angle(new Vector3(0, 0, 1), vector);
			if ( vector.x < 0 ) {
				// mirror angle
				angle = -angle + 360;
			}

			return angle;
    }
	}
}
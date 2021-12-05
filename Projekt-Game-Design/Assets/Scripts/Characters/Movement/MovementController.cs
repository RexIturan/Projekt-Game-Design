using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using Util;

namespace Characters.Movement {
	public class MovementController : MonoBehaviour {
		
		//todo ref OR find on awake?
		[SerializeField] private Statistics statistics;
		
		//get grid data to snap to grid and calculate movement correctly
		[SerializeField] private GridDataSO gridData;
		
		//Movement Settings
		public float moveSpeed = 5; // Standardwert 5
		public float rotationSpeed = 360.0f;

		//todo transform.position
		public Vector3 position;
		
		//todo transform.rotation.forward?
		public float facingDirection = 0;
		
		//todo settings OR stat value??
		//todo should be in tile distance
		public int movementPointsPerEnergy = 20; // Standardwert 20

		//pathfinding cache
		public List<PathNode> reachableTiles;
		public PathNode movementTarget;

		//todo animation? 
		public bool MovementDone { get; set; } = true;
		[SerializeField] private bool isMoving = false;
		[SerializeField] private GridTransform gridTransform;
		
////////////////////////////////////////////////////////////////////////////////////////////////////		

		#region Monobehaviour

		private void Start() {
			
			//test if references are set
			if(statistics is null)
				Debug.LogError($"MovementController#Start\n statistics is null!");
			
			if(gridData is null)
				Debug.LogError($"MovementController#Start\n gridData is null!");
			
			// set position of gameobject    
			MoveToGridPosition();
		}

		private void FixedUpdate() {
			// Move character to position smoothly 
			transform.position =
				Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);

			// Rotate character to facing position smoothly
			Quaternion target = Quaternion.Euler(0, facingDirection, 0);
			//
			transform.rotation = Quaternion.RotateTowards(
				transform.rotation,
				target,
				Time.deltaTime * rotationSpeed);
		}

		#endregion
		
////////////////////////////////////////////////////////////////////////////////////////////////////
		
		public void MoveToGridPosition() {
			var pos = gridTransform.gridPosition + gridData.GetCellCenter();
			pos *= gridData.CellSize;
			pos += gridData.OriginPosition;

			gameObject.transform.position = pos;
		}

		public int GetEnergyUseUpFromMovement() {
			return Mathf.CeilToInt(( float )movementTarget.dist / movementPointsPerEnergy);
		}

		public int GetMaxMoveDistance() {
			return statistics.StatusValues.GetValue(StatusType.Energy).value * movementPointsPerEnergy;
		}

		public void FaceMovingDirection() {
			float minDifference = 0.1f; // the point that's being moved to 
			// has to be at least this far away from the player

			Vector3 movingDirection = position - gameObject.transform.position;
			if ( movingDirection.magnitude > minDifference ) {
				float angle = Vector3.Angle(new Vector3(0, 0, 1), movingDirection);
				if ( movingDirection.x < 0 ) {
					// mirror angle
					angle = -angle + 360;
				}

				facingDirection = angle;
			}
		}
		
		public void FaceDirection(float direction) {
			facingDirection = direction;
		}
	}
}
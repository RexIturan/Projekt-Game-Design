using System.Collections.Generic;
using Grid;
using UnityEngine;
using Util;

namespace Characters.Movement {
	public class MovementController : MonoBehaviour {
		
		//Scriptable Object Reference 
		public GridDataSO globalGridData;
		
		//Movement Settings
		public float moveSpeed; // Standardwert 5
		public float rotationSpeed = 360.0f;

		//grid pos cache
		public Vector3Int gridPosition; // within the grid
		
		//todo transform.position
		public Vector3 position;
		
		//todo transform.rotation.forward?
		public float facingDirection;		
		
		//todo settings OR stat value??
		//todo should be in tile distance
		public int movementPointsPerEnergy; // Standardwert 20

		//pathfinding cache
		public List<PathNode> reachableTiles;
		public PathNode movementTarget;

		//todo animation? 
		[SerializeField] private bool movementDone = true;
		[SerializeField] private bool isMoving = false;

		//todo initialise
		private StatusValues stats;
		
////////////////////////////////////////////////////////////////////////////////////////////////////		
		
		public void StartMovement() {
			movementPointsPerEnergy = 20;
			moveSpeed = 5f;
			facingDirection = 0f;
			// set position of gameobject    
			MoveToGridPosition();
		}

		public void MoveToGridPosition() {
			var pos = gridPosition + globalGridData.GetCellCenter();
			pos *= globalGridData.CellSize;
			pos += globalGridData.OriginPosition;

			gameObject.transform.position = pos;
		}

		public int GetEnergyUseUpFromMovement() {
			return Mathf.CeilToInt(( float )movementTarget.dist / movementPointsPerEnergy);
		}

		public int GetMaxMoveDistance() {
			return stats.GetValue(StatusType.Energy).value * movementPointsPerEnergy;
		}

		public void FixedUpdateMovement() {
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
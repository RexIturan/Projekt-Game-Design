using System.Collections.Generic;
using System.Linq;
using Events.ScriptableObjects.GameState;
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
		[SerializeField] private PositionGameObjectEventChannelSO onTileEnterEC;
		[SerializeField] private PositionGameObjectEventChannelSO onTileExitEC;
		
////////////////////////////////////////////////////////////////////////////////////////////////////		

		private GameObject model;
		private int step;
		private Vector3Int previousGridPosition;
		private Vector3Int stepTargetGridPosition;
		private List<PathNode> path;

		//world positions
		private Vector3 startPosition;
		private Vector3 targetPosition;
		
		private float stepMoveDistance;
		private float currentMoveDistance;

		private float tileEnterDistance = 0.3f;
		private float tileExitDistance = 0.3f;

		private bool entered = false;
		private bool exited = false;
		
///// Properties ////////////////////////////////////////////////////////////////////////////		

		private Vector3 CurrentPosition => transform.position;
		private bool ShouldExit => currentMoveDistance >= tileExitDistance;
		private bool ShouldEnter => currentMoveDistance >= stepMoveDistance - tileEnterDistance;
		private bool IsAtTarget => step == path.Count;
		
///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void Move() {
			//todo enter exit cell/tile EC here?

			var previousPosition = transform.position; 
			
			// Move character to position smoothly
			var newPos = 
				Vector3.MoveTowards(
					current: previousPosition, 
					target: gridData.GetWorldPosFromGridPos(gridTransform.gridPosition), 
					maxDistanceDelta: moveSpeed * Time.deltaTime);
			
			transform.position = newPos;
		}

		/// <summary>
		/// MoveTowards the target Grid Position
		/// </summary>
		/// <param name="target"> Target World Position </param>
		/// <returns> Distance Moved </returns>
		private float MoveTo(Vector3 target) {
			
			var previousPosition = transform.position; 
			
			// Move character to position smoothly
			var newPos = 
				Vector3.MoveTowards(
					current: previousPosition, 
					target: target, 
					maxDistanceDelta: moveSpeed * Time.deltaTime);
			
			transform.position = newPos;

			return Vector3.Distance(previousPosition, newPos);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="start">World Position Start</param>
		/// <param name="end">World Position End</param>
		private void SetNewTarget(Vector3 start, Vector3 end) {
			startPosition = start;
			targetPosition = end;
			stepMoveDistance = Vector3.Distance(startPosition, targetPosition);
			currentMoveDistance = 0;

			// reset tile enter exited
			exited = false;
			entered = false;
			
			FaceMovingDirection();
		}
		
		private void OnStepDone() {
			step++;

			if ( !exited ) {
				OnTileExit();
			}

			if ( !entered ) {
				OnTileEntered();
			}
		}

		private void OnTileEntered() {
			Debug.Log($"Enter Tile at {stepTargetGridPosition}");
			
			entered = true;
			// - set new position
			gridTransform.gridPosition = stepTargetGridPosition;
			
			onTileEnterEC.RaiseEvent(stepTargetGridPosition, gameObject);
		}

		private void OnTileExit() {
			Debug.Log($"Exit Tile at {previousGridPosition}");
			exited = true;

			onTileExitEC.RaiseEvent(previousGridPosition, gameObject);
			
			previousGridPosition = stepTargetGridPosition;
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
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

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
			if ( !MovementDone ) {

				if ( currentMoveDistance >= stepMoveDistance ) {
					
					// step done
					OnStepDone();

					if ( IsAtTarget ) {
						MovementDone = true;
					}
					else {
						stepTargetGridPosition = path[step].pos;
						SetNewTarget(CurrentPosition, gridData.GetWorldPosFromGridPos(stepTargetGridPosition));
						
					}
				}

				if ( !MovementDone ) {

					currentMoveDistance += MoveTo(targetPosition);

					//check if exited
					if ( !exited && ShouldExit ) {
						// - trigger on exit
						OnTileExit();
					}

					//check if entered
					if ( !entered && ShouldEnter ) {
						// - trigger on enter
						
						OnTileEntered();
					}
				}
				
				// var position = transform.position;
				//
				// if ( position == stepTarget ) {
				// 	//get new step target 
				// 	step++;
				// 	stepTarget = path[step].pos;
				// 	
				// 	//set gridPosition
				// 	gridTransform.gridPosition = stepTarget;
				// 	//todo trigger on enter
				// }
				//
				// // move to step positon
				// var newPos = MoveTo(stepTarget);
				//
				// // Move();
				//
				// if ( newPos == movementTarget.pos ) {
				// 	MovementDone = true;
				// }
			}

			RotateModel();
		}

		#endregion
		
////////////////////////////////////////////////////////////////////////////////////////////////////

		public void StartNewMove(List<PathNode> path) {
			
			// float timeSinceLastStep = 0;
			
			step = 1;
			MovementDone = false;
			
			// var timePerStep = gridData.CellSize / moveSpeed;
			this.path = path;
			previousGridPosition = gridTransform.gridPosition;
			stepTargetGridPosition = path[step].pos;

			SetNewTarget(transform.position, gridData.GetWorldPosFromGridPos(stepTargetGridPosition));
			

			// corutine
			//
			// if ( currentStep >= path.Count && timeSinceLastStep >= timePerStep )
			// 	MovementDone = true;
			//
			// if ( !MovementDone ) {
			// 	FaceMovingDirection();
			//
			// 	timeSinceLastStep += Time.deltaTime;
			//
			// 	if ( timeSinceLastStep >= timePerStep && currentStep < path.Count ) {
			// 		timeSinceLastStep -= timePerStep;
			//
			// 		gridTransform.gridPosition = path[currentStep].pos;
			//
			// 		currentStep++;
			// 	}
			// }
		}

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
			Vector3 movingDirection = targetPosition - CurrentPosition;

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

		//todo move to util
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
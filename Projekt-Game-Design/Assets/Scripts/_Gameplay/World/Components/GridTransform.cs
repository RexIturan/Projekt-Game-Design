using FullSerializer;
using Grid;
using UnityEngine;

namespace Characters {
	public class GridTransform : MonoBehaviour {
		//todo use gameobject transform and this class just communicates between logic and unity
		//grid pos cache
		public Vector3Int gridPosition; // within the grid
		public Vector3 rotation;

		//TODO get current grid data
		[SerializeField, fsIgnore] private GridDataSO gridData;

		public GridDataSO GetGridData() { return gridData; }

///// Public Methodes //////////////////////////////////////////////////////////////////////////////		
		
		// todo set GameObject to grid Position
		[ContextMenu("MoveToGridPosition")]
		public void MoveToGridPosition() {
			gameObject.transform.position = gridData.GetWorldPosFromGridPos(gridPosition);
		}

		private void RotateToRotation() {
			gameObject.transform.rotation = Quaternion.LookRotation(rotation);
		}
		
		public void MoveTo(Vector3 worldPos) {
			MoveTo(gridData.GetGridPos3DFromWorldPos(worldPos));
		}

		public void MoveTo(Vector3Int gridPos) {
			gridPosition = gridPos;
			MoveToGridPosition();
		}
		
		public void RotateTo(Vector3 newRotation) {
			rotation = newRotation;
			RotateToRotation();
		}

		///// Unity Methodes ///////////////////////////////////////////////////////////////////////////////

		public void Start() {
			MoveToGridPosition();
		}
	}
}
using Grid;
using UnityEngine;

namespace Characters {
	public class GridTransform : MonoBehaviour {
		//grid pos cache
		public Vector3Int gridPosition; // within the grid

		[SerializeField] private GridDataSO gridData;

		public void Start() {
			MoveToGridPosition();
		}

		// todo set GameObject to grid Position
		public void MoveToGridPosition() {
			gameObject.transform.position = gridData.GetWorldPosFromGridPos(gridPosition);
		}
	}
}
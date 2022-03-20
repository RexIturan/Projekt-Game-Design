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

		public void Start() {
			MoveToGridPosition();
		}

		// todo set GameObject to grid Position
		public void MoveToGridPosition() {
			gameObject.transform.position = gridData.GetWorldPosFromGridPos(gridPosition);
		}
	}
}
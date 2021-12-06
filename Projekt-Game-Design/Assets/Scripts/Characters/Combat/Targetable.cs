using Characters;
using UnityEngine;

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

		
		public void ReceivesDamage(int damage) {
			statistics.StatusValues.HitPoints.Decrease(damage);
		}

		public Vector3Int GetGridPosition() {
			return gridTransform.gridPosition;
		}
	}
}
using UnityEngine;

namespace GameManager.ScriptableObjects {
	public abstract class GameEndConditionSO : ScriptableObject {
		public abstract bool CheckCondition();
	}
}
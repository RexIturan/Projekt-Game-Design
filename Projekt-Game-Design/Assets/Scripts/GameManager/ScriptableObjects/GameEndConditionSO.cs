using UnityEngine;

namespace GameManager.ScriptableObjects {
	public abstract class GameEndConditionSO : ScriptableObject {
		public abstract bool CheckCondition();
		
		public virtual void Reset(){}
	}
}
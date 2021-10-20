using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Statemachine.Enemy.Conditions {
	[CreateAssetMenu(fileName = "e_HasTarget",
		menuName = "State Machines/Conditions/Enemy/Has Target")]
	public class HasTargetSO : StateConditionSO {
		protected override Condition CreateCondition() => new HasTarget();
	}

	public class HasTarget : Condition {
		protected new HasTargetSO OriginSO => ( HasTargetSO )base.OriginSO;

		private EnemyCharacterSC _enemyCharacterSC;

		public override void Awake(StateMachine stateMachine) {
			_enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
		}

		protected override bool Statement() {
			return !( _enemyCharacterSC.target is null );
		}

		public override void OnStateEnter() { }

		public override void OnStateExit() { }
	}
}
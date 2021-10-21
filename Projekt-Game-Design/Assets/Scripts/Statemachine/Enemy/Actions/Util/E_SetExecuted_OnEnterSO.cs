using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions {
	[CreateAssetMenu(fileName = "e_SetExecuted_OnEnter",
		menuName = "State Machines/Actions/Enemy/e_SetExecuted_OnEnter")]
	public class E_SetExecuted_OnEnterSO : StateActionSO {
		public override StateAction CreateAction() => new E_SetExecuted_OnEnter();
	}

	public class E_SetExecuted_OnEnter : StateAction {
		private EnemyCharacterSC _enemyCharacterSC;

		public override void Awake(UOP1.StateMachine.StateMachine stateMachine) {
			_enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
		}

		public override void OnUpdate() { }

		public override void OnStateEnter() {
			_enemyCharacterSC.abilityExecuted = true;
		}

		public override void OnStateExit() { }
	}
}
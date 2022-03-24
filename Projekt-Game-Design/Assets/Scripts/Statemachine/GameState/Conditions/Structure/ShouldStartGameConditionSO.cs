using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Statemachine.GameState.Conditions.Structure {
	[CreateAssetMenu(fileName = "g_ShouldStartGame", menuName = "State Machines/Conditions/GameState/start game")]
	public class ShouldStartGameConditionSO : StateConditionSO
	{
		protected override Condition CreateCondition() => new ShouldStartGameCondition();
	}

	public class ShouldStartGameCondition : Condition	{
		private GameSC _gameSc;

		public override void Awake(StateMachine stateMachine) {
			_gameSc = stateMachine.gameObject.GetComponent<GameSC>();
		}

		protected override bool Statement() {
			return _gameSc.start;
		}
	
		public override void OnStateEnter() {}
	
		public override void OnStateExit() {}
	}
}




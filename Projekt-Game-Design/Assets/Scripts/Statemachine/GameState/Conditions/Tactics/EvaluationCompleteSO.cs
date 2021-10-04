using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Statemachine.GameState.Conditions {
	[CreateAssetMenu(fileName = "g_EvaluationComplete", menuName = "State Machines/Conditions/GameState/Evaluation Complete")]
	public class EvaluationCompleteSO : StateConditionSO
	{
		protected override Condition CreateCondition() => new EvaluationComplete();
	}

	public class EvaluationComplete : Condition
	{
		protected new EvaluationCompleteSO OriginSO => (EvaluationCompleteSO)base.OriginSO;

		private GameSC gameSC;
	
		public override void Awake(StateMachine stateMachine) {
			gameSC = stateMachine.gameObject.GetComponent<GameSC>();
		}
	
		protected override bool Statement()
		{
			return gameSC.evaluated;
		}
	
		public override void OnStateEnter()
		{
		}
	
		public override void OnStateExit()
		{
		}
	}
}
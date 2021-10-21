using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Statemachine.Enemy.Conditions {
	[CreateAssetMenu(fileName = "e_NoTargetFound", menuName = "State Machines/Conditions/Enemy/No Target Found")]
	public class NoTargetFoundSO : StateConditionSO
	{
		protected override Condition CreateCondition() => new NoTargetFound();
	}

	public class NoTargetFound : Condition
	{
		protected new NoTargetFoundSO OriginSO => (NoTargetFoundSO)base.OriginSO;
	
		private EnemyCharacterSC _enemyCharacterSC;
	
		public override void Awake(StateMachine stateMachine) {
			_enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
		}
	
		protected override bool Statement()
		{
			return _enemyCharacterSC.noTargetFound;
		}
	
		public override void OnStateEnter()
		{
		}
	
		public override void OnStateExit()
		{
		}
	}
}
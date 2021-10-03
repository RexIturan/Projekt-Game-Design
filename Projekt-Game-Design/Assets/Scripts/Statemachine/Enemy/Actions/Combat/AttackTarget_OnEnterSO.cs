using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Statemachine.Enemy.Actions.Combat {
	[CreateAssetMenu(fileName = "AttackTarget_OnEnter", menuName = "State Machines/Actions/Enemy/Combat/Attack Target_On Enter")]
	public class AttackTarget_OnEnterSO : StateActionSO
	{
		public override StateAction CreateAction() => new AttackTarget_OnEnter();
	}

	public class AttackTarget_OnEnter : StateAction
	{
		protected new AttackTarget_OnEnterSO OriginSO => (AttackTarget_OnEnterSO)base.OriginSO;
		private EnemyCharacterSC enemyCharacterSC;

		public override void Awake(StateMachine stateMachine) {
			enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
		}
	
		public override void OnUpdate()
		{
		}
	
		public override void OnStateEnter() {
			var attackEnergyCost = 1;
			if (enemyCharacterSC.energy >= attackEnergyCost) {
				Debug.Log($"enemy attacked player for {enemyCharacterSC.attackDamage} damage");
				enemyCharacterSC.target.healthPoints -= enemyCharacterSC.attackDamage;
				// todo move somewhere else
				enemyCharacterSC.energy -= attackEnergyCost;
				enemyCharacterSC.abilityExecuted = true;	
			}
			else {
				enemyCharacterSC.isDone = true;
			}
		}
	
		public override void OnStateExit()
		{
		}
	}
}
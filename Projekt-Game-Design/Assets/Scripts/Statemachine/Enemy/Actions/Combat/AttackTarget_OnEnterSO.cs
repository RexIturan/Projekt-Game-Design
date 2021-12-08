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
		private EnemyCharacterSC _enemyCharacterSC;

		public override void Awake(StateMachine stateMachine) {
			_enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
		}
	
		public override void OnUpdate()
		{
		}
	
		public override void OnStateEnter() {
						/*
			var attackEnergyCost = 1;
			if (_enemyCharacterSC.energy >= attackEnergyCost) {
				Debug.Log($"enemy attacked player for {_enemyCharacterSC.attackDamage} damage");
				//todo(combat) use compat system to determin damage and dealing damage

				var damage = _enemyCharacterSC.attackDamage;
				_enemyCharacterSC.target.ReceivesDamage(damage);
				// todo move somewhere else
				_enemyCharacterSC.energy -= attackEnergyCost;
				_enemyCharacterSC.abilityExecuted = true;	
			}
			else {
				_enemyCharacterSC.isDone = true;
			}
			*/
		}
	
		public override void OnStateExit()
		{
		}
	}
}
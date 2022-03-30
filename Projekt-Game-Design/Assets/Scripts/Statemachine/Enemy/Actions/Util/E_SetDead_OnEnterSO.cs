using System;
using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[Obsolete]
[CreateAssetMenu(fileName = "e_SetDead_OnEnter", menuName = "State Machines/Actions/Enemy/Set Dead On Enter")]
public class E_SetDead_OnEnterSO : StateActionSO
{
		public override StateAction CreateAction() => new E_SetDead_OnEnter();
}

[Obsolete]
public class E_SetDead_OnEnter : StateAction
{
		private EnemyCharacterSC _enemy;
				
		public override void OnUpdate() { }

		public override void Awake(StateMachine stateMachine)
		{
				_enemy = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
		}
		
		public override void OnStateEnter() {}
}
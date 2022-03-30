using System;
using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[Obsolete]
[CreateAssetMenu(fileName = "e_DestroySelf_OnEnterSO", menuName = "State Machines/Actions/Enemy/Destroy Self On Enter")]
public class E_DestroySelf_OnEnterSO : StateActionSO
{
		public override StateAction CreateAction() => new E_DestroySelf_OnEnter();
}

[Obsolete]
public class E_DestroySelf_OnEnter : StateAction
{
		private GameObject _enemy;
				
		public override void OnUpdate() { }

		public override void Awake(StateMachine stateMachine)
		{
				_enemy = stateMachine.gameObject;
		}

		public override void OnStateEnter()
		{
				// CharacterList characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
				// characterList.enemyContainer.Remove(_enemy);
				// characterList.deadEnemies.Add(_enemy);
		}
}
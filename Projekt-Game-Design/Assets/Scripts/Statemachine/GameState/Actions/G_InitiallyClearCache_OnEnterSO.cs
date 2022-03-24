using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_InitiallyClearCache_OnEnter", menuName = "State Machines/Actions/GameState/Initially Clear Cache On Enter")]
public class G_InitiallyClearCache_OnEnterSO : StateActionSO
{
		public override StateAction CreateAction() => new G_InitiallyClearCache_OnEnter();
}

public class G_InitiallyClearCache_OnEnter : StateAction
{
		private GameSC _gameSC;

		public override void Awake(StateMachine stateMachine) {
				_gameSC = stateMachine.gameObject.GetComponent<GameSC>();
		}

		public override void OnUpdate() { }

		public override void OnStateEnter() {
			_gameSC.ResetGameState();
		}

		public override void OnStateExit() { }
}
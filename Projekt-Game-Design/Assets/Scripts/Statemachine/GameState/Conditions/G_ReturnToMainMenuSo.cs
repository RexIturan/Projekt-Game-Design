using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Statemachine.GameState.Conditions {
	[CreateAssetMenu(fileName = "g_ReturnToMainMenu", menuName = "State Machines/Conditions/Return To Main Menu")]
	public class G_ReturnToMainMenuSo : StateConditionSO{
		protected override Condition CreateCondition() => new G_ReturnToMainMenu();	
	}

	public class G_ReturnToMainMenu : Condition {
		protected new G_AlwaysFalseSO OriginSO => (G_AlwaysFalseSO)base.OriginSO;

		private GameSC _gameSC;
		
		public override void Awake(StateMachine stateMachine) {
			_gameSC = stateMachine.GetComponent<GameSC>();
		}
	
		protected override bool Statement()
		{
			return _gameSC.reload;
		}
	
		public override void OnStateEnter()
		{
		}
	
		public override void OnStateExit() {
			_gameSC.reload = false;
		}
	}
}

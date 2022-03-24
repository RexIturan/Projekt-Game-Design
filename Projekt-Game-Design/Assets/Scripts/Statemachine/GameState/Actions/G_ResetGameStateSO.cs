using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "G_ResetGameState", menuName = "State Machines/Actions/GameState/G_Reset Game State")]
public class G_ResetGameStateSO : StateActionSO {
	[SerializeField] private StateAction.SpecificMoment phase;
	public override StateAction CreateAction() => new G_ResetGameState(phase);
}

public class G_ResetGameState : StateAction {
	private readonly StateAction.SpecificMoment phase;
	private GameSC _gameSC;
	
	public G_ResetGameState(StateAction.SpecificMoment phase){
		this.phase = phase;
	}
	
	public override void Awake(StateMachine stateMachine){
		_gameSC = stateMachine.GetComponent<GameSC>();
	}
	
	public override void OnUpdate() {
		if ( phase == SpecificMoment.OnUpdate ) {
			_gameSC.ResetGameState();
		}
	}

	public override void OnStateEnter() {
		if ( phase == SpecificMoment.OnStateEnter ) {
			_gameSC.ResetGameState();
		}
	}

	public override void OnStateExit() {
		if ( phase == SpecificMoment.OnStateExit ) {
			_gameSC.ResetGameState();
		}
	}
}

using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using static SaveManagerAction;
using static SaveManagerTiming;

[CreateAssetMenu(fileName = "SaveManagerConditions", menuName = "State Machines/Conditions/GameState/Save Manager Conditions")]
public class SaveManagerConditionsSO : StateConditionSO {
	[SerializeField] private SaveManagerTiming timing;
	[SerializeField] private SaveManagerAction action;
	protected override Condition CreateCondition() => new SaveManagerConditions(timing, action);
}

public class SaveManagerConditions : Condition
{
	protected new SaveManagerConditionsSO OriginSO => (SaveManagerConditionsSO)base.OriginSO;
	private readonly SaveManagerTiming _timing;
	private readonly SaveManagerAction _action;
	private GameSC _gameSc;
	
	public SaveManagerConditions(SaveManagerTiming timing, SaveManagerAction action) {
		this._timing = timing;
		this._action = action;
	}

	public override void Awake(StateMachine stateMachine) {
		_gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}
	
	protected override bool Statement() {
		bool statement = false;
		
		switch (timing: _timing, action: _action) {
			case (SaveManagerTiming.Input, Load):
				statement = _gameSc.saveManagerData.inputLoad;
				break;
			case (SaveManagerTiming.Input, NewGame):
				statement = _gameSc.saveManagerData.inputNewGame;
				break;
			case (SaveManagerTiming.Input, Save):
				statement = _gameSc.saveManagerData.inputSave;
				break;
			case (Finished, Save):
				statement = _gameSc.saveManagerData.saved;
				break;
			case (Finished, Load):
			case (Finished, NewGame):
				statement = _gameSc.saveManagerData.loaded;
				break;
		}
		return statement;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}

public enum SaveManagerTiming {
	Input,
	Finished
}

public enum SaveManagerAction {
	Load,
	Save,
	NewGame
}
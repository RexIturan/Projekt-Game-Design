using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using static ESaveManagerAction;
using static ESaveManagerTiming;

[CreateAssetMenu(fileName = "SaveManagerConditions", menuName = "State Machines/Conditions/GameState/Save Manager Conditions")]
public class SaveManagerConditionsSO : StateConditionSO {
	[SerializeField] private ESaveManagerTiming timing;
	[SerializeField] private ESaveManagerAction action;
	protected override Condition CreateCondition() => new SaveManagerConditions(timing, action);
}

public class SaveManagerConditions : Condition
{
	protected new SaveManagerConditionsSO OriginSO => (SaveManagerConditionsSO)base.OriginSO;
	private readonly ESaveManagerTiming timing;
	private readonly ESaveManagerAction action;
	private GameSC gameSc;
	
	public SaveManagerConditions(ESaveManagerTiming timing, ESaveManagerAction action) {
		this.timing = timing;
		this.action = action;
	}

	public override void Awake(StateMachine stateMachine) {
		gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}
	
	protected override bool Statement() {
		bool statement = false;
		
		switch (timing, action) {
			case (input, load):
				statement = gameSc.saveManagerData.inputLoad;
				break;
			case (input, newGame):
				statement = gameSc.saveManagerData.inputNewGame;
				break;
			case (input, save):
				statement = gameSc.saveManagerData.inputSave;
				break;
			case (finished, save):
				statement = gameSc.saveManagerData.saved;
				break;
			case (finished, load):
			case (finished, newGame):
				statement = gameSc.saveManagerData.loaded;
				break;
			default:
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

public enum ESaveManagerTiming {
	input,
	finished
}

public enum ESaveManagerAction {
	load,
	save,
	newGame
}
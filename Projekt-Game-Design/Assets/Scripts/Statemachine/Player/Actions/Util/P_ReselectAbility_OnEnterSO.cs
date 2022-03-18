using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Characters;
using GDP01.Characters.Component;

[CreateAssetMenu(fileName = "p_ReselectAbility_OnEnter",
	menuName = "State Machines/Actions/Player/Reselect Ability On Enter")]
public class P_ReselectAbility_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new P_ReselectAbility_OnEnter();
}

public class P_ReselectAbility_OnEnter : StateAction {
	private AbilityController _abilityController;
	private Statistics _statistics;
	private readonly GameObjActionIntEventChannelSO _selectPlayerEC;

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_statistics = stateMachine.gameObject.GetComponent<Statistics>();
	}

	public override void OnStateEnter() {
		AbilitySO lastAbility = _abilityController.GetLastSelectedAbility();

		if(lastAbility && lastAbility.repeated) {
			if( _statistics.StatusValues.Energy.Value >= lastAbility.costs ) { 
				_abilityController.SelectedAbilityID = _abilityController.LastSelectedAbilityID;
				_abilityController.abilitySelected = true;
			}
			else
			  _abilityController.LastSelectedAbilityID = -1;
		}
	}
}
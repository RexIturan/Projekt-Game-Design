using System.Collections.Generic;
using Ability.ScriptableObjects;
using Characters.Ability;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_SuperAction",
	menuName = "State Machines/Actions/Enemy/e_SuperAction")]
public class E_SuperActionSO : StateActionSO {
	[SerializeField] private AbilityPhase abilityPhase;
	[SerializeField] private AbilityContainerSO abilityContainer;
	public override StateAction CreateAction() => new E_SuperAction(abilityPhase, abilityContainer);
}

public class E_SuperAction : StateAction {
	protected new SuperActionSO OriginSO => ( SuperActionSO )base.OriginSO;

	// input for each
	private readonly AbilityContainerSO _abilityContainer;
	private readonly AbilityPhase _phase;

	// 
	private StateMachine _stateMachine;
	private List<StateAction> _subActions;
	private int _abilityID;
	private EnemyCharacterSC _enemyCharacterSC;
  private AbilityController _abilityController;

	public E_SuperAction(AbilityPhase phase, AbilityContainerSO abilityContainer) {
		this._phase = phase;
		this._abilityContainer = abilityContainer;
	}

	public override void Awake(StateMachine stateMachine) {
		this._stateMachine = stateMachine;
		_enemyCharacterSC = stateMachine.GetComponent<EnemyCharacterSC>();
		_abilityController = stateMachine.GetComponent<AbilityController>();
	}

	public override void OnStateEnter() {
		_subActions = new List<StateAction>();
		// get Actions from Ability
		_abilityID = _abilityController.SelectedAbilityID;
		var ability = _abilityContainer.abilities[_abilityID];
		StateActionSO[] actions;

		if ( _phase == AbilityPhase.Selected ) {
			actions = ability.selectedActions.ToArray();
		}
		else {
			actions = ability.executingActions.ToArray();
		}

		foreach ( var actionSo in actions ) {
			var action = actionSo.CreateAction();
			_subActions.Add(action);
		}

		foreach ( var action in _subActions ) {
			action.Awake(_stateMachine);
			action.OnStateEnter();
		}
	}

	public override void OnUpdate() {
		if ( _subActions != null ) {
			foreach ( var action in _subActions ) {
				action.OnUpdate();
			}
		}
	}

	public override void OnStateExit() {
		foreach ( var action in _subActions ) {
			action.OnStateExit();
		}
	}
}
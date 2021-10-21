﻿using System.Collections.Generic;
using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SuperAction", menuName = "State Machines/Actions/Super Action")]
public class SuperActionSO : StateActionSO {
	[SerializeField] private AbilityPhase abilityPhase;
	[SerializeField] private AbilityContainerSO abilityContainer;
	public override StateAction CreateAction() => new SuperAction(abilityPhase, abilityContainer);
}

public class SuperAction : StateAction
{
	protected new SuperActionSO OriginSO => (SuperActionSO)base.OriginSO;

	// input for each
	private AbilityContainerSO _abilityContainer;
	private AbilityPhase _phase;
	
	// 
	private StateMachine _stateMachine;
	private List<StateAction> _subActions;
	private int _abilityID;
	private PlayerCharacterSC _playerCharacterSC;

	public SuperAction(AbilityPhase phase, AbilityContainerSO abilityContainer) {
		this._phase = phase;
		this._abilityContainer = abilityContainer;
	}
	
	public override void Awake(StateMachine stateMachine) {
		this._stateMachine = stateMachine;
		_playerCharacterSC = stateMachine.GetComponent<PlayerCharacterSC>();
	}
	
	public override void OnStateEnter() {
		_subActions = new List<StateAction>();
		// get Actions from Ability
		_abilityID = _playerCharacterSC.AbilityID;
		var ability = _abilityContainer.abilities[_abilityID];
		StateActionSO[] actions;

		if (_phase == AbilityPhase.Selected) {
			actions = ability.selectedActions.ToArray();
		}
		else {
			actions = ability.executingActions.ToArray();
		}
		
		foreach (var actionSo in actions) {
			var action = actionSo.CreateAction();
			_subActions.Add(action);
		}

		foreach (var action in _subActions) {
			action.Awake(_stateMachine);
			action.OnStateEnter();
		}
	}
	
	public override void OnUpdate()
	{
		if (_subActions != null) {
			foreach (var action in _subActions) {
				action.OnUpdate();
			}	
		}
	}
	
	public override void OnStateExit()
	{
		foreach (var action in _subActions) {
			action.OnStateExit();
		}
	}

	// enum AbilityPhase {
	// 	selected,
	// 	executing
	// }
}

public enum AbilityPhase {
	Selected,
	Executing
}
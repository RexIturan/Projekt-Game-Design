using System.Collections.Generic;
using Characters.Ability;
using Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;
using Ability;

/// <summary>
/// Sets the singleTarget flag in the ability controller 
/// if ability only has one proper target at the moment
/// </summary>
[CreateAssetMenu(fileName = "c_SetSingleTargetFlag_OnEnter",
	menuName = "State Machines/Actions/Character/Set Single Target Flag On Enter")]
public class C_SetSingleTargetFlag_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() =>
		new C_SetSingleTargetFlag_OnEnter();
}

public class C_SetSingleTargetFlag_OnEnter : StateAction {
	private Attacker _attacker;
	private AbilityController _abilityController;

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
	}
	
	/// <summary>
	/// Sets the singleTarget flag in the ability controller 
	/// if ability only has one proper target at the moment
	/// </summary>
	public override void OnStateEnter() {
		List<Vector3Int> range = PathNode.ConvertPathNodeListToVector3IntList(_attacker.tilesInRange);

		// ability is single target if it either:
		// targets ground and has one tile in its range or
		// there is one targetable in its range 
		if ( _abilityController.GetSelectedAbility().targets.HasFlag(AbilityTarget.Ground) && range.Count == 1 ) { 
			_abilityController.singleTargetPos = range[0];
			_abilityController.singleTarget = true;
		}
		else {
			List<Targetable> targetables = CombatUtils.FindAllTargets(range, _attacker, _abilityController.GetSelectedAbility().targets);

			if (!_abilityController.GetSelectedAbility().targets.HasFlag(AbilityTarget.Ground) && targetables.Count == 1) { 
				_abilityController.singleTargetPos = targetables[0].GetGridPosition();
				_abilityController.singleTarget = true;
			}
			else { 
				_abilityController.singleTargetPos = Vector3Int.zero;
				_abilityController.singleTarget = false;
			}
		}
	}
}
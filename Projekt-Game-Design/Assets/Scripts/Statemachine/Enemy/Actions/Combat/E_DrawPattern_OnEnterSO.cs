using Ability.ScriptableObjects;
using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using GDP01.Characters.Component;
using GDP01.World.Components;

[CreateAssetMenu(fileName = "e_DrawPattern_OnEnter", menuName = "State Machines/Actions/Enemy/Draw Pattern On Enter")]
public class E_DrawPattern_OnEnterSO : StateActionSO {
	[Header("Sending Events On")]
	[SerializeField] private DrawPatternEventChannelSO drawPatternEC;

	public override StateAction CreateAction() => new E_DrawPattern_OnEnter(drawPatternEC);
}

public class E_DrawPattern_OnEnter : StateAction {
	private readonly DrawPatternEventChannelSO _drawPatternEC;

	private Attacker _attacker;
	private AbilityController _abilityController;
		
	public E_DrawPattern_OnEnter(DrawPatternEventChannelSO drawPatternEC) {
		_drawPatternEC = drawPatternEC;
	}

	public override void Awake(StateMachine stateMachine) {
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() {
		AbilitySO ability = _abilityController.SelectedAbility;

		if(ability.targetedEffects.Length <= 0 || ability.targetedEffects[0].area == null)
			return;

		Vector3Int targetPos = _attacker.groundTargetSet ? _attacker.GetGroundTarget() : _attacker.GetTargetPosition();
		int rotations = _attacker.GetRotationsToTarget(targetPos);
		_drawPatternEC.RaiseEvent(targetPos,
			ability.targetedEffects[0].area.GetRotatedPattern(rotations),
			ability.targetedEffects[0].area.GetRotatedAnchor(rotations));
	}

	public override void OnStateExit() { }
}
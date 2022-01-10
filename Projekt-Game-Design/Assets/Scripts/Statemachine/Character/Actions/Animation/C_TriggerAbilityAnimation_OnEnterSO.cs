using Characters;
using Characters.Ability;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_TriggerAbilityAnimation_OnEnter",
	menuName = "State Machines/Actions/Character/Trigger Ability Animation On Enter")]
public class C_TriggerAbilityAnimation_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new C_TriggerAbilityAnimation_OnEnter();
}

public class C_TriggerAbilityAnimation_OnEnter : StateAction {
	private ModelController _modelController;
  private AbilityController _abilityController;
	private readonly CharacterAnimation _characterAnimation;

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_modelController = stateMachine.gameObject.GetComponent<ModelController>();
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	public override void OnStateEnter() {
		CharacterAnimationController controller = _modelController.GetAnimationController();
		if ( controller )
			controller.PlayAnimation(_abilityController.GetSelectedAbility().Animation);
		else
			Debug.Log("Couldn't play animation. Animation controller not found. ");
	}
}
using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_TriggerAnimation_OnEnter",
	menuName = "State Machines/Actions/Character/Trigger Animation On Enter")]
public class C_TriggerAnimation_OnEnterSO : StateActionSO {
	[SerializeField] private CharacterAnimation characterAnimation;

	public override StateAction CreateAction() => new C_TriggerAnimation_OnEnter(characterAnimation);
}

public class C_TriggerAnimation_OnEnter : StateAction {
	private ModelController modelController;
	private readonly CharacterAnimation _characterAnimation;

	public C_TriggerAnimation_OnEnter(CharacterAnimation characterAnimation) {
		this._characterAnimation = characterAnimation;
	}

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		modelController = stateMachine.gameObject.GetComponent<ModelController>();
	}

	public override void OnStateEnter() {
		CharacterAnimationController controller = modelController.GetAnimationController();
		if ( controller )
			controller.PlayAnimation(_characterAnimation);
		else
			Debug.Log("Couldn't play animation. Animation controller not found. ");
	}
}
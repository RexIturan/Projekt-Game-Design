using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_TriggerAnimation_OnEnter", menuName = "State Machines/Actions/Player/TriggerAnimation")]
public class P_TriggerAnimation_OnEnterSO : StateActionSO {
    [SerializeField] private AnimationType animation;

	public override StateAction CreateAction() => new P_TriggerAnimation_OnEnter(animation);
}

public class P_TriggerAnimation_OnEnter : StateAction {
	private PlayerCharacterSC player;
    private AnimationType animation;

    public P_TriggerAnimation_OnEnter(AnimationType animation) {
        this.animation = animation;
    }

    public override void OnUpdate() {
    }

    public override void Awake(StateMachine stateMachine) {
        player = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnStateEnter() {
		CharacterAnimationController controller = player.GetAnimationController();
		// controller.PlayAnimation(animation);
    }
}

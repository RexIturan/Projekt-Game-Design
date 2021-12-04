using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_TriggerAnimation_OnEnter", menuName = "State Machines/Actions/Player/TriggerAnimation")]
public class P_TriggerAnimation_OnEnterSO : StateActionSO {
    [SerializeField] private CharacterAnimation characterAnimation;

	public override StateAction CreateAction() => new P_TriggerAnimation_OnEnter(characterAnimation);
}

public class P_TriggerAnimation_OnEnter : StateAction {
	private PlayerCharacterSC player;
    private CharacterAnimation _characterAnimation;

    public P_TriggerAnimation_OnEnter(CharacterAnimation characterAnimation) {
        this._characterAnimation = characterAnimation;
    }

    public override void OnUpdate() {
    }

    public override void Awake(StateMachine stateMachine) {
        player = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnStateEnter() {
			CharacterAnimationController controller = player.GetAnimationController();
				if ( controller )
						controller.PlayAnimation(_characterAnimation);
				else
						Debug.Log("Couldn't play animation. Animation controller not found. ");
    }
}

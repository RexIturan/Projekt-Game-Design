using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_TriggerAnimation_OnEnter", menuName = "State Machines/Actions/Enemy/TriggerAnimation")]
public class E_TriggerAnimation_OnEnterSO : StateActionSO {
    [SerializeField] private AnimationType animation;

    public override StateAction CreateAction() => new E_TriggerAnimation_OnEnter(animation);
}

public class E_TriggerAnimation_OnEnter : StateAction
{
	private EnemyCharacterSC enemy;
	private AnimationType animation;

	public E_TriggerAnimation_OnEnter(AnimationType animation)
	{
		this.animation = animation;
	}

	public override void OnUpdate()
	{
	}

	public override void Awake(StateMachine stateMachine)
	{
		enemy = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	public override void OnStateEnter()
	{
		CharacterAnimationController controller = enemy.GetAnimationController();
		controller.PlayAnimation(animation);
	}
}

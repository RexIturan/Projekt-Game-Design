using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_TriggerAnimation_OnEnter", menuName = "State Machines/Actions/Enemy/TriggerAnimation")]
public class E_TriggerAnimation_OnEnterSO : StateActionSO {
    [SerializeField] private CharacterAnimation characterAnimation;

    public override StateAction CreateAction() => new E_TriggerAnimation_OnEnter(characterAnimation);
}

public class E_TriggerAnimation_OnEnter : StateAction
{
	private EnemyCharacterSC enemy;
	private CharacterAnimation _characterAnimation;

	public E_TriggerAnimation_OnEnter(CharacterAnimation characterAnimation)
	{
		this._characterAnimation = characterAnimation;
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
		controller.PlayAnimation(_characterAnimation);
	}
}

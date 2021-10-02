using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "PlayAbilityConfirmedAnimation", menuName = "State Machines/Actions/Player/PlayAbilityConfirmedAnimation")]
public class PlayAbilityConfirmedAnimationSO : StateActionSO
{
    public override StateAction CreateAction() => new PlayAbilityConfirmedAnimation();
}

public class PlayAbilityConfirmedAnimation : StateAction
{
    private StateMachine stateMachine;

    public PlayAbilityConfirmedAnimation()
    {

    }

    public override void OnUpdate()
    {
    }

    public override void Awake(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void OnStateEnter()
    {
        Debug.Log("Ich faerbe mich jetzt gelb (ability confirmed)");
        MeshRenderer rend = stateMachine.GetComponent<MeshRenderer>();
        rend.material.color = Color.yellow;
    }
}

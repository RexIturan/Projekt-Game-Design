using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "PlayExecuteAbilityAnimation", menuName = "State Machines/Actions/Player/PlayExecuteAbilityAnimation")]
public class PlayExecuteAbilityAnimationSO : StateActionSO
{
    public override StateAction CreateAction() => new PlayExecuteAbilityAnimation();
}

public class PlayExecuteAbilityAnimation : StateAction
{
    private StateMachine stateMachine;

    public PlayExecuteAbilityAnimation()
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
        Debug.Log("Ich faerbe mich jetzt rot (execution)");
        MeshRenderer rend = stateMachine.GetComponent<MeshRenderer>();
        rend.material.color = Color.red;
    }
}

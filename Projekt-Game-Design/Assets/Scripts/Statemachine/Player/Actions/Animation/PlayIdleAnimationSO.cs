using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "PlayIdleAnimation", menuName = "State Machines/Actions/Player/PlayIdleAnimation")]
public class PlayIdleAnimationSO : StateActionSO
{
    public override StateAction CreateAction() => new PlayIdleAnimation();
}

public class PlayIdleAnimation : StateAction
{
    private StateMachine _stateMachine;

    public override void OnUpdate()
    {
    }

    public override void Awake(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public override void OnStateEnter()
    {
        Debug.Log("Ich faerbe mich jetzt blau (idle)");
        MeshRenderer rend = _stateMachine.GetComponent<MeshRenderer>();
        rend.material.color = Color.blue;
    }
}

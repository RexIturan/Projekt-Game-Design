using Events.ScriptableObjects;
using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "PlaySelectedAnimation", menuName = "State Machines/Actions/Player/PlaySelectedAnimation")]
public class PlaySelectedAnimationSO : StateActionSO
{
    protected override StateAction CreateAction() => new PlaySelectedAnimation();
}

public class PlaySelectedAnimation : StateAction
{
    private StateMachine stateMachine;

    public PlaySelectedAnimation()
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
        Debug.Log("Ich faerbe mich jetzt magenta (selected)");
        MeshRenderer rend = stateMachine.GetComponent<MeshRenderer>();
        rend.material.color = Color.magenta;
    }
}

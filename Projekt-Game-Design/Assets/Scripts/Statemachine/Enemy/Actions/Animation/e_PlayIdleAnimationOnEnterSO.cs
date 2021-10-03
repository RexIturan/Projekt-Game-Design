using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_PlayIdleAnimationOnEnter", menuName = "State Machines/Actions/Enemy/e_PlayIdleAnimationOnEnter")]
public class e_PlayIdleAnimationOnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new e_PlayIdleAnimationOnEnter();
}

public class e_PlayIdleAnimationOnEnter : StateAction
{
    private StateMachine stateMachine;

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void OnStateEnter()
    {
        // Debug.Log("Ich faerbe mich jetzt blau (idle)");
        MeshRenderer rend = stateMachine.GetComponent<MeshRenderer>();
        rend.material.color = Color.blue;
    }
}

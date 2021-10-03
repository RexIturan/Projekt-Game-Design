using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_PlayExecuteAbilityAnimationOnEnter", menuName = "State Machines/Actions/Enemy/e_PlayExecuteAbilityAnimationOnEnter")]
public class e_PlayExecuteAbilityAnimationOnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new e_PlayExecuteAbilityAnimationOnEnter();
}

public class e_PlayExecuteAbilityAnimationOnEnter : StateAction
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
        // Debug.Log("Ich faerbe mich jetzt rot (execute ability)");
        MeshRenderer rend = stateMachine.GetComponent<MeshRenderer>();
        rend.material.color = Color.red;
    }
}

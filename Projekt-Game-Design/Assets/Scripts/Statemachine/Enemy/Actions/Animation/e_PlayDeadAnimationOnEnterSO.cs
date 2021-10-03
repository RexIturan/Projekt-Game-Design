using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_PlayDeadAnimationOnEnter", menuName = "State Machines/Actions/Enemy/e_PlayDeadAnimationOnEnter")]
public class e_PlayDeadAnimationOnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new e_PlayDeadAnimationOnEnter();
}

public class e_PlayDeadAnimationOnEnter : StateAction
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
        // Debug.Log("Ich faerbe mich jetzt grau (dead)");
        MeshRenderer rend = stateMachine.GetComponent<MeshRenderer>();
        rend.material.color = Color.grey;
    }
}

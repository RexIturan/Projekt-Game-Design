using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_PlayDeadAnimationOnEnter", menuName = "State Machines/Actions/Enemy/e_PlayDeadAnimationOnEnter")]
public class E_PlayDeadAnimationOnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new E_PlayDeadAnimationOnEnter();
}

public class E_PlayDeadAnimationOnEnter : StateAction
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
        // Debug.Log("Ich faerbe mich jetzt grau (dead)");
        MeshRenderer rend = _stateMachine.GetComponent<MeshRenderer>();
        rend.material.color = Color.grey;
    }
}

using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_PlaySearchAnimationOnEnter", menuName = "State Machines/Actions/Enemy/e_PlaySearchAnimationOnEnter")]
public class E_PlaySearchAnimationOnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new E_PlaySearchAnimationOnEnter();
}

public class E_PlaySearchAnimationOnEnter : StateAction
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
        // Debug.Log("Ich faerbe mich jetzt gelb (search)");
        MeshRenderer rend = _stateMachine.GetComponent<MeshRenderer>();
        rend.material.color = Color.yellow;
    }
}

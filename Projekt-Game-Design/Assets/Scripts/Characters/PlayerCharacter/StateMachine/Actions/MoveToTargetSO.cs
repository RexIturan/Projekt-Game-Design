using Events.ScriptableObjects;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "MoveToTarget", menuName = "State Machines/Actions/Player/MoveToTarget")]
public class MoveToTargetSO : StateActionSO
{
    public override StateAction CreateAction() => new MoveToTarget();
}

public class MoveToTarget : StateAction
{
    private StateMachine stateMachine;

    public MoveToTarget()
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
        PlayerCharacterSC playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();

        playerStateContainer.position = playerStateContainer.movementTarget;
        playerStateContainer.transformToPosition();
    }
}

using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "ResetTimeSinceTransition", menuName = "State Machines/Actions/Player/ResetTimeSinceTransition")]
public class ResetTimerOnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new ResetTimerOnEnter();
}

public class ResetTimerOnEnter : StateAction
{
    private PlayerCharacterSC _playerStateContainer;

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnStateEnter()
    {
        _playerStateContainer.timeSinceTransition = 0;
    }
}

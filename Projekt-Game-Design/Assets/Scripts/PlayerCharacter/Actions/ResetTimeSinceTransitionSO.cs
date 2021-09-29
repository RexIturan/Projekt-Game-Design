using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "ResetTimeSinceTransition", menuName = "State Machines/Actions/Player/ResetTimeSinceTransition")]
public class ResetTimeSinceTransitionSO : StateActionSO
{
    public override StateAction CreateAction() => new ResetTimeSinceTransition();
}

public class ResetTimeSinceTransition : StateAction
{
    private PlayerCharacterSC playerStateContainer;

    public ResetTimeSinceTransition()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnStateEnter()
    {
        playerStateContainer.timeSinceTransition = 0;
    }
}

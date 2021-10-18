using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_MovementDone", menuName = "State Machines/Conditions/Player/Movement Done")]
public class p_MovementDoneSO : StateConditionSO
{
    protected override Condition CreateCondition() => new p_MovementDone();
}

public class p_MovementDone : Condition
{
    private PlayerCharacterSC playerSC;

    public override void Awake(StateMachine stateMachine)
    {
        playerSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    protected override bool Statement()
    {
        return playerSC.movementDone;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

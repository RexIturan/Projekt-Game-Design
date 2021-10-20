using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_AttackDone", menuName = "State Machines/Conditions/Player/Attack Done")]
public class p_AttackDoneSO : StateConditionSO
{
    protected override Condition CreateCondition() => new p_AttackDone();
}

public class p_AttackDone : Condition
{
    private PlayerCharacterSC playerSC;

    public override void Awake(StateMachine stateMachine)
    {
        playerSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    protected override bool Statement()
    {
        return !playerSC.waitForAttackToFinish ||
            playerSC.timeSinceTransition >= playerSC.playerType.TIME_OF_ATTACK_ANIMATION;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

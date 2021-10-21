using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_AttackDone", menuName = "State Machines/Conditions/Player/Attack Done")]
public class P_AttackDoneSO : StateConditionSO
{
    protected override Condition CreateCondition() => new P_AttackDone();
}

public class P_AttackDone : Condition
{
    private PlayerCharacterSC _playerSC;

    public override void Awake(StateMachine stateMachine)
    {
        _playerSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    protected override bool Statement()
    {
        return !_playerSC.waitForAttackToFinish ||
            _playerSC.timeSinceTransition >= _playerSC.playerType.time_Of_Attack_Animation;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

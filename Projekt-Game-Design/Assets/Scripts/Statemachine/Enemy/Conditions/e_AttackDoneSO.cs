using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_AttackDone", menuName = "State Machines/Conditions/Enemy/Attack Done")]
public class E_AttackDoneSO : StateConditionSO
{
    protected override Condition CreateCondition() => new E_AttackDone();
}

public class E_AttackDone : Condition
{
    private EnemyCharacterSC _enemySC;

    public override void Awake(StateMachine stateMachine)
    {
        _enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    protected override bool Statement()
    {
        return !_enemySC.waitForAttackToFinish ||
            _enemySC.timeSinceTransition >= _enemySC.enemyType.time_Of_Attack_Animation;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

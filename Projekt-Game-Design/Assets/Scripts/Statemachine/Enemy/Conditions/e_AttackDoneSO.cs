using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_AttackDone", menuName = "State Machines/Conditions/Enemy/Attack Done")]
public class e_AttackDoneSO : StateConditionSO
{
    protected override Condition CreateCondition() => new e_AttackDone();
}

public class e_AttackDone : Condition
{
    private EnemyCharacterSC enemySC;

    public override void Awake(StateMachine stateMachine)
    {
        enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    protected override bool Statement()
    {
        return !enemySC.waitForAttackToFinish ||
            enemySC.timeSinceTransition >= enemySC.enemyType.TIME_OF_ATTACK_ANIMATION;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

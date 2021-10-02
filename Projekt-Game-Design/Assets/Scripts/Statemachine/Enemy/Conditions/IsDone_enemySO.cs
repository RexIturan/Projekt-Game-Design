using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsDone_enemy", menuName = "State Machines/Conditions/Enemy/Is Done")]
public class IsDone_enemySO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsDone_enemy();
}

public class IsDone_enemy : Condition
{
    private EnemyCharacterSC enemyCharacterSc;

    public override void Awake(StateMachine stateMachine)
    {
        this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    protected override bool Statement()
    {
        return this.enemyCharacterSc.isDone;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsOnTurn_enemy", menuName = "State Machines/Conditions/Enemy/Is On Turn")]
public class IsOnTurn_enemySO : StateConditionSO
{
    protected override Condition CreateCondition() => new IsOnTurn_enemy();
}

public class IsOnTurn_enemy : Condition
{
    private EnemyCharacterSC enemyCharacterSc;

    public override void Awake(StateMachine stateMachine)
    {
        this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    protected override bool Statement()
    {
        return this.enemyCharacterSc.isOnTurn;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

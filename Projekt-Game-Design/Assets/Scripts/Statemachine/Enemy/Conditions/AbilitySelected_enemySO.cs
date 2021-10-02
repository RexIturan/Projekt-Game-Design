using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilitySelected_enemy", menuName = "State Machines/Conditions/Enemy/Ability Selected")]
public class AbilitySelected_enemySO : StateConditionSO
{
    protected override Condition CreateCondition() => new AbilitySelected_enemy();
}

public class AbilitySelected_enemy : Condition
{
    private EnemyCharacterSC enemyCharacterSc;

    public override void Awake(StateMachine stateMachine)
    {
        this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    protected override bool Statement()
    {
        return this.enemyCharacterSc.abilitySelected;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

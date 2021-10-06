using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_MovementDone", menuName = "State Machines/Conditions/Enemy/Movement Done")]
public class e_MovementDoneSO  : StateConditionSO
{
    protected override Condition CreateCondition() => new e_MovementDone();
}

public class e_MovementDone : Condition
{
    private EnemyCharacterSC enemySC;

    public override void Awake(StateMachine stateMachine)
    {
        enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    protected override bool Statement()
    {
        return enemySC.movementDone;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

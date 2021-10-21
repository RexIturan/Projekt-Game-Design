using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_ResetTimer_OnEnter", menuName = "State Machines/Actions/Enemy/ResetTimeSinceTransition")]
public class E_ResetTimer_OnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new E_ResetTimer_OnEnter();
}

public class E_ResetTimer_OnEnter : StateAction
{
    private EnemyCharacterSC _enemyContainer;

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        _enemyContainer = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    public override void OnStateEnter()
    {
        _enemyContainer.timeSinceTransition = 0;
    }
}

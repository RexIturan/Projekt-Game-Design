using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_ResetTimer_OnEnter", menuName = "State Machines/Actions/Enemy/ResetTimeSinceTransition")]
public class e_ResetTimer_OnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new e_ResetTimer_OnEnter();
}

public class e_ResetTimer_OnEnter : StateAction
{
    private EnemyCharacterSC enemyContainer;

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        enemyContainer = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    public override void OnStateEnter()
    {
        enemyContainer.timeSinceTransition = 0;
    }
}

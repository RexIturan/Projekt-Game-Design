using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_SetWaitForAttack_OnEnter", menuName = "State Machines/Actions/Enemy/Set Wait For Attack")]
public class e_SetWaitForAttack_OnEnterSO : StateActionSO {
    public override StateAction CreateAction() => new e_SetWaitForAttack_OnEnter();
}

public class e_SetWaitForAttack_OnEnter : StateAction {

    protected new FinishAbilityExecution_playerSO OriginSO => (FinishAbilityExecution_playerSO)base.OriginSO;

    private EnemyCharacterSC enemyContainer;

    public override void Awake(StateMachine stateMachine) {
        enemyContainer = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    public override void OnUpdate() {
    }

    public override void OnStateEnter() {
        enemyContainer.waitForAttackToFinish = true;
    }

    public override void OnStateExit() {
    }
}

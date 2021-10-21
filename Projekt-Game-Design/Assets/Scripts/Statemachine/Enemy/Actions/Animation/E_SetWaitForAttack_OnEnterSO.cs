using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_SetWaitForAttack_OnEnter", menuName = "State Machines/Actions/Enemy/Set Wait For Attack")]
public class E_SetWaitForAttack_OnEnterSO : StateActionSO {
    public override StateAction CreateAction() => new E_SetWaitForAttack_OnEnter();
}

public class E_SetWaitForAttack_OnEnter : StateAction {

    // protected new FinishAbilityExecution_playerSO OriginSO => (FinishAbilityExecution_playerSO)base.OriginSO;

    private EnemyCharacterSC _enemyContainer;

    public override void Awake(StateMachine stateMachine) {
        _enemyContainer = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    public override void OnUpdate() {
    }

    public override void OnStateEnter() {
        _enemyContainer.waitForAttackToFinish = true;
    }

    public override void OnStateExit() {
    }
}

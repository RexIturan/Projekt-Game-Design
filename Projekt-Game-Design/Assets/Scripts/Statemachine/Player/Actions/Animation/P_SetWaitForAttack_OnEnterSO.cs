using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_SetWaitForAttack_OnEnter", menuName = "State Machines/Actions/Player/Set Wait For Attack")]
public class P_SetWaitForAttack_OnEnterSO : StateActionSO {
    public override StateAction CreateAction() => new P_SetWaitForAttack_OnEnter();
}

public class P_SetWaitForAttack_OnEnter : StateAction {

    // protected new FinishAbilityExecution_playerSO OriginSO => (FinishAbilityExecution_playerSO)base.OriginSO;

    private PlayerCharacterSC _playerStateContainer;

    public override void Awake(StateMachine stateMachine) {
        _playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate() {
    }

    public override void OnStateEnter() {
        _playerStateContainer.waitForAttackToFinish = true;
    }

    public override void OnStateExit() {
    }
}

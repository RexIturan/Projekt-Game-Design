using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_SetWaitForAttack_OnEnter", menuName = "State Machines/Actions/Player/Set Wait For Attack")]
public class p_SetWaitForAttack_OnEnterSO : StateActionSO {
    public override StateAction CreateAction() => new p_SetWaitForAttack_OnEnter();
}

public class p_SetWaitForAttack_OnEnter : StateAction {

    protected new FinishAbilityExecution_playerSO OriginSO => (FinishAbilityExecution_playerSO)base.OriginSO;

    private PlayerCharacterSC playerStateContainer;

    public override void Awake(StateMachine stateMachine) {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate() {
    }

    public override void OnStateEnter() {
        playerStateContainer.waitForAttackToFinish = true;
    }

    public override void OnStateExit() {
    }
}

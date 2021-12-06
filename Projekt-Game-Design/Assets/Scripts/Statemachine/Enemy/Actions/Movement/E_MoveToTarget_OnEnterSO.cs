using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_MoveToTarget_OnEnter", menuName = "State Machines/Actions/Enemy/e_MoveToTarget_OnEnter")]
public class E_MoveToTarget_OnEnterSO : StateActionSO {
    public override StateAction CreateAction() => new E_MoveToTarget_OnEnter();
}

public class E_MoveToTarget_OnEnter : StateAction {
    protected new E_MoveToTarget_OnEnterSO OriginSO => (E_MoveToTarget_OnEnterSO) base.OriginSO;

    private EnemyCharacterSC _enemyCharacterSC;

    public override void Awake(StateMachine stateMachine) {
        _enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
	    _enemyCharacterSC.gridPosition = _enemyCharacterSC.movementTarget.pos;
        _enemyCharacterSC.MoveToGridPosition();
    }
}
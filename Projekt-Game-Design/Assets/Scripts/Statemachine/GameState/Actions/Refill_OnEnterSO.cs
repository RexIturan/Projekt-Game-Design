using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_Refill_OnEnter", menuName = "State Machines/Actions/GameState/Refill On Enter")]
public class Refill_OnEnterSO : StateActionSO {
    [SerializeField] private TacticsGameDataSO tacticsGameData;
    public override StateAction CreateAction() => new Refill_OnEnter(tacticsGameData);
}

public class Refill_OnEnter : StateAction {
    protected new Refill_OnEnterSO OriginSO => (Refill_OnEnterSO) base.OriginSO;
    private TacticsGameDataSO tacticsGameData;

    public Refill_OnEnter(TacticsGameDataSO tacticsGameData) {
        this.tacticsGameData = tacticsGameData;
    }

    public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter() { }

    public override void OnStateExit() { }
}
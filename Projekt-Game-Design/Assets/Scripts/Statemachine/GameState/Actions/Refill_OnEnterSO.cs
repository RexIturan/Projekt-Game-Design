using Characters.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_Refill_OnEnter", menuName = "State Machines/Actions/GameState/Refill On Enter")]
public class Refill_OnEnterSO : StateActionSO {
    [SerializeField] private CharacterContainerSO characterContainer;
    [SerializeField] private TacticsGameDataSO tacticsGameData;
    public override StateAction CreateAction() => new Refill_OnEnter(tacticsGameData, characterContainer);
}

public class Refill_OnEnter : StateAction {
    protected new Refill_OnEnterSO OriginSO => (Refill_OnEnterSO) base.OriginSO;
    private TacticsGameDataSO tacticsGameData;
    [SerializeField] private CharacterContainerSO characterContainer;
    
    public Refill_OnEnter(TacticsGameDataSO tacticsGameData, CharacterContainerSO characterContainer) {
        this.tacticsGameData = tacticsGameData;
        this.characterContainer = characterContainer;
    }

    public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
        
        switch (tacticsGameData.currentPlayer) {
            case EFaction.player:
                foreach (var player in characterContainer.playerContainer) {
                    player.Refill();
                }
                break;
            case EFaction.enemy:
                foreach (var enemy in characterContainer.enemyContainer) {
                    enemy.Refill();
                }
                break;
            default:
                break;
        }
    }

    public override void OnStateExit() { }
}
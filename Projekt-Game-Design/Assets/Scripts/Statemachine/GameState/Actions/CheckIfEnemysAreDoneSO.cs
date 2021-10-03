using Characters.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "CheckIfEnemysAreDone", menuName = "State Machines/Actions/GameState/Check If Enemys Are Done")]
public class CheckIfEnemysAreDoneSO : StateActionSO {
    [SerializeField] private CharacterContainerSO characterContainer;
    [SerializeField] private EFactionEventChannelSO endTurnEC;
    public override StateAction CreateAction() => new CheckIfEnemysAreDone(characterContainer, endTurnEC);
}

public class CheckIfEnemysAreDone : StateAction {
    protected new CheckIfEnemysAreDoneSO OriginSO => (CheckIfEnemysAreDoneSO) base.OriginSO;
    private readonly CharacterContainerSO characterContainer;
    private readonly EFactionEventChannelSO endTurnEC;

    public CheckIfEnemysAreDone(CharacterContainerSO characterContainer, EFactionEventChannelSO endTurnEC) {
        this.characterContainer = characterContainer;
        this.endTurnEC = endTurnEC;
    }

    public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() {
        var done = true;
        foreach (var enemy in characterContainer.enemyContainer) {
            if (!enemy.isDone) {
                done = false;
            }
        }

        if (done) {
            endTurnEC.RaiseEvent(EFaction.enemy);
        }
    }

    public override void OnStateEnter() { }

    public override void OnStateExit() {
        // foreach (var enemy in characterContainer.enemyContainer) {
        //     enemy.isOnTurn = false;
        // }
    }
}
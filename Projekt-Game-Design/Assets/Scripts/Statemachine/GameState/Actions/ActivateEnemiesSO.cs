using Characters.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_ActivateEnemies_OnEnter", menuName = "State Machines/Actions/GameState/Activate Enemies OnEnter")]
public class ActivateEnemiesSO : StateActionSO {
    [SerializeField] private CharacterContainerSO characterContainer;
    public override StateAction CreateAction() => new ActivateEnemies_OnEnter(characterContainer);
}

public class ActivateEnemies_OnEnter : StateAction {
    protected new ActivateEnemiesSO OriginSO => (ActivateEnemiesSO) base.OriginSO;
    private CharacterContainerSO characterContainer;

    public ActivateEnemies_OnEnter(CharacterContainerSO characterContainer) {
        this.characterContainer = characterContainer;
    }

    public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
        Debug.Log("Activating Enemies. ");
        foreach (var enemy in characterContainer.enemyContainer) {
            enemy.isOnTurn = true;
        }
    }

    public override void OnStateExit() { }
}
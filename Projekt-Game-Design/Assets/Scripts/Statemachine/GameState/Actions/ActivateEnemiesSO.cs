using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_ActivateEnemies_OnEnter",
    menuName = "State Machines/Actions/GameState/Activate Enemies OnEnter")]
public class ActivateEnemiesSO : StateActionSO {
    public override StateAction CreateAction() => new ActivateEnemies_OnEnter();
}

public class ActivateEnemies_OnEnter : StateAction {
    protected new ActivateEnemiesSO OriginSO => (ActivateEnemiesSO)base.OriginSO;

    public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
        Debug.Log("Activating Enemies. ");
        var characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
        foreach (var enemy in characterList.enemyContainer) {
            enemy.GetComponent<EnemyCharacterSC>().isOnTurn = true;
        }
    }

    public override void OnStateExit() { }
}

﻿using Characters;
using GameManager;
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
    private readonly TacticsGameDataSO _tacticsGameData;
    private GameSC _gameSC;
    
    public Refill_OnEnter(TacticsGameDataSO tacticsGameData) {
        this._tacticsGameData = tacticsGameData;
    }

    public override void Awake(StateMachine stateMachine) {
	    _gameSC = stateMachine.GetComponent<GameSC>();
    }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
        var characterList = Object.FindObjectOfType<CharacterList>();
        switch (_tacticsGameData.currentPlayer) { 
            case Faction.Player:
                foreach (var player in characterList.playerContainer) {
                    player.GetComponent<Statistics>().RefillEnergy();
                }
                _gameSC.UpdateOverlay(Faction.Player);
                break;
            case Faction.Enemy:
                foreach (var enemy in characterList.enemyContainer) {
                    enemy.GetComponent<Statistics>().RefillEnergy();
                }
                break;
        }
    }

    public override void OnStateExit() { }
}

using Characters;
using Characters.Types;
using GameManager;
using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ReduceCooldown", menuName = "State Machines/Actions/Reduce Cooldown")]
public class ReduceCooldown_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new ReduceCooldown_OnEnter();
}

public class ReduceCooldown_OnEnter : StateAction {
	// protected new ReduceCooldown_OnEnterSO OriginOnEnterSO => (ReduceCooldown_OnEnterSO)base.OriginSO;

	private GameSC _gameSC;
	
	public override void Awake(StateMachine stateMachine){
		_gameSC = stateMachine.GetComponent<GameSC>();
	}
	
	public override void OnUpdate() {}
	
	public override void OnStateEnter() {
		var characterList = Object.FindObjectOfType<CharacterList>();
		switch (_gameSC.CurrentPlayer) { 
			case Faction.Player:
				foreach (var player in characterList.playerContainer) {
					player.GetComponent<AbilityController>().ReduceCooldowns();
				}
				_gameSC.UpdateOverlay(Faction.Player);
				break;
			case Faction.Enemy:
				foreach (var enemy in characterList.enemyContainer) {
					enemy.GetComponent<AbilityController>().ReduceCooldowns();
				}
				break;
		}
	}
	
	public override void OnStateExit() {}
}

using Characters;
using Characters.Types;
using Events.ScriptableObjects.GameState;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "CheckIfEnemysAreDone",
	menuName = "State Machines/Actions/GameState/Check If Enemys Are Done")]
public class CheckIfEnemysAreDoneSO : StateActionSO {
	[SerializeField] private EFactionEventChannelSO endTurnEC;
	public override StateAction CreateAction() => new CheckIfEnemysAreDone(endTurnEC);
}

public class CheckIfEnemysAreDone : StateAction {
	protected new CheckIfEnemysAreDoneSO OriginSO => ( CheckIfEnemysAreDoneSO )base.OriginSO;
	private readonly EFactionEventChannelSO _endTurnEC;

	public CheckIfEnemysAreDone(EFactionEventChannelSO endTurnEC) {
		_endTurnEC = endTurnEC;
	}

	public override void Awake(StateMachine stateMachine) { }

	public override void OnUpdate() {
		var characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
		var done = true;
		foreach ( var enemy in characterList.enemyContainer ) {
			if ( !enemy.GetComponent<EnemyCharacterSC>().isDone ) {
				done = false;
			}
		}

		if ( done ) {
			_endTurnEC.RaiseEvent(Faction.Enemy);
		}
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}
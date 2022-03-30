using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Types;
using Events.ScriptableObjects.GameState;
using GDP01._Gameplay.Provider;
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
		var done = true;

		done = GameplayProvider.Current.CharacterManager.GetEnemyCahracters().All(enemy => enemy.isDone);

		if ( done ) {
			_endTurnEC.RaiseEvent(Faction.Enemy);
		}
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}
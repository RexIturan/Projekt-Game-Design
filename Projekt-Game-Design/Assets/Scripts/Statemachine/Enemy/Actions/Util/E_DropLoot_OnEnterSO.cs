using Characters;
using GDP01._Gameplay.Provider;
using GDP01.Loot.ScriptableObjects;
using Grid;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using WorldObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_DropLoot_OnEnter",
	menuName = "State Machines/Actions/Enemy/Drop Loot On Enter")]
public class E_DropLoot_OnEnterSO : StateActionSO {
	[SerializeField] private VoidEventChannelSO redrawLevelEC;

	public override StateAction CreateAction() => new E_DropLoot_OnEnter(redrawLevelEC);
}

public class E_DropLoot_OnEnter : StateAction {
	private VoidEventChannelSO _redrawLevelEC;

	private GridController _gridController;
	private EnemyCharacterSC _enemy;
	private GridTransform _gridTransform;

	public override void OnUpdate() { }

	public E_DropLoot_OnEnter(VoidEventChannelSO redrawLevelEC) {
		_redrawLevelEC = redrawLevelEC;
	}

	public override void Awake(StateMachine stateMachine) {
		_enemy = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
		_gridController = GridController.FindGridController();
	}

	public override void OnStateEnter() {
		WorldObjectManager.DropLoot(_enemy.LootTable, _gridTransform.gridPosition);
	}
}
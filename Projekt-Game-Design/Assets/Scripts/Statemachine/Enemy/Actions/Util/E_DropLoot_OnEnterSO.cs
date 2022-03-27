using Characters;
using GDP01._Gameplay.Provider;
using GDP01.Loot.ScriptableObjects;
using Grid;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
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
		DropLoot(_enemy.LootTable, _gridTransform.gridPosition);
	}

	// TODO move to Item Spawner
	public static void DropLoot(LootTableSO lootTable,
		Vector3Int gridPos) {
		var worldObjectManager = GameplayProvider.Current.WorldObjectManager;

		if ( !worldObjectManager )
			Debug.LogWarning("Could not find worldObjectManager. ");
		else {
			ItemTypeSO itemType = null;
			int dropID = -1;
			var lootDrop = lootTable.GetLootDrop();

			//todo drop multiple items 
			if ( lootDrop.items.Count > 0 ) {
				dropID = lootDrop.items[0].id;
				itemType = lootDrop.items[0];
			}

			if ( dropID >= 0 && itemType is {} ) {
				//todo move reference to grid controller to ItemSpawner or so and just invoke a event here 
				Debug.Log("Dropping loot: " + dropID + " at " + gridPos.x + ", " + gridPos.z);
				
				worldObjectManager.AddItemAt(itemType, gridPos);
			}
		}
	}
}
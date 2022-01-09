using Characters;
using Grid;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_DropLoot_OnEnter", menuName = "State Machines/Actions/Enemy/Drop Loot On Enter")]
public class E_DropLoot_OnEnterSO : StateActionSO
{
		[SerializeField] private VoidEventChannelSO redrawLevelEC;

		public override StateAction CreateAction() => new E_DropLoot_OnEnter(redrawLevelEC);
}

public class E_DropLoot_OnEnter : StateAction
{
		private VoidEventChannelSO _redrawLevelEC;

		private GridController _gridController;
		private EnemyCharacterSC _enemy;
		private GridTransform _gridTransform;

		public override void OnUpdate() { }

		public E_DropLoot_OnEnter(VoidEventChannelSO redrawLevelEC)
		{
				_redrawLevelEC = redrawLevelEC;
		}

		public override void Awake(StateMachine stateMachine)
		{
				_enemy = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
				_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
				_gridController = GridController.FindGridController();
		}

		public override void OnStateEnter()
		{
				DropLoot(_redrawLevelEC, _enemy.enemyType.drops, _gridTransform.gridPosition);
		}

		// maybe TODO maybe move somewhere else
		public static void DropLoot(VoidEventChannelSO redrawLevelEC, LootTable drops, Vector3Int gridPos)
		{
				GridController _gridController = GridController.FindGridController();

				if ( !_gridController )
						Debug.LogWarning("Could not find grid controller. ");
				else
				{
						// generate random drop
						int dropID = -1;
						foreach ( var possibleDrop in drops.itemDropList )
						{
								if ( dropID == -1 && possibleDrop.probability >= Random.value )
										dropID = possibleDrop.itemID;
						}

						// if player was lucky, drop it
						if ( dropID >= 0 )
						{
								Debug.Log("Dropping loot: " + dropID + " at " + gridPos.x + ", " + gridPos.z);
								_gridController.AddItemAtGridPos(gridPos, dropID);
								redrawLevelEC.RaiseEvent();
						}
				}
		}
}
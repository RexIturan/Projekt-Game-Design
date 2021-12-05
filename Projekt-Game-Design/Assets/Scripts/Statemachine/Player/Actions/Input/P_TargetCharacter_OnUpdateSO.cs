using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using UnityEngine.InputSystem;
using Grid;
using Util;
using Ability;
using Ability.ScriptableObjects;
using Characters;
using Characters.Ability;
using Combat;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "P_TargetCharacter_OnUpdate",
	menuName = "State Machines/Actions/Player/Target Character On Update")]
public class P_TargetCharacter_OnUpdateSO : StateActionSO {
	[Header("SO Data")] [SerializeField] private GridDataSO globalGridData;
	[SerializeField] private AbilityContainerSO abilityContainer;

	public override StateAction CreateAction() =>
		new P_TargetCharacter_OnUpdate(globalGridData, abilityContainer);
}

public class P_TargetCharacter_OnUpdate : StateAction {
	private const float TimeBeforeAcceptingInput = 0.5f;
	private const bool Diagonal = false;

	private Timer _timer;
	private Attacker _attacker;
	private AbilityController _abilityController;
	private Targetable _thisTargatable;

	private readonly GridDataSO _globalGridData;
	private readonly AbilityContainerSO _abilityContainer;

	private AbilityTarget _targets;

	// private TileProperties _conditions;
	private int _range;

	public P_TargetCharacter_OnUpdate(GridDataSO globalGridData,
		AbilityContainerSO abilityContainer) {
		this._globalGridData = globalGridData;
		this._abilityContainer = abilityContainer;
	}

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_timer = stateMachine.gameObject.GetComponent<Timer>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();

		_targets = _abilityContainer.abilities[_abilityController.SelectedAbilityID].targets;
		// _conditions = _abilityContainer.abilities[_playerStateContainer.AbilityID].conditions;
		_range = _abilityContainer.abilities[_abilityController.SelectedAbilityID].range;
	}

	public override void OnUpdate() {
		var characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
		if ( _timer.timeSinceTransition > TimeBeforeAcceptingInput &&
		     Mouse.current.leftButton.wasPressedThisFrame ) {
			var pos = MousePosition.GetTilePositionFromMousePosition(_globalGridData, true, out bool hitBottom);
			//todo unnoetig
			Vector3Int mouseGridPos = _globalGridData.GetGridPos3DFromWorldPos(pos);

			// Debug.Log("TargetCharacter: Mouse click position: " + mousePos.x + ", " + mousePos.y);

			// was a player clicked?
			if ( ( _targets & AbilityTarget.Ally ) != 0 ) {
				foreach ( var player in characterList.playerContainer ) {
					// Debug.Log("Player position: " + player.gridPosition.x + ", " + player.gridPosition.y + ", " + player.gridPosition.z);

					var pcTargetable = player.GetComponent<Targetable>();
					if ( !pcTargetable.Equals(_thisTargatable) &&
					     pcTargetable.GetGridPosition().Equals(mouseGridPos) &&
					     _range >= CalcDistance(_thisTargatable.GetGridPosition(),
						     pcTargetable.GetGridPosition(), Diagonal) ) {
						Debug.Log("Ally targeted. ");
						// Confirm ability and set target
						_attacker.playerTarget = pcTargetable;
						_abilityController.abilityConfirmed = true;
					}
				}
			}

			// was an enemy clicked?
			//
			if ( ( _targets & AbilityTarget.Enemy ) != 0 ) {
				foreach ( var enemy in characterList.enemyContainer ) {
					// Debug.Log("Enemy position: " + enemy.gridPosition.x + ", " + enemy.gridPosition.y + ", " + enemy.gridPosition.z);

					var ecTargetable = enemy.GetComponent<Targetable>();
					if ( ecTargetable.GetGridPosition().Equals(mouseGridPos) &&
					     _range >= CalcDistance(_thisTargatable.GetGridPosition(),
						     ecTargetable.GetGridPosition(), Diagonal) ) {
						Debug.Log("Enemy targeted. ");
						// Confirm ability and set target
						_attacker.enemyTarget = ecTargetable;
						_abilityController.abilityConfirmed = true;
					}
				}
			}

			// was self clicked?
			//            
			if ( ( _targets & AbilityTarget.Self ) != 0 ) {
				if( _thisTargatable.GetGridPosition().Equals(mouseGridPos) ) {
					Debug.Log("Self targeted. ");
					// Confirm ability and set target
					_attacker.playerTarget = _thisTargatable;
					_abilityController.abilityConfirmed = true;
				}
			}
		}
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }

	// TODO: Use util method instead
	// Calculates the distance
	private int CalcDistance(Vector3Int start, Vector3Int end, bool diagonal) {
		if ( diagonal ) {
			return Mathf.Max(Mathf.Abs(end.x - start.x), Mathf.Abs(end.z - start.z));
		}
		else
			return Mathf.Abs(end.x - start.x) + Mathf.Abs(end.z - start.z);
	}

	// TODO(): Codeverdopplung vermeiden (copy paste aus PathfindingController) 
	public Vector2Int WorldPosToGridPos(Vector3 worldPos) {
		var lowerBounds = Vector3Int.FloorToInt(_globalGridData.OriginPosition);
		var flooredPos = Vector3Int.FloorToInt(worldPos);
		return new Vector2Int(
			x: flooredPos.x + Mathf.Abs(lowerBounds.x),
			y: flooredPos.z + Mathf.Abs(lowerBounds.z));
	}
}
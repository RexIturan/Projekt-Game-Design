using Ability.ScriptableObjects;
using System.Collections.Generic;
using Characters;
using Characters.Ability;
using Combat;
using Events.ScriptableObjects.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "P_SaveTilesInRange_OnEnter",
	menuName = "State Machines/Actions/Player/Save Tiles In Range On Enter")]
public class P_SaveTilesInRange_OnEnterSO : StateActionSO {
	[SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
	[SerializeField] private AbilityContainerSO abilityContainer;

	public override StateAction CreateAction() =>
		new P_SaveTilesInRange_OnEnter(pathfindingQueryEvent, abilityContainer);
}

public class P_SaveTilesInRange_OnEnter : StateAction {
	private const int CostsPerTile = 20;

	private Attacker _attacker;
	private AbilityController _abilityController;
	private GridTransform _gridTransform;

	private readonly PathfindingQueryEventChannelSO _pathfindingQueryEvent;
	private readonly AbilityContainerSO _abilityContainer;

	public P_SaveTilesInRange_OnEnter(PathfindingQueryEventChannelSO pathfindingQueryEvent,
		AbilityContainerSO abilityContainer) {
		this._pathfindingQueryEvent = pathfindingQueryEvent;
		this._abilityContainer = abilityContainer;
	}

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnStateEnter() {
		_pathfindingQueryEvent.RaiseEvent(_gridTransform.gridPosition,
			( _abilityContainer.abilities[_abilityController.SelectedAbilityID].range ) * CostsPerTile,
			SaveToStateContainer);
	}

	public void SaveToStateContainer(List<PathNode> tilesInRange) {
		_attacker.tilesInRange = tilesInRange;
	}
}
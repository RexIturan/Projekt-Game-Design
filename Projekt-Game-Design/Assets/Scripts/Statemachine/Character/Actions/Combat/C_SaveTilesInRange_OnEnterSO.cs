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
using Events.ScriptableObjects.FieldOfView;
using Level.Grid;
using FieldOfView;

[CreateAssetMenu(fileName = "c_SaveTilesInRange_OnEnter",
	menuName = "State Machines/Actions/Character/Save Tiles In Range On Enter")]
public class C_SaveTilesInRange_OnEnterSO : StateActionSO {
	[SerializeField] private FOVQueryEventChannelSO fieldOfViewQueryEvent;
	[SerializeField] private AbilityContainerSO abilityContainer;

	public override StateAction CreateAction() =>
		new C_SaveTilesInRange_OnEnter(fieldOfViewQueryEvent, abilityContainer);
}

public class C_SaveTilesInRange_OnEnter : StateAction {
	private const int CostsPerTile = 20;

	private Statistics statistics;
	private Attacker _attacker;
	private AbilityController _abilityController;
	private GridTransform _gridTransform;

	private readonly FOVQueryEventChannelSO fieldOfViewQueryEvent;
	private readonly AbilityContainerSO _abilityContainer;

	public C_SaveTilesInRange_OnEnter(FOVQueryEventChannelSO fieldOfViewQueryEvent, AbilityContainerSO abilityContainer) {
		this.fieldOfViewQueryEvent = fieldOfViewQueryEvent;
		this._abilityContainer = abilityContainer;
	}

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		statistics = stateMachine.gameObject.GetComponent<Statistics>();
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnStateEnter() {
		//todo redo
		int index = 1;
		if ( _abilityController.SelectedAbilityID >= 0 ) {
			index = _abilityController.SelectedAbilityID;
		}
		fieldOfViewQueryEvent.RaiseEvent(_gridTransform.gridPosition,
		( _abilityContainer.abilities[index].range ),
		TileProperties.ShootTrough,
		SaveToStateContainer);
		
		fieldOfViewQueryEvent.RaiseEvent(_gridTransform.gridPosition,
			statistics.StatusValues.ViewDistance.value,
			TileProperties.Opaque | TileProperties.Solid,
			SaveVisibleTilesToStateContainer);
	}

	public void SaveToStateContainer(bool[,] tilesInRange) {
		_attacker.tilesInRange = FieldOfViewController.VisibleTilesToPathNodeList(tilesInRange);
	}
	
	public void SaveVisibleTilesToStateContainer(bool[,] visibleTiles) {
		_attacker.visibleTiles = FieldOfViewController.VisibleTilesToPathNodeList(visibleTiles);
	}
}
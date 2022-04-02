using Ability.ScriptableObjects;
using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Events.ScriptableObjects.FieldOfView;
using Level.Grid;
using FieldOfView;
using GDP01.Characters.Component;
using GDP01.World.Components;
using Util;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "c_SaveTilesInRange_OnEnter",
	menuName = "State Machines/Actions/Character/Save Tiles In Range On Enter")]
public class C_SaveTilesInRange_OnEnterSO : StateActionSO {
	[SerializeField] private FOVQueryEventChannelSO fieldOfViewQueryEvent;
	[SerializeField] private FOVCrossQueryEventChannelSO fieldOfViewCrossQueryEvent;
	[SerializeField] private AbilityContainerSO abilityContainer;

	public override StateAction CreateAction() =>
		new C_SaveTilesInRange_OnEnter(fieldOfViewQueryEvent, fieldOfViewCrossQueryEvent, abilityContainer);
}

public class C_SaveTilesInRange_OnEnter : StateAction {
	private const int CostsPerTile = 20;

	private Statistics statistics;
	private Attacker _attacker;
	private AbilityController _abilityController;
	private GridTransform _gridTransform;

	private readonly FOVQueryEventChannelSO _fieldOfViewQueryEvent;
	private readonly FOVCrossQueryEventChannelSO _fieldOfViewCrossQueryEvent;
	private readonly AbilityContainerSO _abilityContainer;

	public C_SaveTilesInRange_OnEnter(FOVQueryEventChannelSO fieldOfViewQueryEvent, 
			FOVCrossQueryEventChannelSO fieldOfViewCrossQueryEvent, 
			AbilityContainerSO abilityContainer) {
		_fieldOfViewQueryEvent = fieldOfViewQueryEvent;
		_fieldOfViewCrossQueryEvent = fieldOfViewCrossQueryEvent;
		_abilityContainer = abilityContainer;
	}

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		statistics = stateMachine.gameObject.GetComponent<Statistics>();
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnStateEnter() {
		AbilitySO ability = _abilityController.SelectedAbility;
		
		if(ability) { 
			if(ability.targets == 0)
					_attacker.tilesInRange = new List<PathNode>();
			else if(ability.targetableTilesAreCross)
				_fieldOfViewCrossQueryEvent.RaiseEvent(_gridTransform.gridPosition, 
					TileProperties.ShootTrough, SaveToStateContainer);
			else
				_fieldOfViewQueryEvent.RaiseEvent(_gridTransform.gridPosition,
					( ability.range ),
					TileProperties.ShootTrough,
					SaveToStateContainer);
		}
		
		_fieldOfViewQueryEvent.RaiseEvent(_gridTransform.gridPosition,
			statistics.StatusValues.ViewDistance.Value,
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
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
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnStateEnter() {
			fieldOfViewQueryEvent.RaiseEvent(_gridTransform.gridPosition,
			( _abilityContainer.abilities[_abilityController.SelectedAbilityID].range ),
			TileProperties.ShootTrough,
			SaveToStateContainer);
	}

	public void SaveToStateContainer(bool[,] tilesInRange) {
		_attacker.tilesInRange = VisibleTilesToPathNodeList(tilesInRange);
	}

	// TODO: move to somewhere else I guess?
	// TODO: add third dimension to visibility and also this function
	public static List<PathNode> VisibleTilesToPathNodeList(bool[,] visibleTiles) {
		List<PathNode> tilesInRange = new List<PathNode>();
		for(int x = 0; x < visibleTiles.GetLength(0); x++)
		{
			for(int y = 0; y < visibleTiles.GetLength(1); y++)
			{
				if(visibleTiles[x,y])
		  	{
					tilesInRange.Add(new PathNode(x, 1, y));
				}
			}
		}
		return tilesInRange;
	}
}
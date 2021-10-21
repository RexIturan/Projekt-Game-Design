using Ability.ScriptableObjects;
using System.Collections.Generic;
using Events.ScriptableObjects.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "P_SaveTilesInRange_OnEnter", menuName = "State Machines/Actions/Player/Save Tiles In Range On Enter")]
public class P_SaveTilesInRange_OnEnterSO : StateActionSO
{
    [SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
    [SerializeField] private AbilityContainerSO abilityContainer;

    public override StateAction CreateAction() => new P_SaveTilesInRange_OnEnter(pathfindingQueryEvent, abilityContainer);
}

public class P_SaveTilesInRange_OnEnter : StateAction
{
    private const int CostsPerTile = 20;

    private PlayerCharacterSC _playerStateContainer;

    private PathfindingQueryEventChannelSO _pathfindingQueryEvent;
    private AbilityContainerSO _abilityContainer;

    public P_SaveTilesInRange_OnEnter(PathfindingQueryEventChannelSO pathfindingQueryEvent, AbilityContainerSO abilityContainer)
    {
        this._pathfindingQueryEvent = pathfindingQueryEvent;
        this._abilityContainer = abilityContainer;
    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnStateEnter()
    {
        _pathfindingQueryEvent.RaiseEvent(_playerStateContainer.gridPosition, 
                                         (_abilityContainer.abilities[_playerStateContainer.AbilityID].range) * CostsPerTile, 
                                         SaveToStateContainer);
    }

    public void SaveToStateContainer(List<PathNode> tilesInRange)
    {
        _playerStateContainer.tilesInRange = tilesInRange;
    }
}

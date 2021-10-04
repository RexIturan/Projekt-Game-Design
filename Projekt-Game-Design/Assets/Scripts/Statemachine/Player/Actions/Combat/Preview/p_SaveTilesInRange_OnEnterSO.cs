using Ability.ScriptableObjects;
using Events.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_SaveTilesInRange_OnEnter", menuName = "State Machines/Actions/Player/Save Tiles In Range On Enter")]
public class p_SaveTilesInRange_OnEnterSO : StateActionSO
{
    [SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
    [SerializeField] private AbilityContainerSO abilityContainer;

    public override StateAction CreateAction() => new p_SaveTilesInRange_OnEnter(pathfindingQueryEvent, abilityContainer);
}

public class p_SaveTilesInRange_OnEnter : StateAction
{
    private const int COSTS_PER_TILE = 10;

    private PlayerCharacterSC playerStateContainer;

    private PathfindingQueryEventChannelSO pathfindingQueryEvent;
    private AbilityContainerSO abilityContainer;

    public p_SaveTilesInRange_OnEnter(PathfindingQueryEventChannelSO pathfindingQueryEvent, AbilityContainerSO abilityContainer)
    {
        this.pathfindingQueryEvent = pathfindingQueryEvent;
        this.abilityContainer = abilityContainer;
    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnStateEnter()
    {
        pathfindingQueryEvent.RaiseEvent(playerStateContainer.gridPosition, 
                                         (abilityContainer.abilities[playerStateContainer.AbilityID].range + 1) * COSTS_PER_TILE, 
                                         saveToStateContainer);
    }

    public void saveToStateContainer(List<PathNode> tilesInRange)
    {
        playerStateContainer.tilesInRange = tilesInRange;
    }
}

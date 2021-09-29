using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using UnityEngine.InputSystem;
using Grid;
using Pathfinding;
using Util;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ExecuteMovement", menuName = "State Machines/Actions/Player/ExecuteMovement")]
public class ExecuteMovementSO : StateActionSO
{
    public override StateAction CreateAction() => new ExecuteMovement();
}

public class ExecuteMovement : StateAction
{
    private PlayerCharacterSC playerStateContainer;

    public ExecuteMovement()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateEnter()
    {
        // TODO: action "MoveToTarget" is useless here
        Debug.Log("Bewegung!");

        playerStateContainer.position = new Vector3Int(playerStateContainer.movementTarget.x, 
                                                       1, 
                                                       playerStateContainer.movementTarget.y);
        playerStateContainer.energy -= Mathf.CeilToInt((float) playerStateContainer.movementTarget.dist / playerStateContainer.movementPointsPerEnergy);

        playerStateContainer.movementTarget = default;
        playerStateContainer.transformToPosition();

        playerStateContainer.abilityConfirmed = false;
        playerStateContainer.abilityExecuted = true;
        playerStateContainer.isSelected = true;

    }

    public override void OnStateExit()
    {

    }
}

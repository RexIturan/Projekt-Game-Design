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
    private PlayerCharacterSC playerCharacterSC;

    public ExecuteMovement()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateEnter()
    {
        // TODO: action "MoveToTarget" is useless here
        Debug.Log("Bewegung!");

        playerCharacterSC.position = new Vector3Int(playerCharacterSC.movementTarget.x, 
                                                       1, 
                                                       playerCharacterSC.movementTarget.y);
        
        playerCharacterSC.transformToPosition();
        playerCharacterSC.abilityExecuted = true;
    }

    public override void OnStateExit()
    {

    }
}

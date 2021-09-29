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
    private const int STANDARD_Y_VALUE = 1;

    private PlayerCharacterSC playerCharacterSc;

    public override void Awake(StateMachine stateMachine)
    {
        playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateEnter()
    {
        // TODO: action "MoveToTarget" is useless here
        Debug.Log("Bewegung!");

        playerCharacterSc.position = new Vector3Int(playerCharacterSc.movementTarget.x, 
                                                       STANDARD_Y_VALUE, 
                                                       playerCharacterSc.movementTarget.y);
        playerCharacterSc.transformToPosition();
    }

    public override void OnStateExit()
    {

    }
}

using Events.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_MakeMoveOnEnter", menuName = "State Machines/Actions/Enemy/e_MakeMoveOnEnter")]
public class e_MakeMoveOnEnterSO : StateActionSO
{
    [Header("Sending events on")]
    [SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

    public override StateAction CreateAction() => new e_MakeMoveOnEnter(pathfindingPathQueryEventChannel);
}

public class e_MakeMoveOnEnter : StateAction
{
    private EnemyCharacterSC enemySC;
    private EnemyBehaviorSO behavior;

    private GameObject targetPlayer;
    private PathNode closesTileToPlayer;

    private bool canMove;

    private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

    public e_MakeMoveOnEnter(PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel)
    {
        this.pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        behavior = enemySC.behavior;
    }

    public override void OnStateEnter()
    {
        if (behavior.alwaysSkip)
            Skip();
        else
        {
            canMove = true;

            // TODO: find nearest player instead
            targetPlayer = enemySC.characterContainer.playerContainer[0].gameObject;
            PlayerCharacterSC targetContainer = targetPlayer.GetComponent<PlayerCharacterSC>();

            Vector3Int startNode = new Vector3Int(enemySC.position.x,
                                                  enemySC.position.z,
                                                  0);
            Vector3Int endNode = new Vector3Int(targetContainer.position.x,
                                                targetContainer.position.z,
                                                0);

            pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, SaveClosestToPlayer);

            if (canMove)
            {
                enemySC.movementTarget = closesTileToPlayer;
                enemySC.abilityID = 0;
                enemySC.abilitySelected = true;
            }
            else
                Skip();
        }
    }

    private void Skip()
    {
        enemySC.isDone = true;
    }

    private void SaveClosestToPlayer(List<PathNode> path)
    {
        int index = 0;
        for(int i = 1; i < path.Count; i++)
        {
            // TODO: distance instead of GCost? 
            if (path[i].gCost <= enemySC.movementPointsPerEnergy * enemySC.energy)
                index = i;
            else
                break;
        }

        if (index == 0)
        {
            Debug.Log("Gegner kann sich nicht bewegen");
            closesTileToPlayer = null;
            canMove = false;
        }
        else
        {
            path[index].dist = path[index].gCost;
            closesTileToPlayer = path[index];
        }
    }
}

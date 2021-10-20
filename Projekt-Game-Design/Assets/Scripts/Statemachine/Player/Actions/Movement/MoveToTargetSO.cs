using Events.ScriptableObjects;
using System.Collections.Generic;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "MoveToTarget", menuName = "State Machines/Actions/Player/MoveToTarget")]
public class MoveToTargetSO : StateActionSO
{
    [SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

    public override StateAction CreateAction() => new MoveToTarget(pathfindingPathQueryEventChannel);
}

public class MoveToTarget : StateAction
{
    private const float TIME_PER_STEP = 0.5f;

    protected new MoveToTargetSO OriginSO => (MoveToTargetSO)base.OriginSO;
    
    private PlayerCharacterSC playerCharacterSC;
    private float timeSinceLastStep = 0;
    private List<PathNode> path;
    private int currentStep;
    private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

    public MoveToTarget(PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel)
    {
        this.pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel; 
    }

    public override void Awake(StateMachine stateMachine)
    {
        playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }
    
    public override void OnUpdate()
    {
        if(currentStep >= path.Count)
            playerCharacterSC.movementDone = true;

        if (!playerCharacterSC.movementDone)
        {
            timeSinceLastStep += Time.deltaTime;
            if (timeSinceLastStep >= TIME_PER_STEP)
            {
                timeSinceLastStep -= TIME_PER_STEP;

                playerCharacterSC.gridPosition = new Vector3Int(path[currentStep].x,
                                                       1,
                                                       path[currentStep].y);
                playerCharacterSC.TransformToPosition();
                
                currentStep++;
            }
        }
    }

    public override void OnStateEnter()
    {

        Vector3Int startNode = new Vector3Int(playerCharacterSC.gridPosition.x,
                                            playerCharacterSC.gridPosition.z,
                                            0);
        Vector3Int endNode = new Vector3Int(playerCharacterSC.movementTarget.x,
                                            playerCharacterSC.movementTarget.y,
                                            0);

        pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, savePath);

        timeSinceLastStep = 0;
        currentStep = 1;
        playerCharacterSC.movementDone = false;
    }

    private void savePath(List<PathNode> path)
    {
        this.path = path;
    }
}

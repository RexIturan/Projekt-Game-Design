using Characters.ScriptableObjects;
using Events.ScriptableObjects;
using System.Collections.Generic;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_MoveToTarget_OnUpdate", menuName = "State Machines/Actions/Enemy/Move To Target On Update")]
public class e_MoveToTarget_OnUpdateSO : StateActionSO
{
    [SerializeField] private CharacterContainerSO characterContainer;
    [SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

    public override StateAction CreateAction() => new e_MoveToTarget_OnUpdate(characterContainer, pathfindingPathQueryEventChannel);
}

public class e_MoveToTarget_OnUpdate : StateAction
{
    private const float TIME_PER_STEP = 0.2f;

    protected new MoveToTargetSO OriginSO => (MoveToTargetSO)base.OriginSO;

    private EnemyCharacterSC enemyCharacterSC;
    private float timeSinceLastStep = 0;
    private List<PathNode> path;
    private int currentStep;
    [SerializeField] private CharacterContainerSO characterContainer;
    private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

    public e_MoveToTarget_OnUpdate(CharacterContainerSO characterContainer, PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel)
    {
        this.pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
        this.characterContainer = characterContainer;
    }

    public override void Awake(StateMachine stateMachine)
    {
        enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    public override void OnUpdate()
    {
        if (currentStep >= path.Count || playerOnField())
        {
            enemyCharacterSC.movementDone = true;
            enemyCharacterSC.abilityExecuted = true;
            enemyCharacterSC.isDone = true;
        }

            if (!enemyCharacterSC.movementDone)
        {
            timeSinceLastStep += Time.deltaTime;
            if (timeSinceLastStep >= TIME_PER_STEP)
            {
                timeSinceLastStep -= TIME_PER_STEP;

                enemyCharacterSC.gridPosition = new Vector3Int(path[currentStep].x,
                                                       1,
                                                       path[currentStep].y);
                enemyCharacterSC.transformToPosition();

                currentStep++;
            }
        }
    }

    public override void OnStateEnter()
    {

        Vector3Int startNode = new Vector3Int(enemyCharacterSC.gridPosition.x,
                                            enemyCharacterSC.gridPosition.z,
                                            0);
        Vector3Int endNode = new Vector3Int(enemyCharacterSC.movementTarget.x,
                                            enemyCharacterSC.movementTarget.y,
                                            0);

        pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, savePath);

        timeSinceLastStep = 0;
        currentStep = 1;
        enemyCharacterSC.movementDone = false;
    }

    private void savePath(List<PathNode> path)
    {
        this.path = path;
    }

    private bool playerOnField()
    {
        bool fieldOccupied = false;
            
        Vector3Int stepPosition = new Vector3Int(path[currentStep].x,
                                                 1,
                                                 path[currentStep].y);
        foreach (PlayerCharacterSC player in characterContainer.playerContainer)
        {
            if (player.gridPosition.Equals(stepPosition))
                fieldOccupied = true;
        }

        return fieldOccupied;
    }
}

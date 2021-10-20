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
    private const float TimePerStep = 0.2f;

    protected new MoveToTargetSO OriginSO => (MoveToTargetSO)base.OriginSO;
    
    private PlayerCharacterSC _playerCharacterSC;
    private float _timeSinceLastStep;
    private List<PathNode> _path;
    private int _currentStep;
    private PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;

    public MoveToTarget(PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel)
    {
        this._pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel; 
    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }
    
    public override void OnUpdate()
    {
        if(_currentStep >= _path.Count)
            _playerCharacterSC.movementDone = true;

        if (!_playerCharacterSC.movementDone)
        {
            _timeSinceLastStep += Time.deltaTime;
            if (_timeSinceLastStep >= TimePerStep)
            {
                _timeSinceLastStep -= TimePerStep;

                _playerCharacterSC.gridPosition = new Vector3Int(_path[_currentStep].x,
                                                       1,
                                                       _path[_currentStep].y);
                _playerCharacterSC.TransformToPosition();
                
                _currentStep++;
            }
        }
    }

    public override void OnStateEnter()
    {

        Vector3Int startNode = new Vector3Int(_playerCharacterSC.gridPosition.x,
                                            _playerCharacterSC.gridPosition.z,
                                            0);
        Vector3Int endNode = new Vector3Int(_playerCharacterSC.movementTarget.x,
                                            _playerCharacterSC.movementTarget.y,
                                            0);

        _pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, SavePath);

        _timeSinceLastStep = 0;
        _currentStep = 1;
        _playerCharacterSC.movementDone = false;
    }

    private void SavePath(List<PathNode> path)
    {
        this._path = path;
    }
}

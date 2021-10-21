using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using UnityEngine.InputSystem;
using Grid;
using Util;
using Ability;
using Ability.ScriptableObjects;

[CreateAssetMenu(fileName = "P_TargetCharacter_OnUpdate", menuName = "State Machines/Actions/Player/Target Character On Update")]
public class P_TargetCharacter_OnUpdateSO : StateActionSO
{
    [Header("SO Data")]
    [SerializeField] private GridDataSO globalGridData;
    [SerializeField] private AbilityContainerSO abilityContainer;

    public override StateAction CreateAction() => new P_TargetCharacter_OnUpdate(globalGridData, abilityContainer);
}

public class P_TargetCharacter_OnUpdate : StateAction
{
    private const float TimeBeforeAcceptingInput = 0.5f;
    private const bool Diagonal = false;

    private PlayerCharacterSC _playerStateContainer;

    private readonly GridDataSO _globalGridData;
    private readonly AbilityContainerSO _abilityContainer;

    private AbilityTarget _targets;
    // private TileProperties _conditions;
    private int _range;

    public P_TargetCharacter_OnUpdate(GridDataSO globalGridData,
                                      AbilityContainerSO abilityContainer)
    {
        this._globalGridData = globalGridData;
        this._abilityContainer = abilityContainer;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();

        _targets = _abilityContainer.abilities[_playerStateContainer.AbilityID].targets;
        // _conditions = _abilityContainer.abilities[_playerStateContainer.AbilityID].conditions;
        _range = _abilityContainer.abilities[_playerStateContainer.AbilityID].range;
    }

    public override void OnUpdate()
    {
        var characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
        if (_playerStateContainer.timeSinceTransition > TimeBeforeAcceptingInput && Mouse.current.leftButton.wasPressedThisFrame)
        {
            var pos = MousePosition.GetMouseWorldPosition(Vector3.up, 1f);
            Vector2Int mousePos = WorldPosToGridPos(pos);

            // Debug.Log("TargetCharacter: Mouse click position: " + mousePos.x + ", " + mousePos.y);

            // was a player clicked?
            // 
            if ((_targets & AbilityTarget.Ally) != 0)
            {
                foreach(var player in characterList.playerContainer) {
                    // Debug.Log("Player position: " + player.gridPosition.x + ", " + player.gridPosition.y + ", " + player.gridPosition.z);


                    var playerCharacterSc = player.GetComponent<PlayerCharacterSC>();
                    if (!playerCharacterSc.Equals(_playerStateContainer) &&
                        playerCharacterSc.gridPosition.x == mousePos.x && playerCharacterSc.gridPosition.z == mousePos.y && 
                        _range >= CalcDistance(_playerStateContainer.gridPosition, playerCharacterSc.gridPosition, Diagonal))
                    {
                        Debug.Log("Ally targeted. ");
                        // Confirm ability and set target
                        _playerStateContainer.playerTarget = playerCharacterSc;
                        _playerStateContainer.abilityConfirmed = true; 
                    }
                }
            }

            // was an enemy clicked?
            //
            if ((_targets & AbilityTarget.Enemy) != 0)
            {
                foreach (var enemy in characterList.enemyContainer) {
                    // Debug.Log("Enemy position: " + enemy.gridPosition.x + ", " + enemy.gridPosition.y + ", " + enemy.gridPosition.z);

                    var enemyCharacterSc = enemy.GetComponent<EnemyCharacterSC>();
                    if (enemyCharacterSc.gridPosition.x == mousePos.x && enemyCharacterSc.gridPosition.z == mousePos.y &&
                        _range >= CalcDistance(_playerStateContainer.gridPosition, enemyCharacterSc.gridPosition, Diagonal))
                    {
                        Debug.Log("Enemy targeted. ");
                        // Confirm ability and set target
                        _playerStateContainer.enemyTarget = enemyCharacterSc;
                        _playerStateContainer.abilityConfirmed = true;
                    }
                }
            }

            // was self clicked?
            //            
            if ((_targets & AbilityTarget.Self) != 0)
            {
                if(_playerStateContainer.gridPosition.x == mousePos.x && _playerStateContainer.gridPosition.z == mousePos.y)
                {
                    Debug.Log("Self targeted. ");
                    // Confirm ability and set target
                    _playerStateContainer.playerTarget = _playerStateContainer;
                    _playerStateContainer.abilityConfirmed = true;
                }
            }
        }
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    // TODO: Use util method instead
    // Calculates the distance
    private int CalcDistance(Vector3Int start, Vector3Int end, bool diagonal)
    {
        if (diagonal)
        {
            return Mathf.Max(Mathf.Abs(end.x - start.x), Mathf.Abs(end.z - start.z));
        }
        else
            return Mathf.Abs(end.x - start.x) + Mathf.Abs(end.z - start.z);
    }

    // TODO: Codeverdopplung vermeiden (copy paste aus PathfindingController) 
    public Vector2Int WorldPosToGridPos(Vector3 worldPos)
    {
        var lowerBounds = Vector3Int.FloorToInt(_globalGridData.OriginPosition);
        var flooredPos = Vector3Int.FloorToInt(worldPos);
        return new Vector2Int(
            x: flooredPos.x + Mathf.Abs(lowerBounds.x),
            y: flooredPos.z + Mathf.Abs(lowerBounds.z));
    }
}

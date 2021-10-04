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
using Characters.ScriptableObjects;
using Ability;
using Ability.ScriptableObjects;

[CreateAssetMenu(fileName = "p_TargetCharacter_OnUpdate", menuName = "State Machines/Actions/Player/Target Character On Update")]
public class p_TargetCharacter_OnUpdateSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] private GridDataSO globalGridData;
    [SerializeField] private CharacterContainerSO characterContainer;
    [SerializeField] private AbilityContainerSO abilityContainer;

    public override StateAction CreateAction() => new p_TargetCharacter_OnUpdate(globalGridData, characterContainer, abilityContainer);
}

public class p_TargetCharacter_OnUpdate : StateAction
{
    private const float TIME_BEFORE_ACCEPTING_INPUT = 0.5f;
    private const bool DIAGONAL = false;

    private PlayerCharacterSC playerStateContainer;

    private GridDataSO globalGridData;
    private CharacterContainerSO characterContainer;
    private AbilityContainerSO abilityContainer;

    private EAbilityTargetFlags targets;
    private ETileFlags conditions;
    private int range;

    public p_TargetCharacter_OnUpdate(GridDataSO globalGridData,
                                      CharacterContainerSO characterContainer,
                                      AbilityContainerSO abilityContainer)
    {
        this.globalGridData = globalGridData;
        this.characterContainer = characterContainer;
        this.abilityContainer = abilityContainer;
    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();

        targets = abilityContainer.abilities[playerStateContainer.AbilityID].targets;
        conditions = abilityContainer.abilities[playerStateContainer.AbilityID].conditions;
        range = abilityContainer.abilities[playerStateContainer.AbilityID].range;
    }

    public override void OnUpdate()
    {
        if (playerStateContainer.timeSinceTransition > TIME_BEFORE_ACCEPTING_INPUT && Mouse.current.leftButton.wasPressedThisFrame)
        {
            var pos = MousePosition.GetMouseWorldPosition(Vector3.up, 1f);
            Vector2Int mousePos = WorldPosToGridPos(pos);

            // Debug.Log("TargetCharacter: Mouse click position: " + mousePos.x + ", " + mousePos.y);

            // was a player clicked?
            // 
            if ((targets & EAbilityTargetFlags.ALLY) != 0)
            {
                foreach(PlayerCharacterSC player in characterContainer.playerContainer)
                {
                    // Debug.Log("Player position: " + player.gridPosition.x + ", " + player.gridPosition.y + ", " + player.gridPosition.z);

                    if (!player.Equals(playerStateContainer) &&
                        player.gridPosition.x == mousePos.x && player.gridPosition.z == mousePos.y && 
                        range >= calcDistance(playerStateContainer.gridPosition, player.gridPosition, DIAGONAL))
                    {
                        Debug.Log("Ally targeted. ");
                        // Confirm ability and set target
                        playerStateContainer.playerTarget = player;
                        playerStateContainer.abilityConfirmed = true; 
                    }
                }
            }

            // was an enemy clicked?
            //
            if ((targets & EAbilityTargetFlags.ENEMY) != 0)
            {
                foreach (EnemyCharacterSC enemy in characterContainer.enemyContainer)
                {
                    // Debug.Log("Enemy position: " + enemy.gridPosition.x + ", " + enemy.gridPosition.y + ", " + enemy.gridPosition.z);

                    if (enemy.gridPosition.x == mousePos.x && enemy.gridPosition.z == mousePos.y &&
                        range >= calcDistance(playerStateContainer.gridPosition, enemy.gridPosition, DIAGONAL))
                    {
                        Debug.Log("Enemy targeted. ");
                        // Confirm ability and set target
                        playerStateContainer.enemyTarget = enemy;
                        playerStateContainer.abilityConfirmed = true;
                    }
                }
            }

            // was self clicked?
            //            
            if ((targets & EAbilityTargetFlags.SELF) != 0)
            {
                if(playerStateContainer.gridPosition.x == mousePos.x && playerStateContainer.gridPosition.z == mousePos.y)
                {
                    Debug.Log("Self targeted. ");
                    // Confirm ability and set target
                    playerStateContainer.playerTarget = playerStateContainer;
                    playerStateContainer.abilityConfirmed = true;
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
    private int calcDistance(Vector3Int start, Vector3Int end, bool diagonal)
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
        var lowerBounds = Vector3Int.FloorToInt(globalGridData.OriginPosition);
        var flooredPos = Vector3Int.FloorToInt(worldPos);
        return new Vector2Int(
            x: flooredPos.x + Mathf.Abs(lowerBounds.x),
            y: flooredPos.z + Mathf.Abs(lowerBounds.z));
    }
}

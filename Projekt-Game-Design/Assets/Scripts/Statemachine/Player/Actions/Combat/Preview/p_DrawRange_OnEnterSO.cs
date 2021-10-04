using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Pathfinding;
using System.Collections.Generic;
using Util;
using Ability.ScriptableObjects;

[CreateAssetMenu(fileName = "p_DrawRange_OnEnter", menuName = "State Machines/Actions/Player/Draw Range On Enter")]
public class p_DrawRange_OnEnterSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] private NodeListEventChannelSO drawRangeEC;

    public override StateAction CreateAction() => new p_DrawRange_OnEnter(drawRangeEC);
}

public class p_DrawRange_OnEnter : StateAction
{
    private NodeListEventChannelSO drawRangeEC;
    private PlayerCharacterSC playerStateContainer;

    public p_DrawRange_OnEnter(NodeListEventChannelSO drawRangeEC)
    {
        this.drawRangeEC = drawRangeEC;
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
        drawRangeEC.RaiseEvent(playerStateContainer.tilesInRange);
    }

    public override void OnStateExit()
    {

    }
}

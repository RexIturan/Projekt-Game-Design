using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_UpdateAbilities_OnEnter", menuName = "State Machines/Actions/Player/Update Abilities On Enter")]
public class p_UpdateAbilities_OnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new p_UpdateAbilities_OnEnter();
}

public class p_UpdateAbilities_OnEnter : StateAction
{
    protected new p_UpdateAbilities_OnEnterSO OriginSO => (p_UpdateAbilities_OnEnterSO)base.OriginSO;

    private PlayerCharacterSC playerStateContainer;

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateEnter()
    {
        playerStateContainer.RefreshAbilities();
    }

    public override void OnStateExit()
    {

    }
}

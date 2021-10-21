using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "P_UpdateAbilities_OnEnter", menuName = "State Machines/Actions/Player/Update Abilities On Enter")]
public class P_UpdateAbilities_OnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new P_UpdateAbilities_OnEnter();
}

public class P_UpdateAbilities_OnEnter : StateAction
{
    protected new P_UpdateAbilities_OnEnterSO OriginSO => (P_UpdateAbilities_OnEnterSO)base.OriginSO;

    private PlayerCharacterSC _playerStateContainer;

    public override void Awake(StateMachine stateMachine)
    {
        _playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateEnter()
    {
        _playerStateContainer.RefreshAbilities();
    }

    public override void OnStateExit()
    {

    }
}

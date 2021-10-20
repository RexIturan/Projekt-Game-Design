using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_AbilityAffordable", menuName = "State Machines/Conditions/Ability Affordable ")]
public class P_AbilityAffordableSO : StateConditionSO
{
    [SerializeField] private AbilityContainerSO abilityContainer;

    protected override Condition CreateCondition() => new P_AbilityAffordable(abilityContainer);
}

public class P_AbilityAffordable : Condition
{
    protected new P_AbilityAffordableSO OriginSO => (P_AbilityAffordableSO)base.OriginSO;

    private PlayerCharacterSC _playerCharacterSc;
    private AbilityContainerSO _abilityContainer;

    public P_AbilityAffordable(AbilityContainerSO abilityContainer)
    {
        this._abilityContainer = abilityContainer;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    protected override bool Statement()
    {
        return _playerCharacterSc.energy >= _abilityContainer.abilities[_playerCharacterSc.AbilityID].costs;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

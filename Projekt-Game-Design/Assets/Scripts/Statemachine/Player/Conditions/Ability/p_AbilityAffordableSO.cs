using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_AbilityAffordable", menuName = "State Machines/Conditions/Ability Affordable ")]
public class p_AbilityAffordableSO : StateConditionSO
{
    [SerializeField] private AbilityContainerSO abilityContainer;

    protected override Condition CreateCondition() => new p_AbilityAffordable(abilityContainer);
}

public class p_AbilityAffordable : Condition
{
    protected new p_AbilityAffordableSO OriginSO => (p_AbilityAffordableSO)base.OriginSO;

    private PlayerCharacterSC playerCharacterSc;
    private AbilityContainerSO abilityContainer;

    public p_AbilityAffordable(AbilityContainerSO abilityContainer)
    {
        this.abilityContainer = abilityContainer;
    }

    public override void Awake(StateMachine stateMachine)
    {
        playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    protected override bool Statement()
    {
        return playerCharacterSc.energy >= abilityContainer.abilities[playerCharacterSc.AbilityID].costs;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }
}

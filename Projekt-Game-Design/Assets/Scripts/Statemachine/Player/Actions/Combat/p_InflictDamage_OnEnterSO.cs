using Ability;
using Ability.ScriptableObjects;
using Events.ScriptableObjects;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_InflictDamage_OnEnter", menuName = "State Machines/Actions/Player/Inflict Damage On Enter")]
public class p_InflictDamage_OnEnterSO : StateActionSO
{
    [Header("Sending events on: ")]
    [SerializeField] private CreateFloatingTextEventChannelSO createTextEC;

    [SerializeField] private AbilityContainerSO abilityContainer;

    public override StateAction CreateAction() => new p_InflictDamage_OnEnter(createTextEC, abilityContainer);
}

public class p_InflictDamage_OnEnter : StateAction
{
    protected new p_InflictDamage_OnEnterSO OriginSO => (p_InflictDamage_OnEnterSO)base.OriginSO;

    private CreateFloatingTextEventChannelSO createTextEC;

    private AbilityContainerSO abilityContainer;
    private PlayerCharacterSC playerCharacterSC;

    public p_InflictDamage_OnEnter(CreateFloatingTextEventChannelSO createTextEC, AbilityContainerSO abilityContainer)
    {
        this.createTextEC = createTextEC;
        this.abilityContainer = abilityContainer;
}

    public override void Awake(StateMachine stateMachine)
    {
        playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateEnter()
    {


        // TODO: check conditions for targeted effects and don't 
        //       just inflict damage on target
        AbilitySO ability = abilityContainer.abilities[playerCharacterSC.AbilityID];
        Debug.Log("Ability: " + ability.name);
        int damage = 0;
        foreach (TargetedEffect targetedEffect in ability.targetedEffects)
        {
            Effect effect = targetedEffect.effect;
            int effectDamage = effect.baseDamage +
                               (int)effect.strengthBonus * playerCharacterSC.CurrentStats.strength +
                               (int)effect.dexterityBonus * playerCharacterSC.CurrentStats.dexterity +
                               (int)effect.intelligenceBonus * playerCharacterSC.CurrentStats.intelligence;

            if (effect.type == EDamageType.HEALING)
                effectDamage = -effectDamage;

            damage += effectDamage;
        }

        Color damageColor;

        if (damage > 0)
            damageColor = Color.red;
        else if (damage < 0)
            damageColor = Color.green;
        else
            damageColor = Color.grey;

        
        if (playerCharacterSC.enemyTarget) {
            playerCharacterSC.enemyTarget.healthPoints -= damage;
            
            createTextEC.RaiseEvent(Mathf.Abs(damage).ToString(), 
                playerCharacterSC.enemyTarget.gameObject.transform.position + Vector3.up, 
                damageColor);
        }
        if (playerCharacterSC.playerTarget) {
            playerCharacterSC.playerTarget.healthPoints -= damage;

            createTextEC.RaiseEvent(Mathf.Abs(damage).ToString(),
                playerCharacterSC.playerTarget.gameObject.transform.position + Vector3.up,
                damageColor);
        }
        
    }
}

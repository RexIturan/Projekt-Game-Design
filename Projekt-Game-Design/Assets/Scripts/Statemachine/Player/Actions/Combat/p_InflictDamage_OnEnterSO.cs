using Ability;
using Events.ScriptableObjects;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_InflictDamage_OnEnter", menuName = "State Machines/Actions/Player/Inflict Damage On Enter")]
public class p_InflictDamage_OnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new p_InflictDamage_OnEnter();
}

public class p_InflictDamage_OnEnter : StateAction
{
    protected new p_InflictDamage_OnEnterSO OriginSO => (p_InflictDamage_OnEnterSO)base.OriginSO;

    private PlayerCharacterSC playerCharacterSC;

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
        AbilitySO ability = playerCharacterSC.Abilities[playerCharacterSC.AbilityID];
        int damage = 0;
        foreach(TargetedEffect targetedEffect in ability.targetedEffects)
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

        if (playerCharacterSC.enemyTarget)
            playerCharacterSC.enemyTarget.healthPoints -= damage;
        if (playerCharacterSC.playerTarget)
            playerCharacterSC.playerTarget.healthPoints -= damage;
    }
}

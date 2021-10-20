using Ability;
using Ability.ScriptableObjects;
using Events.ScriptableObjects;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_InflictDamage_OnEnter", menuName = "State Machines/Actions/Player/Inflict Damage On Enter")]
public class P_InflictDamage_OnEnterSO : StateActionSO
{
    [Header("Sending events on: ")]
    [SerializeField] private CreateFloatingTextEventChannelSO createTextEC;

    [SerializeField] private AbilityContainerSO abilityContainer;

    public override StateAction CreateAction() => new P_InflictDamage_OnEnter(createTextEC, abilityContainer);
}

public class P_InflictDamage_OnEnter : StateAction
{
    protected new P_InflictDamage_OnEnterSO OriginSO => (P_InflictDamage_OnEnterSO)base.OriginSO;

    private CreateFloatingTextEventChannelSO _createTextEC;

    private AbilityContainerSO _abilityContainer;
    private PlayerCharacterSC _playerCharacterSC;

    public P_InflictDamage_OnEnter(CreateFloatingTextEventChannelSO createTextEC, AbilityContainerSO abilityContainer)
    {
        this._createTextEC = createTextEC;
        this._abilityContainer = abilityContainer;
}

    public override void Awake(StateMachine stateMachine)
    {
        _playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {

    }

    public override void OnStateEnter()
    {


        // TODO: check conditions for targeted effects and don't 
        //       just inflict damage on target
        //AbilitySO ability = playerCharacterSC.Abilities[playerCharacterSC.AbilityID];
        AbilitySO ability = _abilityContainer.abilities[_playerCharacterSC.AbilityID];
        Debug.Log("Ability: " + ability.name);
        
        int damage = 0;
        foreach (TargetedEffect targetedEffect in ability.targetedEffects)
        {
            Effect effect = targetedEffect.effect;
            int effectDamage = effect.baseDamage +
                               (int)effect.strengthBonus * _playerCharacterSC.CurrentStats.strength +
                               (int)effect.dexterityBonus * _playerCharacterSC.CurrentStats.dexterity +
                               (int)effect.intelligenceBonus * _playerCharacterSC.CurrentStats.intelligence;

            if (effect.type == DamageType.Healing)
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

        
        if (_playerCharacterSC.enemyTarget) {
            _playerCharacterSC.enemyTarget.healthPoints -= damage;
            
            _createTextEC.RaiseEvent(Mathf.Abs(damage).ToString(), 
                _playerCharacterSC.enemyTarget.gameObject.transform.position + Vector3.up, 
                damageColor);
        }
        if (_playerCharacterSC.playerTarget) {
            _playerCharacterSC.playerTarget.healthPoints -= damage;

            _createTextEC.RaiseEvent(Mathf.Abs(damage).ToString(),
                _playerCharacterSC.playerTarget.gameObject.transform.position + Vector3.up,
                damageColor);
        }
        
    }
}

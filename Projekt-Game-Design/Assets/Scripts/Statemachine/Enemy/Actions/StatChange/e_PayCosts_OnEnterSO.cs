using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions
{
    [CreateAssetMenu(fileName = "e_PayCosts_OnEnter", menuName = "State Machines/Actions/Enemy/e_PayCosts_OnEnter")]
    public class e_PayCosts_OnEnterSO : StateActionSO
    {
        [SerializeField] private AbilityContainerSO abilityContainer;
        public override StateAction CreateAction() => new e_PayCosts_OnEnter(abilityContainer);
    }

    public class e_PayCosts_OnEnter : StateAction
    {
        protected new e_PayCosts_OnEnterSO OriginSO => (e_PayCosts_OnEnterSO)base.OriginSO;
        private EnemyCharacterSC enemyCharacterSC;
        private readonly AbilityContainerSO abilityContainer;

        public e_PayCosts_OnEnter(AbilityContainerSO abilityContainer)
        {
            this.abilityContainer = abilityContainer;
        }

        public override void Awake(UOP1.StateMachine.StateMachine stateMachine)
        {
            enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public override void OnUpdate()
        {
        }

        public override void OnStateEnter()
        {
            var currentAbility = abilityContainer.abilities[enemyCharacterSC.abilityID];

            var energyReduction = currentAbility.costs + (currentAbility.moveToTarget ? enemyCharacterSC.GetEnergyUseUpFromMovement() : 0);
            enemyCharacterSC.energy -= energyReduction;
        }

        public override void OnStateExit()
        {

        }
    }
}
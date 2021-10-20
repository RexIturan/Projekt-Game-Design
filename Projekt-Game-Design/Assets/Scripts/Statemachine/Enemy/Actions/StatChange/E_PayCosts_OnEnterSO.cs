using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Statemachine.Enemy.Actions.StatChange {
    [CreateAssetMenu(fileName = "e_PayCosts_OnEnter", menuName = "State Machines/Actions/Enemy/PayCosts OnEnter")]
    public class E_PayCosts_OnEnterSO : StateActionSO {
        [SerializeField] private AbilityContainerSO abilityContainer;
        public override StateAction CreateAction() => new E_PayCosts_OnEnter(abilityContainer);
    }

    public class E_PayCosts_OnEnter : StateAction {
        protected new E_PayCosts_OnEnterSO OriginSO => (E_PayCosts_OnEnterSO) base.OriginSO;
        private EnemyCharacterSC _enemyCharacterSC;
        private readonly AbilityContainerSO _abilityContainer;

        public E_PayCosts_OnEnter(AbilityContainerSO abilityContainer) {
            this._abilityContainer = abilityContainer;
        }

        public override void Awake(StateMachine stateMachine) {
            _enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public override void OnUpdate() { }

        public override void OnStateEnter() {
            var currentAbility = _abilityContainer.abilities[_enemyCharacterSC.abilityID];

            // var energyReduction =  
                //currentAbility.costs + (currentAbility.moveToTarget ? enemyCharacterSC.GetEnergyUseUpFromMovement() : 0);
            // enemyCharacterSC.energy -= energyReduction;
        }

        public override void OnStateExit() { }
    }
}
using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions
{
    [CreateAssetMenu(fileName = "e_ClearCache_OnExit", menuName = "State Machines/Actions/Enemy/e_ClearCache_OnExit")]
    public class e_ClearCache_OnExitSO : StateActionSO
    {
        [Header("Clear the following fields")]
        [SerializeField] private bool isOnTurn;
        [SerializeField] private bool isDone;
        [SerializeField] private bool abilitySelected;
        [SerializeField] private bool abilityExecuted;
        [SerializeField] private bool abilityID;
        [SerializeField] private bool movementTarget;
        [SerializeField] private bool waitForAttack;

        public override StateAction CreateAction() => new e_ClearCache_OnExit(isOnTurn, isDone, abilitySelected, 
                                                                              abilityExecuted, abilityID, movementTarget,
                                                                              waitForAttack);
    }

    public class e_ClearCache_OnExit : StateAction
    {
        private EnemyCharacterSC enemyCharacterSC;
        
        private bool isOnTurn;
        private bool isDone;
        private bool abilitySelected;
        private bool abilityExecuted;
        private bool abilityID;
        private bool movementTarget;
        private bool waitForAttack;

        public e_ClearCache_OnExit(bool isOnTurn, bool isDone, bool abilitySelected, 
                                   bool abilityExecuted, bool abilityID, bool movementTarget,
                                   bool waitForAttack)
        {
            this.isOnTurn = isOnTurn;
            this.isDone = isDone;
            this.abilitySelected = abilitySelected;
            this.abilityExecuted = abilityExecuted;
            this.abilityID = abilityID;
            this.movementTarget = movementTarget;
            this.waitForAttack = waitForAttack;
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

        }

        public override void OnStateExit()
        {
            if (isOnTurn)
                enemyCharacterSC.isOnTurn = false;

            if (isDone)
                enemyCharacterSC.isDone = false;

            if (abilitySelected)
                enemyCharacterSC.abilitySelected = false;

            if (abilityExecuted)
                enemyCharacterSC.abilityExecuted = false;

            if (abilityID)
                enemyCharacterSC.abilityID = 0;

            if (movementTarget)
                enemyCharacterSC.movementTarget = null;

            if (waitForAttack)
                enemyCharacterSC.waitForAttackToFinish = false;
        }
    }
}

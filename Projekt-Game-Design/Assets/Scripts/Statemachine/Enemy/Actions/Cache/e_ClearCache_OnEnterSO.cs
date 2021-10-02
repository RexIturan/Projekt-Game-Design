using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions
{
    [CreateAssetMenu(fileName = "e_ClearCache_OnEnter", menuName = "State Machines/Actions/Enemy/e_ClearCache_OnEnter")]
    public class e_ClearCache_OnEnterSO : StateActionSO
    {
        [Header("Clear the following fields")]
        [SerializeField] private bool isOnTurn;
        [SerializeField] private bool isDone;
        [SerializeField] private bool abilitySelected;
        [SerializeField] private bool abilityExecuted;
        [SerializeField] private bool abilityID;
        [SerializeField] private bool movementTarget;

        public override StateAction CreateAction() => new e_ClearCache_OnEnter(isOnTurn, isDone, abilitySelected,
                                                                              abilityExecuted, abilityID, movementTarget);
    }

    public class e_ClearCache_OnEnter : StateAction
    {
        private EnemyCharacterSC enemyCharacterSC;

        private bool isOnTurn;
        private bool isDone;
        private bool abilitySelected;
        private bool abilityExecuted;
        private bool abilityID;
        private bool movementTarget;

        public e_ClearCache_OnEnter(bool isOnTurn, bool isDone, bool abilitySelected,
                                   bool abilityExecuted, bool abilityID, bool movementTarget)
        {
            this.isOnTurn = isOnTurn;
            this.isDone = isDone;
            this.abilitySelected = abilitySelected;
            this.abilityExecuted = abilityExecuted;
            this.abilityID = abilityID;
            this.movementTarget = movementTarget;
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
        }

        public override void OnStateExit()
        {

        }
    }
}
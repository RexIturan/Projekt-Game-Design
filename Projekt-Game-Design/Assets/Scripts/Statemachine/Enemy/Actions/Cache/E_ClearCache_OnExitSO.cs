using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions
{
    [CreateAssetMenu(fileName = "e_ClearCache_OnExit", menuName = "State Machines/Actions/Enemy/e_ClearCache_OnExit")]
    public class E_ClearCache_OnExitSO : StateActionSO
    {
        [Header("Clear the following fields")]
        [SerializeField] private bool isOnTurn;
        [SerializeField] private bool isDone;
        [SerializeField] private bool abilitySelected;
        [SerializeField] private bool abilityExecuted;
        [SerializeField] private bool abilityID;
        [SerializeField] private bool movementTarget;

        public override StateAction CreateAction() => new E_ClearCache_OnExit(isOnTurn, isDone, abilitySelected, 
                                                                              abilityExecuted, abilityID, movementTarget);
    }

    public class E_ClearCache_OnExit : StateAction
    {
        private EnemyCharacterSC _enemyCharacterSC;
        
        private readonly bool _isOnTurn;
        private readonly bool _isDone;
        private readonly bool _abilitySelected;
        private readonly bool _abilityExecuted;
        private readonly bool _abilityID;
        private readonly bool _movementTarget;

        public E_ClearCache_OnExit(bool isOnTurn, bool isDone, bool abilitySelected, 
                                   bool abilityExecuted, bool abilityID, bool movementTarget)
        {
            _isOnTurn = isOnTurn;
            _isDone = isDone;
            _abilitySelected = abilitySelected;
            _abilityExecuted = abilityExecuted;
            _abilityID = abilityID;
            _movementTarget = movementTarget;
    }

        public override void Awake(UOP1.StateMachine.StateMachine stateMachine)
        {
            _enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public override void OnUpdate()
        {
        }

        public override void OnStateEnter()
        {

        }

        public override void OnStateExit()
        {
            if (_isOnTurn)
                _enemyCharacterSC.isOnTurn = false;

            if (_isDone)
                _enemyCharacterSC.isDone = false;

            if (_abilitySelected)
                _enemyCharacterSC.abilitySelected = false;

            if (_abilityExecuted)
                _enemyCharacterSC.abilityExecuted = false;

            if (_abilityID)
                _enemyCharacterSC.abilityID = 0;

            if (_movementTarget)
                _enemyCharacterSC.movementTarget = null;
        }
    }
}
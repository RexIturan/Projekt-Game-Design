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
        [SerializeField] private bool waitForAttack;

        public override StateAction CreateAction() => new E_ClearCache_OnExit(isOnTurn, isDone, abilitySelected, 
                                                                              abilityExecuted, abilityID, movementTarget,
                                                                              waitForAttack);
    }

    public class E_ClearCache_OnExit : StateAction
    {
        private EnemyCharacterSC _enemyCharacterSC;
        
        private bool _isOnTurn;
        private bool _isDone;
        private bool _abilitySelected;
        private bool _abilityExecuted;
        private bool _abilityID;
        private bool _movementTarget;
        private bool _waitForAttack;

        public E_ClearCache_OnExit(bool isOnTurn, bool isDone, bool abilitySelected, 
                                   bool abilityExecuted, bool abilityID, bool movementTarget,
                                   bool waitForAttack)
        {
            this._isOnTurn = isOnTurn;
            this._isDone = isDone;
            this._abilitySelected = abilitySelected;
            this._abilityExecuted = abilityExecuted;
            this._abilityID = abilityID;
            this._movementTarget = movementTarget;
            this._waitForAttack = waitForAttack;
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

            if (_waitForAttack)
                _enemyCharacterSC.waitForAttackToFinish = false;
        }
    }
}

using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using GDP01;
using StateMachine = UOP1.StateMachine.StateMachine;
using Characters.EnemyCharacter;
using GDP01.Characters.Component;
using GDP01.World.Components;
using Characters.Movement;

[CreateAssetMenu(fileName = "e_MakeMoveOnEnter", menuName = "State Machines/Actions/Enemy/e_MakeMoveOnEnter")]
public class E_MakeMoveOnEnterSO : StateActionSO {
    public override StateAction CreateAction() => new E_MakeMoveOnEnter();
}

public class E_MakeMoveOnEnter : StateAction {
		private Attacker _attacker;
		private Statistics statistics;
		private EnemyCharacterSC _enemySC;
		private AIController _aiController;
		private AbilityController _abilityController;

    private GameObject _targetPlayer;
    private PathNode _closesTileToPlayer;

		private bool _canMove;

    public override void OnUpdate() { }

    public override void Awake(StateMachine stateMachine) {
	    statistics = stateMachine.gameObject.GetComponent<Statistics>();
	    _attacker = stateMachine.gameObject.GetComponent<Attacker>();
      _enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
			_aiController = stateMachine.gameObject.GetComponent<AIController>();
			_abilityController = _enemySC.gameObject.GetComponent<AbilityController>();
		}

    public override void OnStateEnter() {
				_aiController.ClearFullCache();
				_abilityController.RefreshAbilities();
				_aiController.RefreshValidAbilities();

				EnemyBehaviorSO behavior = _aiController.GetBehavior();

				bool actionTaken = false;

				for(int i = 0; !actionTaken && i < behavior.actionPriorities.Count; i++) {
						switch ( behavior.actionPriorities[i] ) {
								case AIAction.SKIP:
										actionTaken = HandleSkip();
										break;
								case AIAction.ATTACK:
										actionTaken = HandleAttack();
										break;
								case AIAction.SUPPORT:
										actionTaken = HandleSupport();
										break;
								case AIAction.MOVE_TO_ATTACK:
										actionTaken = HandleMoveToAttack();
										break;
								case AIAction.MOVE_TO_SUPPORT:
										actionTaken = HandleMoveToSupport();
										break;
						}
				}

				if ( !actionTaken )
						HandleSkip();
		}

		/// <summary>
		/// Skips the turn. 
		/// </summary>
		/// <returns>True because skipping is always a valid move to make </returns>
		private bool HandleSkip() {
        _enemySC.isDone = true;
				return true;
		}

		/// <summary>
		/// Tries to attack the target. 
		/// </summary>
		/// <returns>True if the target can be attacked </returns>
		private bool HandleAttack() {
				return _aiController.ChooseAbilityWithHighestDamageOutput();
		}

		/// <summary>
		/// Tries to move towards target. 
		/// </summary>
		/// <returns>True if enemy can successfully move towards the target </returns>
		private bool HandleMoveToAttack() {
				_aiController.TargetClosestVisiblePlayer(_attacker.visibleTiles);
				return TryToMove();
		}

		private bool HandleSupport() {
				return _aiController.ChooseAbilityWithHighestHealingOutput();
		}

		private bool HandleMoveToSupport() {
				_aiController.TargetEnemyWithLowestHealth(_attacker.visibleTiles);

				if ( _aiController.aiTarget ) {
						return TryToMove();
				}
				else
						return false;
		}

		private bool TryToMove() {
				bool actionSelected = false;
				
				// try to move towards target
				_aiController.TargetNearestTileToTarget();

				if ( _aiController.movementTarget != null ) {
						// does enemy character have moveing ability?
						int abilityId = -1;
						foreach ( var ability in _abilityController.Abilities ) {
								// TODO: Here, it is not checked if the ability is affordable 
								if ( ability.moveToTarget ) {
										abilityId = ability.id;
								}
						}

						if ( abilityId != -1 ) {
								// moving ability available, so execute it
								_abilityController.SelectedAbilityID = abilityId;
								_abilityController.abilityConfirmed = true;

								actionSelected = true;
						}
				}

				return actionSelected;
		}
}
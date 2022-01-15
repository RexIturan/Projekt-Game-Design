using Events.ScriptableObjects;
using System.Collections.Generic;
using Characters;
using Characters.Movement;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;
using Characters.EnemyCharacter;
using Characters.Ability;
using Combat;

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
		private EnemyBehaviorSO _behavior;

    private GameObject _targetPlayer;
    private PathNode _closesTileToPlayer;

		private bool _canMove;

    public override void OnUpdate() { }

    public override void Awake(StateMachine stateMachine) {
	    statistics = stateMachine.gameObject.GetComponent<Statistics>();
	    _attacker = stateMachine.gameObject.GetComponent<Attacker>();
        _enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        _behavior = _enemySC.behavior;
				_aiController = stateMachine.gameObject.GetComponent<AIController>();
				_abilityController = _enemySC.gameObject.GetComponent<AbilityController>();
		}

    public override void OnStateEnter() {
				_aiController.ClearFullCache();

				if ( _enemySC.behavior.alwaysSkip )
						Skip();
				else
				{
						_abilityController.RefreshAbilities();
						
						
						_aiController.TargetClosestVisiblePlayer(_attacker.visibleTiles);
						// _aiController.TargetClosestPlayer();
						if ( !_aiController.aiTarget )
								Skip();
						else
						{
								// Debug.Log("Closest player found ");

								// try to attack player
								_aiController.SaveValidAbilities();
								if ( _aiController.validAbilities.Count > 0 )
								{
										// choose random ability
										// choose index of ability in ValidAbility list, index is between 0 and validAbilities.Count - 1
										int randomAbility = Mathf.FloorToInt(Random.value * _aiController.validAbilities.Count);

										_abilityController.SelectedAbilityID = _aiController.validAbilities[randomAbility].id;
										_abilityController.abilityConfirmed = true;
								}
								else
								{
										// try to move towards player
										_aiController.TargetNearestTileToPlayerTarget();
										if ( _aiController.movementTarget != null )
										{
												// execute movement
												// Debug.Log("Closest Tile to player target found ");

												// does enemy character have moveing ability?
												int abilityId = -1;
												foreach ( var ability in _abilityController.Abilities )
												{
														if ( ability.moveToTarget )
														{
																abilityId = ability.id;
														}
												}

												if ( abilityId != -1 )
												{
														// moving ability available, so execute it
														_abilityController.SelectedAbilityID = abilityId;
														_abilityController.abilityConfirmed = true;
												}
												else
														Skip();
										}
										else
												Skip();
								}
						}
				}
    }

    private void Skip() {
        _enemySC.isDone = true;
    }
}
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

[CreateAssetMenu(fileName = "e_MakeMoveOnEnter", menuName = "State Machines/Actions/Enemy/e_MakeMoveOnEnter")]
public class E_MakeMoveOnEnterSO : StateActionSO {
    public override StateAction CreateAction() => new E_MakeMoveOnEnter();
}

public class E_MakeMoveOnEnter : StateAction {
    private EnemyCharacterSC _enemySC;
		private AIController _aiController;
		private AbilityController _abilityController;
		private EnemyBehaviorSO _behavior;

    private GameObject _targetPlayer;
    private PathNode _closesTileToPlayer;


		private bool _canMove;

    private PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;

    public override void OnUpdate() { }

    public override void Awake(StateMachine stateMachine) {
        _enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        _behavior = _enemySC.behavior;
				_aiController = stateMachine.gameObject.GetComponent<AIController>();
				_abilityController = _enemySC.gameObject.GetComponent<AbilityController>();
		}

    public override void OnStateEnter() {
				_abilityController.RefreshAbilities();

				_aiController.TargetClosestPlayer();
				if ( !_aiController.playerTarget )
						Skip();
				else
				{
						Debug.Log("Closest player found ");
						_aiController.TargetNearestTileToPlayerTarget();
						if ( _aiController.movementTarget != null)
						{
								// execute movement
								Debug.Log("Closest Tile to player target found ");

								// does enemy character have moveing ability?
								int abilityId = -1;
								foreach(var ability in _abilityController.Abilities)
								{
										if(ability.moveToTarget)
										{
												abilityId = ability.abilityID;
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
				// TODO: Make AI later on
				/*
        if (_behavior.alwaysSkip)
            Skip();
        else {
            _canMove = true;

            // TODO: find nearest player instead
            _targetPlayer = _enemySC.characterList.playerContainer[0].gameObject;
            var targetContainer = _targetPlayer.GetComponent<GridTransform>();

            Vector3Int startNode = new Vector3Int(_enemySC.gridPosition.x,
                _enemySC.gridPosition.z,
                0);
            Vector3Int endNode = new Vector3Int(targetContainer.gridPosition.x,
                targetContainer.gridPosition.z,
                0);

            _pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, SaveClosestToPlayer);

            if (_canMove) {
                _enemySC.movementTarget = _closesTileToPlayer;
                _enemySC.abilityID = 0;
                _enemySC.abilitySelected = true;
            }
            else
                Skip();
        }
				*/
    }

    private void Skip() {
        _enemySC.isDone = true;
    }

		/*
    private void SaveClosestToPlayer(List<PathNode> path) {
        int index = 0;
        for (int i = 1; i < path.Count; i++) {
            // TODO: distance instead of GCost? 
            if (path[i].gCost <= _enemySC.movementPointsPerEnergy * _enemySC.energy)
                index = i;
            else
                break;
        }

        if (index == 0) {
            Debug.Log("Gegner kann sich nicht bewegen");
            _closesTileToPlayer = null;
            _canMove = false;
        }
        else {
            path[index].dist = path[index].gCost;
            _closesTileToPlayer = path[index];
        }
    }
		*/
}
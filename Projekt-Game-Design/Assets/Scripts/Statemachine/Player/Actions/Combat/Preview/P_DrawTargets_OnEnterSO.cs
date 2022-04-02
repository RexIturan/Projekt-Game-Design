using Ability;
using Characters;
using Combat;
using Events.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using GDP01._Gameplay.Provider;
using GDP01.Characters.Component;
using GDP01.World.Components;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using WorldObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_DrawTargets_OnEnterSO",
	menuName = "State Machines/Actions/Player/Draw Targets On Enter")]
public class P_DrawTargets_OnEnterSO : StateActionSO {
	[Header("Sending Events On")] 
	[SerializeField] private DrawTargetTilesEventChannelSO drawTargetTilesEC;

	public override StateAction CreateAction() => new P_DrawTargets_OnEnter(drawTargetTilesEC);
}

public class P_DrawTargets_OnEnter : StateAction {
	private readonly DrawTargetTilesEventChannelSO _drawTargetTilesEC;
	private Attacker _attacker;
	private AbilityController _abilityController;
	private GridTransform _gridTransform;

	public P_DrawTargets_OnEnter(DrawTargetTilesEventChannelSO drawTargetTilesEC) {
		_drawTargetTilesEC = drawTargetTilesEC;
	}

	public override void Awake(StateMachine stateMachine) {
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() {
		WorldObjectList worldObjects = WorldObjectList.FindInstant();

		List<PathNode> allies = new List<PathNode>();
		List<PathNode> neutrals = new List<PathNode>();
		List<PathNode> enemies = new List<PathNode>();

		AbilitySO ability = _abilityController.GetSelectedAbility();

		// TODO: (more or less) Create list of all targetables and then compare the faction value of character/object

		// add nodes if target is existent and proper for ability
		//
		foreach(PathNode tile in _attacker.tilesInRange) {
			// self
			if (ability.targets.HasFlag(TargetRelationship.Self) && 
					tile.pos.Equals(_gridTransform.gridPosition)) {
				allies.Add(tile);
			}

			// allies
			if (ability.targets.HasFlag(TargetRelationship.Ally)) {
				bool allyTarget = false;

				List<PlayerCharacterSC> allyCharcters = GameplayProvider.Current.CharacterManager
					.GetPlayerCharactersWhere(
						player => 
							player.GridPosition.Equals(tile.pos) && player.gameObject != _attacker.gameObject)
					.ToList();

				if ( allyCharcters is { Count: > 0 } ) {
					allyTarget = true;
				}
				
				if(allyTarget)
					allies.Add(tile);
			}

			// neutrals
			if (ability.targets.HasFlag(TargetRelationship.Neutral)) {
				bool neutralTarget = false;
				
				List<GameObject> neutralObjects = new List<GameObject>();

				WorldObjectManager worldObjectManager = GameplayProvider.Current.WorldObjectManager;
				
				neutralObjects.AddRange(worldObjectManager.GetDoors().Select(door => door.gameObject));
				neutralObjects.AddRange(worldObjectManager.GetJunks().Select(junk => junk.gameObject));

				foreach(GameObject neutralObj in neutralObjects) {
					Targetable neutralTargetable = neutralObj.GetComponent<Targetable>();
					if( neutralTargetable && neutralTargetable.GetGridPosition().Equals(tile.pos))
						neutralTarget = true;
				}

				if( neutralTarget )
					neutrals.Add(tile);
			}
			
			// enemy
			if (ability.targets.HasFlag(TargetRelationship.Enemy)) {
				bool enemyTarget = false;

				List<EnemyCharacterSC> foundEnemysAtPosition = GameplayProvider.Current.CharacterManager
					.GetEnemyCahractersWhere(
						enemy => enemy.GridPosition.Equals(tile.pos) && enemy.IsAlive)
					.ToList();

				if ( foundEnemysAtPosition is { Count: > 0 } ) {
					enemyTarget = true;
				}
				
				// foreach(GameObject enemy in characters.enemyContainer) {
				// 	Targetable enemyTargetable = enemy.GetComponent<Targetable>();
				// 	if( enemyTargetable && enemyTargetable.GetGridPosition().Equals(tile.pos))
				// 		enemyTarget = true;
				// }

				if(enemyTarget)
					enemies.Add(tile);
			}
		}

		_drawTargetTilesEC.RaiseEvent(allies, neutrals, enemies, ability.damaging);
	}

	public override void OnStateExit() { }
}
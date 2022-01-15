using Characters;
using Characters.Ability;
using Characters.EnemyCharacter;
using Characters.EnemyCharacter.ScriptableObjects;
using Characters.Equipment;
using Characters.Movement;
using Combat;
using SaveSystem.SaveFormats;
using UnityEngine;

/// <summary><c>Enemy State Container</c> Script to attached to each enemy</summary>
[System.Serializable]
public class EnemyCharacterSC : MonoBehaviour
{
		[Header("Basic Stats")]
		// Base stats
		public EnemyTypeSO enemyType;
		// public EnemySpawnDataSO enemySpawnData;
		public EnemyBehaviorSO behavior;

		[SerializeField] private Statistics _statistics;
		[SerializeField] private GridTransform _gridTransform;
		[SerializeField] private Attacker _attacker;
		[SerializeField] private MovementController _movementController;
		[SerializeField] private AbilityController _abilityController;
		[SerializeField] private ModelController _modelController;
		[SerializeField] private AIController _aIController;

		[Header("Statemachine")]
		public bool isNextToAct; // it's the enemy character's turn to act (decided by Enemy Controller)
		public bool isDone; // this enemy character in particular is done
		public bool isDead; // set when enemy enters the dead state
		public bool abilitySelected;
		public bool abilityExecuted;
		public bool noTargetFound;
		public bool rangeChecked;

		public void Initialize()
		{
				//stats
				_statistics.SetFaction(Faction.Enemy);
				_statistics.StatusValues.InitValues(enemyType.baseStatusValues);

			//movement Position
			_movementController.movementPointsPerEnergy = enemyType.movementPointsPerEnergy;

				//Grid Position
				_gridTransform.gridPosition = Vector3Int.zero;

			// Equipment
			// maybe later

			//Abilities
			_abilityController.RefreshAbilities();
			_abilityController.BaseAbilities = enemyType.basicAbilities;
			_abilityController.damageInflicted = true;

			//model
			_modelController.prefab = enemyType.modelPrefab;
			_modelController.Initialize();
			_modelController.SetStandardHead(enemyType.headModel);
			_modelController.SetStandardBody(enemyType.bodyModel);
			_modelController.SetMeshHead(null);
			_modelController.SetMeshBody(null);

			//ai
			behavior = enemyType.behaviour;
			_aIController.SetBehavior(behavior);
		}
		
		public void InitializeFromSave(Enemy_Save saveData) {
			Initialize();
			_statistics.StatusValues.HitPoints.value = saveData.hitpoints;
			_statistics.StatusValues.Energy.value = saveData.energy;
			_gridTransform.gridPosition = saveData.pos;
		}
}

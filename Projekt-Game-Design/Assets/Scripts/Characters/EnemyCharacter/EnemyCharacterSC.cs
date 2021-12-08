using Characters;
using Characters.Ability;
using Characters.EnemyCharacter.ScriptableObjects;
using Characters.Equipment;
using Characters.Movement;
using Combat;
using UnityEngine;

/// <summary><c>Enemy State Container</c> Script to attached to each enemy</summary>
[System.Serializable]
public class EnemyCharacterSC : MonoBehaviour
{
		[Header("Basic Stats")]
		// Base stats
		public EnemyTypeSO enemyType;
		public EnemySpawnDataSO enemySpawnData;
		public EnemyBehaviorSO behavior;

		[SerializeField] private Statistics _statistics;
		[SerializeField] private GridTransform _gridTransform;
		[SerializeField] private Attacker _attacker;
		[SerializeField] private MovementController _movementController;
		[SerializeField] private AbilityController _abilityController;
		[SerializeField] private ModelController _modelController;

		[Header("Statemachine")]
		public bool isOnTurn; // it's Enemy's turn
		public bool isDone; // this enemy in particular is done
		public bool abilitySelected;
		public bool abilityExecuted;
		public bool noTargetFound;
		public bool rangeChecked;

		public void Initialize()
		{
				//stats
				_statistics.StatusValues.InitValues(enemySpawnData.overrideStatusValues);
				_statistics.SetFaction(Faction.Enemy);

				//movement Position
				_movementController.movementPointsPerEnergy = enemyType.movementPointsPerEnergy;

				//Grid Position
				_gridTransform.gridPosition = enemySpawnData.gridPos;

				// Equipment
				// maybe later

				//Abilities
				_abilityController.RefreshAbilities();
				_abilityController.BaseAbilities = enemyType.basicAbilities;

				//model
				_modelController.prefab = enemyType.modelPrefab;
		}
}


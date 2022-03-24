using System;
using Characters.EnemyCharacter;
using GDP01._Gameplay.World.Character.Components;
using GDP01._Gameplay.World.Character.Data;
using GDP01.Loot.ScriptableObjects;
using SaveSystem.SaveFormats;
using UnityEngine;
using static Characters.Types.Faction;

/// <summary><c>Enemy State Container</c> Script to attached to each enemy</summary>
[Serializable]
public class EnemyCharacterSC : Character<EnemyCharacterSC, EnemyCharacterData> {
	[Header("Basic Stats")]
	// Base stats
	// public EnemyTypeSO enemyType;
	public EnemyBehaviorSO behavior;

	[SerializeField] private AIController _aIController;


	[Header("Statemachine")]
	public bool isNextToAct; // it's the enemy character's turn to act (decided by Enemy Controller)

	public bool isDone; // this enemy character in particular is done
	public bool isDead; // set when enemy enters the dead state
	public bool abilitySelected;
	public bool abilityExecuted;
	public bool noTargetFound;
	public bool rangeChecked;


	//Properties
	public LootTableSO LootTable { get; set; }

	public EnemyTypeSO Type {
		get => ( EnemyTypeSO )_type;
		set => _type = value;
	}


	[Obsolete]
	public void Initialize() {
		//stats
		_statistics.SetFaction(Enemy);
		_statistics.StatusValues.InitValues(_type.baseStatusValues);

		//movement Position
		_movementController.movementPointsPerEnergy = _type.movementPointsPerEnergy;

		//Grid Position
		_gridTransform.gridPosition = Vector3Int.zero;

		// Equipment
		// maybe later

		//Abilities
		_abilityController.RefreshAbilities();
		_abilityController.BaseAbilities = _type.basicAbilities;
		_abilityController.damageInflicted = true;

		//model
		_modelController.prefab = _type.modelPrefab;
		_modelController.Initialize(null);
		_modelController.SetStandardHead(_type.headModel);
		_modelController.SetStandardBody(_type.bodyModel);
		_modelController.SetMeshHead(_type.headModel);
		_modelController.SetMeshBody(_type.bodyModel);
		if ( Type.weapon != null ) {
			_modelController.SetMeshRight(Type.weapon.mesh, Type.weapon.material);
		}
		else {
			_modelController.SetMeshRight(null, null);
		}

		_modelController.SetMeshLeft(null, null);
		_modelController.SetFactionMaterial(Enemy);

		//targetable
		_targetable.Initialise();

		//ai
		behavior = Type.behaviour;
		_aIController.SetBehavior(behavior);

		LootTable = Type.lootTable;
	}

	[Obsolete]
	public void InitializeFromSave(Enemy_Save saveData) {
		Initialize();
		_statistics.StatusValues.HitPoints.Value = saveData.hitpoints;
		_statistics.StatusValues.Energy.Value = saveData.energy;
		_gridTransform.gridPosition = saveData.pos;
	}

	public override EnemyCharacterData Save() {
		var data = base.Save();
		
		data.Name = Type.name + "-" + behavior.name;
		data.AiBehaviour = behavior;

		return data;
	}

	public override void Load(EnemyCharacterData data) {
		base.Load(data);

		_statistics.SetFaction(Enemy);

		//init ai
		behavior = data.AiBehaviour;
		_aIController.SetBehavior(behavior);
	}
}
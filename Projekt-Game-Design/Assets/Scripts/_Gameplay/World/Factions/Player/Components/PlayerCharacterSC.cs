using System;
using Characters;
using Characters.Types;
using FullSerializer;
using GDP01._Gameplay.World.Character.Components;
using GDP01._Gameplay.World.Character.Data;
using GDP01.Characters.Component;
using GDP01.Structure;
using GDP01.Structure.Provider;
using SaveSystem.SaveFormats;
using UnityEngine;


/// <summary>
/// attached to each playable character 
/// contains relevant data such as stats
/// </summary> 
public class PlayerCharacterSC : Character<PlayerCharacterSC, PlayerCharacterData> {

	[SerializeField] public PlayerTypeSO playerType;

	//todo move to some central location
	[SerializeField, fsIgnore] private Color playerColor;
	[SerializeField, fsIgnore] private Color friendlyColor;
	[SerializeField] private EquipmentController _equipmentController;

	[SerializeField] private string _locationName = String.Empty;
	[SerializeField] private bool _enteringNewLocation = false;
	[SerializeField] private int _connectorID = 0;
	
///// Properties ///////////////////////////////////////////////////////////////////////////////////
	
	public Statistics Statistics => _statistics;
	public EquipmentController EquipmentController => _equipmentController;
	
	//todo maybe dont use this here
	private LevelManager LevelManager => StructureProvider.Current.LevelManager;
	
	//locationAccess
	public string LocationName {
		get => _locationName;
		set => _locationName = value;
	}
	
	public int ConnectorId {
		get => _connectorID;
		set => _connectorID = value;
	}
	
	public bool EnterNewLocation {
		get => _enteringNewLocation;
		set => _enteringNewLocation = value;
	}
	
///// Private Functions ////////////////////////////////////////////////////////////////////////////	

	private bool initialized = false;

///// Public Functions /////////////////////////////////////////////////////////////////////////////	
	
	//todo remove
	public void Initialize() {
		id = 0;
		active = false;

		//stats
		_statistics.StatusValues.InitValues(playerType.baseStatusValues);
		_statistics.SetFaction(active ? Faction.Player : Faction.Friendly);
		_statistics.DisplayImage = playerType.icon;

		//movement Position
		_movementController.movementPointsPerEnergy = playerType.movementPointsPerEnergy;
		_movementController.movementCostPerTile = playerType.movementCostPerTile;

		//Grid Position
		_gridTransform.gridPosition = Vector3Int.zero;

		//model
		_modelController.prefab = playerType.modelPrefab;
		_modelController.Initialize(null);
		_modelController.SetStandardHead(playerType.headModel);
		_modelController.SetStandardBody(playerType.bodyModel);
		_modelController.SetFactionMaterial(_statistics.Faction);

		//Equipment
		_equipmentController.EquipmentID = id; // playerSpawnData.equipmentID;

		//Abilities
		_abilityController.BaseAbilities = playerType.basicAbilities;

		_healthbarController.SetColor(_statistics.Faction == Faction.Player
			? playerColor
			: friendlyColor);

		_targetable.Initialise();

		initialized = true;
	}

	//todo remove
	public void InitializeFromSave(PlayerCharacter_Save saveData) {
		Initialize();
		id = saveData.id;
		active = saveData.active;
		_statistics.StatusValues.HitPoints.Value = saveData.hitpoints;
		_statistics.StatusValues.Energy.Value = saveData.energy;
		_gridTransform.gridPosition = saveData.pos;
		_equipmentController.EquipmentID = saveData.id;

		_statistics.SetFaction(active ? Faction.Player : Faction.Friendly);
		_modelController.SetFactionMaterial(_statistics.Faction);
	}

	public void Activate() {
		active = true;
		_statistics.SetFaction(Faction.Player);
		_modelController.SetFactionMaterial(_statistics.Faction);
		_healthbarController.SetColor(playerColor);

		CharacterList characters = CharacterList.FindInstant();
		characters.friendlyContainer.Remove(gameObject);
		characters.playerContainer.Add(gameObject);
	}

	public override PlayerCharacterData Save() {
		//todo gather refgerences and transform data

		PlayerCharacterData data = base.Save();

		// data.LocationName = StructureProvider.Current.LevelManager

		data.LocationName = _locationName = LevelManager.CurrentLevel.name;
		
		
		return data; 
	}

	public override void Load(PlayerCharacterData data) {
		// Initialize();
		base.Load(data);
		
		//Equipment
		_equipmentController.EquipmentID = id;
		
		_healthbarController.SetColor(_statistics.Faction == Faction.Player
			? playerColor
			: friendlyColor);
		
		_statistics.SetFaction(active ? Faction.Player : Faction.Friendly);
		_modelController.SetFactionMaterial(_statistics.Faction);

		if ( data.LocationName == null || data.LocationName.Equals(String.Empty) ) {
			data.LocationName = LevelManager.CurrentLevel.name;
		}
		else {
			//todo deactivate char
		}
		
		// Debug.Log("Load PlayerCharacterSC");
		
		_equipmentController.RefreshEquipment();
	}
	
///// Unity Function ///////////////////////////////////////////////////////////////////////////////
}
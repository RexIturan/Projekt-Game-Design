using Characters;
using Characters.Movement;
using Characters.PlayerCharacter.ScriptableObjects;
using Characters.Types;
using Combat;
using GDP01.Characters.Component;
using GDP01.World.Components;
using SaveSystem.SaveFormats;
using UnityEngine;
using Visual.Healthbar;


/// <summary>
/// attached to each playable character 
/// contains relevant data such as stats
/// </summary> 
[System.Serializable]
public class PlayerCharacterSC : MonoBehaviour {
		public int id;
		public bool active;

    [Header("Basic Stats")]
    // Base stats
    public PlayerTypeSO playerType;
    // public PlayerSpawnDataSO playerSpawnData;

    [SerializeField] private Statistics _statistics;
    [SerializeField] private GridTransform _gridTransform;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private EquipmentController _equipmentController;
    [SerializeField] private AbilityController _abilityController;
    [SerializeField] private ModelController _modelController;
    [SerializeField] private Targetable _targetable; 
    
    [SerializeField] private HealthbarController _healthbarController;

    //todo move to some central location
    [SerializeField] private Color playerColor;
    [SerializeField] private Color friendlyColor;
    
    public void Initialize() {
			id = -1;
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
	    _modelController.Initialize();
	    _modelController.SetStandardHead(playerType.headModel);
	    _modelController.SetStandardBody(playerType.bodyModel);
	    _modelController.SetFactionMaterial(_statistics.Faction);
	    
	    //Equipment
	    _equipmentController.equipmentID = 0; // playerSpawnData.equipmentID;
	    
	    //Abilities
	    _abilityController.BaseAbilities = playerType.basicAbilities;
	    _abilityController.LastSelectedAbilityID = -1;
	    _abilityController.damageInflicted = true;
	    
	    _healthbarController.SetColor(_statistics.Faction == Faction.Player ? playerColor : friendlyColor);
	    
	    _targetable.Initialise();
    }
    
    public void InitializeFromSave(PlayerCharacter_Save saveData) {
	    Initialize();
	    id = saveData.id;
	    active = saveData.active;
	    _statistics.StatusValues.HitPoints.Value = saveData.hitpoints;
	    _statistics.StatusValues.Energy.Value = saveData.energy;
	    _gridTransform.gridPosition = saveData.pos;
	    _equipmentController.equipmentID = saveData.id;
	    
	    _statistics.SetFaction(active ? Faction.Player : Faction.Friendly);
	    _modelController.SetFactionMaterial(_statistics.Faction);
    }

		public void Start() {
			_equipmentController.RefreshEquipment();
		}

		public void Activate()
		{
				active = true;
				_statistics.SetFaction(Faction.Player);
				_modelController.SetFactionMaterial(_statistics.Faction);
				_healthbarController.SetColor(playerColor);

				CharacterList characters = CharacterList.FindInstant();
				characters.friendlyContainer.Remove(gameObject);
				characters.playerContainer.Add(gameObject);
		}
}

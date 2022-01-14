using Characters;
using Characters.Ability;
using Characters.Equipment;
using Characters.Movement;
using Characters.PlayerCharacter.ScriptableObjects;
using Combat;
using SaveSystem.SaveFormats;
using UnityEngine;


/// <summary>
/// attached to each playable character 
/// contains relevant data such as stats
/// </summary> 
[System.Serializable]
public class PlayerCharacterSC : MonoBehaviour {
		public bool active;

    [Header("Basic Stats")]
    // Base stats
    public PlayerTypeSO playerType;
    public PlayerSpawnDataSO playerSpawnData;

    [SerializeField] private Statistics _statistics;
    [SerializeField] private GridTransform _gridTransform;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private EquipmentController _equipmentController;
    [SerializeField] private AbilityController _abilityController;
    [SerializeField] private ModelController _modelController;
    
    public void Initialize() {
	    //stats
	    _statistics.StatusValues.InitValues(playerSpawnData.overrideStatusValues);
	    _statistics.SetFaction(active ? Faction.Player : Faction.Friendly);
	    
	    //movement Position
	    _movementController.movementPointsPerEnergy = playerType.movementPointsPerEnergy;

	    //Grid Position
	    _gridTransform.gridPosition = playerSpawnData.gridPos;

	    //model
	    _modelController.prefab = playerType.modelPrefab;
	    _modelController.Initialize();
	    _modelController.SetStandardHead(playerType.headModel);
	    _modelController.SetStandardBody(playerType.bodyModel);
			
	    _modelController.SetFactionMaterial(_statistics.Faction);
	    
	    //Equipment
	    _equipmentController.equipmentID = playerSpawnData.equipmentID;
	    
	    //Abilities
	    _abilityController.BaseAbilities = playerType.basicAbilities;
	    _abilityController.LastSelectedAbilityID = -1;
	    _abilityController.damageInflicted = true;
    }
    
    public void InitializeFromSave(PlayerCharacter_Save playerCharacterSave) {
	    Initialize();
	    active = playerCharacterSave.active;
	    _gridTransform.gridPosition = playerCharacterSave.pos;
	    _equipmentController.equipmentID = playerCharacterSave.equipmentInventoryId;
    }

		public void Start() {
			_equipmentController.RefreshEquipment();
		}

		public void Activate()
		{
				active = true;
				_statistics.SetFaction(Faction.Player);
				_modelController.SetFactionMaterial(_statistics.Faction);

				CharacterList characters = CharacterList.FindInstant();
				characters.friendlyContainer.Remove(gameObject);
				characters.playerContainer.Add(gameObject);
		}
}

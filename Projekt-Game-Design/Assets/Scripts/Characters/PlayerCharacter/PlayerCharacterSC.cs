using Characters;
using Characters.Ability;
using Characters.Equipment;
using Characters.Movement;
using Characters.PlayerCharacter.ScriptableObjects;
using Combat;
using UnityEngine;


/// <summary>
/// attached to each playable character 
/// contains relevant data such as stats
/// </summary> 
[System.Serializable]
public class PlayerCharacterSC : MonoBehaviour {
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
			_statistics.SetFaction(Faction.Player);
	    
	    //movement Position
	    _movementController.movementPointsPerEnergy = playerType.movementPointsPerEnergy;

	    //Grid Position
	    _gridTransform.gridPosition = playerSpawnData.gridPos;

	    //model
	    _modelController.prefab = playerType.modelPrefab;
	    
	    //Equipment
	    _equipmentController.equipmentID = playerSpawnData.equipmentID;
	    
	    //Abilities
	    _abilityController.RefreshAbilities();
	    _abilityController.BaseAbilities = playerType.basicAbilities;
    }

		public void Start() {
			_equipmentController.RefreshEquipment();
		}
}

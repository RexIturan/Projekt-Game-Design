using System.Collections.Generic;
using Characters.PlayerCharacter.ScriptableObjects;
using Events.ScriptableObjects;
using UnityEngine;
using Input;
using UnityEngine.InputSystem;
using Grid;
using Util;


/// <summary>
/// attached to each playable character 
/// contains relevant data such as stats
/// </summary> 
[System.Serializable]
public class PlayerCharacterSC : MonoBehaviour {
    [Header("Receiving events on")]
    public PathNodeEventChannelSO targetTileEvent;

    [Header("SO Reference")]
    public InputReader input;
    public GridDataSO globalGridData;
		public EquipmentContainerSO equipmentContainer;

    [Header("Basic Stats")]
    // Base stats
    public PlayerTypeSO playerType;
    public PlayerSpawnDataSO playerSpawnData;

    [Header("Current Max Stats")]
    // Stats influenced by status effects
    [SerializeField]
    private CharacterStats currentStats;

    public CharacterStats CurrentStats => currentStats;

    // TODO: implement status effects
    // <summary>stat changing temporary effects</summary>
    // [SerializeField] private List<ScriptableObject> statusEffects;

    public string playerName; 

    [Header("Current Stats")]
    // Leveling
    // TODO: maybe a more complex type later on
    public int level;
    public int experience;


    // Current values, dynamic
    public int healthPoints;
    public int energy;

    public int movementPointsPerEnergy; // Standardwert 20

    public Vector3Int gridPosition; // within the grid

    public int HealthPoints {
        get => healthPoints;
        set => healthPoints = value;
    }

    public int EnergyPoints {
        get => energy;
        set => energy = value;
    }

		[Header("Equipment")]
		public int equipmentID;

    [Header("Abilities")]
		[SerializeField] private AbilitySO[] abilities;
    [SerializeField] private int abilityID;

    public AbilitySO[] Abilities => abilities;

    public int AbilityID {
        get => abilityID;
        set => abilityID = value;
    }

		// for playing animations
		public float movementSpeed; // Standardwert 5
		private CharacterAnimationController animationController;

	  public CharacterAnimationController GetAnimationController() { return animationController; }

    // Statemachine
    //
    [Header("State Machine")] 
    public bool isSelected;
    public bool abilitySelected;
    public bool abilityConfirmed;
    public bool abilityExecuted;

    [Header("Movement Chache")]
    // cached target (tile position)
    public PathNode movementTarget;
    public List<PathNode> reachableTiles;
    public bool movementDone = true;

    [Header("Combat Chache")]
    // cached target (player or enemy)
    public PlayerCharacterSC playerTarget;
    public EnemyCharacterSC enemyTarget;
    public List<PathNode> tilesInRange;
    public bool waitForAttackToFinish = false;

    [Header("Timer")]
    public float timeSinceTransition;

    private void Awake() {
        input.MouseClicked += ToggleIsSelected;
        targetTileEvent.OnEventRaised += TargetTile;
    }

    private void OnDisable() {
        input.MouseClicked -= ToggleIsSelected;
        targetTileEvent.OnEventRaised -= TargetTile;
    }

    public void Start() {
				movementPointsPerEnergy = 20;
				movementSpeed = 5f;
				facingDirection = 0f;
				// set position of gameobject    
				TransformToPosition();
				// create model
				GameObject model = Instantiate(playerType.model, transform);
				// save animation controller
				animationController = model.GetComponent<CharacterAnimationController>();
    }

		// TODO: !!DEBUG!!
		[Header("DEBUG: ")]

		public float facingDirection;
		public Vector3 position;
		public StanceType stance;
		public WeaponPositionType weaponRightPosition;
		// !!!!!!!!!!!!
		public void FixedUpdate() {
        timeSinceTransition += Time.fixedDeltaTime;
				// DEBUG!!!!!!!
				animationController.TakeStance(stance);
				RefreshEquipment();
				RefreshAbilities();
				animationController.ChangeWeaponPosition(EquipmentType.RIGHT, weaponRightPosition);
				// !!!!!!!!!!!

				// Move character to position smoothly 
				transform.position = Vector3.MoveTowards(transform.position, position, movementSpeed * Time.deltaTime);

				// Rotate character to facing position smoothly
				Quaternion target = Quaternion.Euler(0, facingDirection, 0);
				float rotationSpeed = 360.0f;
				transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * rotationSpeed);
		}

    void ToggleIsSelected() {
        if (!abilitySelected && !abilityConfirmed)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100.0f))
            {
                if (rayHit.collider.gameObject == gameObject)
                {
                    isSelected = !isSelected;
                }
            }
        }
    }

		public void Update()
		{
		}

    // transforms the gameobject to it's tile position
    public void TransformToPosition() {
        var pos = gridPosition + globalGridData.GetCellCenter();
        pos *= globalGridData.CellSize;
        pos += globalGridData.OriginPosition;

        position = pos;
    }

		public void FaceDirection(float direction)
		{
				facingDirection = direction;
		}

		public void FaceMovingDirection()
		{
				float minDifference = 0.1f; // the point that's being moved to 
																		// has to be at least this far away from the player

				Vector3 movingDirection = position - gameObject.transform.position;
				if ( movingDirection.magnitude > minDifference )
				{
						float angle = Vector3.Angle(new Vector3(0, 0, 1), movingDirection);
						if ( movingDirection.x < 0 )
						{
								// mirror angle
								angle = -angle + 360;
						}

						facingDirection = angle;
				}
		}

    // target Tile
    public void TargetTile(PathNode target) {
    }

    public int GetEnergyUseUpFromMovement() {
        return Mathf.CeilToInt((float) movementTarget.dist / movementPointsPerEnergy);
    }

    public int GetMaxMoveDistance() {
        return energy * movementPointsPerEnergy;
    }
    
    public void Refill() {
        // refill energy etc.
        energy = currentStats.maxEnergy;
    }

    public void RefreshAbilities()
    {
        List<AbilitySO> currentAbilities = new List<AbilitySO>(playerType.basicAbilities);

				// abilities from items
				List<ItemSO> items = equipmentContainer.GetEquippedItems(equipmentID);
				if ( items != null )
				{
						foreach ( ItemSO item in items )
						{
								if ( item != null && item.abilities != null )
										foreach ( AbilitySO ability in item.abilities )
												if ( !currentAbilities.Contains(ability) )
														currentAbilities.Add(ability);
						}
				}
				
        abilities = currentAbilities.ToArray();
    }

    public void Initialize() {
        level = playerSpawnData.level;
        experience = playerSpawnData.experience;
        healthPoints = playerType.stats.maxHealthPoints;
        energy = playerType.stats.maxEnergy;
        movementPointsPerEnergy = playerSpawnData.movementPointsPerEnergy;
        currentStats = playerType.stats;

        RefreshAbilities();
        RefreshEquipment();
    }

		private void RefreshEquipment() {
				List<ItemSO> items = equipmentContainer.GetEquippedItems(equipmentID);

				// change models
				//
				if ( animationController )
				{
						if(items.Count >= 1)
							animationController.ChangeWeapon(EquipmentType.LEFT, items[(int) EquipmentPosition.LEFT]?.model);
						else
							animationController.ChangeWeapon(EquipmentType.LEFT, null);

						if (items.Count >= 2)
							animationController.ChangeWeapon(EquipmentType.RIGHT, items[(int) EquipmentPosition.RIGHT]?.model);
						else
								animationController.ChangeWeapon(EquipmentType.RIGHT, null);
				}
		}
}

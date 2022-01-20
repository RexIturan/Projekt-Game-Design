using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Characters;
using GDP01.Characters.Component;

[CreateAssetMenu(fileName = "p_EquipActiveWeapon_OnEnter",
	menuName = "State Machines/Actions/Player/Equip Active Weapon On Enter")]
public class P_EquipActiveWeapon_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new P_EquipActiveWeapon_OnEnter();
}

public class P_EquipActiveWeapon_OnEnter : StateAction {
	private ModelController _modelController;
	private AbilityController _abilityController;
	private EquipmentController _equipmentController;

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_modelController = stateMachine.gameObject.GetComponent<ModelController>();
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_equipmentController = stateMachine.gameObject.GetComponent<EquipmentController>();
	}

	public override void OnStateEnter() {
		// Set active hands for the animation of the ability
		//
		switch(_abilityController.GetSelectedAbility().Animation) {
			case(CharacterAnimation.CAST_A):
				_equipmentController.SetActiveHands(EquipmentController.ActiveEquipmentPosition.LEFT);
				break;
			default:
				_equipmentController.SetActiveHands(EquipmentController.ActiveEquipmentPosition.RIGHT);
				break;
		}

		// If the ability comes from the ability on the right, set right weapon active. 
		// If it comes from the weapon on the left, set left weapon active
		//
		WeaponSO weaponRight = _equipmentController.GetWeaponRight();
		bool rightContainsAbility = false;

		if(weaponRight) { 
			foreach(AbilitySO rightAbility in weaponRight.abilities) {
				if(rightAbility.id == _abilityController.SelectedAbilityID)
					rightContainsAbility = true;
			}
		}

		if(rightContainsAbility) {
			_equipmentController.SetActiveWeapon(EquipmentController.ActiveEquipmentPosition.RIGHT);
		}
		else { 
			WeaponSO weaponLeft = _equipmentController.GetWeaponLeft();
			bool leftContainsAbility = false;

			if(weaponLeft) { 
				foreach(AbilitySO leftAbility in weaponLeft.abilities) {
					if(leftAbility.id == _abilityController.SelectedAbilityID)
						leftContainsAbility = true;
				}
			}

			if(leftContainsAbility) {
			_equipmentController.SetActiveWeapon(EquipmentController.ActiveEquipmentPosition.LEFT);
			}
			else {
			_equipmentController.SetActiveWeapon(0);
			}
		}

		_equipmentController.RefreshModels();
	  _equipmentController.RefreshWeaponPositions();
	}
}
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Characters;
using Characters.Ability;
using Characters.Equipment;

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
		// If the ability comes from the ability on the right, do nothing. 
		// If it comes from the weapon on the left, switch weapons
		WeaponSO weaponRight = _equipmentController.GetWeaponRight();
		bool rightContainsAbility = false;

		if(weaponRight) { 
			foreach(AbilitySO rightAbility in weaponRight.abilities) {
				if(rightAbility.abilityID == _abilityController.SelectedAbilityID)
					rightContainsAbility = true;
			}
		}

		if(rightContainsAbility) {
			_equipmentController.ActivateRight();
		}
		else { 
			WeaponSO weaponLeft = _equipmentController.GetWeaponLeft();
			bool leftContainsAbility = false;

			if(weaponLeft) { 
				foreach(AbilitySO leftAbility in weaponLeft.abilities) {
					if(leftAbility.abilityID == _abilityController.SelectedAbilityID)
						leftContainsAbility = true;
				}
			}

			if(leftContainsAbility) {
				_equipmentController.ActivateLeft();
			}
		}
	}
}
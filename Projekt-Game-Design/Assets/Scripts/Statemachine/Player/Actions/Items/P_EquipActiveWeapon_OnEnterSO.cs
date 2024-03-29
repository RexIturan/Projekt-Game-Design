using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Characters;
using GDP01.Characters.Component;
using static EquipmentPosition;

[CreateAssetMenu(fileName = "p_EquipActiveWeapon_OnEnter",
	menuName = "State Machines/Actions/Player/Equip Active Weapon On Enter")]
public class P_EquipActiveWeapon_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new P_EquipActiveWeapon_OnEnter();
}

public class P_EquipActiveWeapon_OnEnter : StateAction {
	private AbilityController _abilityController;
	private EquipmentController _equipmentController;

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_equipmentController = stateMachine.gameObject.GetComponent<EquipmentController>();
	}

	public override void OnStateEnter() {
		// Set active hands for the animation of the ability
		switch(_abilityController.GetSelectedAbility().Animation) {
			case(CharacterAnimation.CAST_A):
				_equipmentController.SetActiveHands(LEFT);
				break;
			default:
				_equipmentController.SetActiveHands(RIGHT);
				break;
		}

		// If the ability comes from the ability on the right, set right weapon active. 
		// If it comes from the weapon on the left, set left weapon active
		WeaponTypeSO weaponTypeRight = _equipmentController.RightWeaponType;
		bool rightContainsAbility = false;

		if(weaponTypeRight) { 
			foreach(AbilitySO rightAbility in weaponTypeRight.abilities) {
				if(rightAbility.id == _abilityController.SelectedAbilityID)
					rightContainsAbility = true;
			}
		}

		WeaponTypeSO weaponTypeLeft = _equipmentController.LeftWeaponType;
		bool leftContainsAbility = false;

		if(weaponTypeLeft != null) { 
			foreach(AbilitySO leftAbility in weaponTypeLeft.abilities) {
				if(leftAbility.id == _abilityController.SelectedAbilityID)
					leftContainsAbility = true;
			}
		}

		_equipmentController.SetActiveWeapon(leftContainsAbility ? LEFT : RIGHT);
		
		_equipmentController.RefreshModels();
	  _equipmentController.RefreshWeaponPositions();
	}
}
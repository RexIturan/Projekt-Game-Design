using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static EquipmentPosition;

/**
 * attached to equipment game object in character model 
 * to change weapon positions and models/textures
 */
public class CharacterEquipmentController : MonoBehaviour {
	
	[Serializable]
	private class ConstraintWrapper {
		[SerializeField] private MultiParentConstraint constraint;
		[SerializeField] private List<WeaponPositionType> positions;
		private Dictionary<WeaponPositionType, int> mappingDict;

		public MultiParentConstraint Constraint => constraint;
		public Dictionary<WeaponPositionType, int> MappingDict => mappingDict;

		public void Init() {
			mappingDict = new Dictionary<WeaponPositionType, int>();
			for ( int i = 0; i < positions.Count; i++ ) {
				mappingDict.Add(positions[i], i);
			}
		}

		public void ActivateConstraint(WeaponPositionType weaponPosition) {
			foreach ( var mapping in mappingDict ) {
				constraint.data.sourceObjects.SetWeight(mapping.Value, mapping.Key == weaponPosition ? 1 : 0);
			}
		}
	}

	[SerializeField] private GameObject WeaponLeftObject;
	[SerializeField] private GameObject WeaponRightObject;
	[SerializeField] private GameObject ShieldObject;

	[SerializeField] private GameObject WeaponLeftModel;
	[SerializeField] private GameObject WeaponRightModel;
	[SerializeField] private GameObject ShieldModel;

	[SerializeField] private ConstraintWrapper WeaponRightConstraint;
	[SerializeField] private ConstraintWrapper WeaponLeftConstraint;
	[SerializeField] private ConstraintWrapper ShildConstraint;

	private void SetWeaponConstraint(EquipmentPosition position, WeaponPositionType weaponPosition) {
		switch ( position ) {
			case LEFT:
				WeaponLeftConstraint.ActivateConstraint(weaponPosition);
				break;
			case RIGHT:
				WeaponRightConstraint.ActivateConstraint(weaponPosition);
				break;
			case SHIELD:
				ShildConstraint.ActivateConstraint(weaponPosition);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(position), position, null);
		}
	}
	
	public void ChangeEquipment(EquipmentPosition position, Mesh newWeapon, Material material) {
		//todo cache filter and renderer?
		//todo refactor setting those things
		
		switch ( position ) {
			case LEFT:
				WeaponLeftModel.GetComponent<MeshFilter>().mesh = newWeapon;
				WeaponLeftModel.GetComponent<MeshRenderer>().material = material;
				break;
			case RIGHT:
				WeaponRightModel.GetComponent<MeshFilter>().mesh = newWeapon;
				WeaponRightModel.GetComponent<MeshRenderer>().material = material;
				break;
			case SHIELD:
				ShieldModel.GetComponent<MeshFilter>().mesh = newWeapon;
				ShieldModel.GetComponent<MeshRenderer>().material = material;
				break;
			case BODY:
			case HEAD:
			default:
				throw new ArgumentOutOfRangeException(nameof(position), position, null);
		}
	}

	public void ChangeWeaponPosition(EquipmentPosition position, WeaponPositionType weaponPosition) {
		SetWeaponConstraint(position, weaponPosition);
		
		// MultiParentConstraint[] constraints = gameObject.GetComponentsInChildren<MultiParentConstraint>();
		// foreach ( MultiParentConstraint constraint in constraints )
		// {
		// 	if ( constraint.tag.Equals(GetTagForEquipmentType(position)) )
		// 	{
		// 		if ( position.Equals(LEFT) || position.Equals(RIGHT) )
		// 		{
		// 			WeightedTransform weightEquipped = new WeightedTransform(constraint.data.sourceObjects.GetTransform(0), 0f);
		// 			WeightedTransform weightBackUpwards = new WeightedTransform(constraint.data.sourceObjects.GetTransform(1), 0f);
		// 			WeightedTransform weightBackDownwards = new WeightedTransform(constraint.data.sourceObjects.GetTransform(2), 0f);
		//
		// 			switch ( newPosition )
		// 			{
		// 				case ( WeaponPositionType.EQUIPPED ):
		// 					weightEquipped.weight = 1.0f;
		// 					break;
		// 				case ( WeaponPositionType.BACK_UPWARDS ):
		// 					weightBackUpwards.weight = 1.0f;
		// 					break;
		// 				case ( WeaponPositionType.BACK_DOWNWARDS ):
		// 					weightBackDownwards.weight = 1.0f;
		// 					break;
		// 				default:
		// 					Debug.LogWarning("Invalid position for weapons. ");
		// 					break;
		// 			}
		//
		// 			WeightedTransformArray newWeights = new WeightedTransformArray();
		// 			newWeights.Add(weightEquipped);
		// 			newWeights.Add(weightBackUpwards);
		// 			newWeights.Add(weightBackDownwards);
		// 			constraint.data.sourceObjects = newWeights;
		// 		}
		// 		else if ( position.Equals(SHIELD) )
		// 		{
		// 			WeightedTransformArray sourceObjects = constraint.data.sourceObjects;
		// 			// NOTE: the order of source objects in character model is important
		// 			sourceObjects.SetWeight(0, 0f);
		// 			sourceObjects.SetWeight(1, 0f);
		//
		// 			switch ( newPosition )
		// 			{
		// 				case ( WeaponPositionType.EQUIPPED ):
		// 					sourceObjects.SetWeight(0, 1.0f);
		// 					break;
		// 				case ( WeaponPositionType.BACK ):
		// 					sourceObjects.SetWeight(1, 1.0f);
		// 					break;
		// 				default:
		// 					Debug.LogWarning("Invalid position for shields. ");
		// 					break;
		// 			}
		// 		}
		// 	}
		// }
	}

	public void DisableEquipment(EquipmentPosition position, bool disable) {
		switch ( position ) {
			case LEFT:
				WeaponLeftObject.SetActive(!disable);
				break;
			case RIGHT:
				WeaponRightObject.SetActive(!disable);
				break;
			case SHIELD:
				ShieldObject.SetActive(!disable);
				break;
			case HEAD:
			case BODY:
			default:
				throw new ArgumentOutOfRangeException(nameof(position), position, null);
		}
	}

	private void Awake() {
		WeaponRightConstraint.Init();
		WeaponLeftConstraint.Init();
		ShildConstraint.Init();
	}
}

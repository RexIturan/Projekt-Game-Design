using UnityEngine;
using UnityEngine.Animations.Rigging;

/**
 * attached to equipment game object in character model 
 * to change weapon positions and models/textures
 */
public class CharacterEquipmentController : MonoBehaviour
{
	public void ChangeEquipment(EquipmentPosition position, Mesh newWeapon)
	{
		MeshFilter[] meshes = gameObject.GetComponentsInChildren<MeshFilter>();
		foreach ( MeshFilter mesh in meshes )
		{
			if ( mesh.tag.Equals(GetTagForEquipmentType(position)) )
				mesh.mesh = newWeapon;
		}
	}

	public void ChangeWeaponPosition(EquipmentPosition position, WeaponPositionType newPosition)
	{
		MultiParentConstraint[] constraints = gameObject.GetComponentsInChildren<MultiParentConstraint>();
		foreach ( MultiParentConstraint constraint in constraints )
		{
			if ( constraint.tag.Equals(GetTagForEquipmentType(position)) )
			{
				if ( position.Equals(EquipmentPosition.LEFT) || position.Equals(EquipmentPosition.RIGHT) )
				{
					WeightedTransform weightEquipped = new WeightedTransform(constraint.data.sourceObjects.GetTransform(0), 0f);
					WeightedTransform weightBackUpwards = new WeightedTransform(constraint.data.sourceObjects.GetTransform(1), 0f);
					WeightedTransform weightBackDownwards = new WeightedTransform(constraint.data.sourceObjects.GetTransform(2), 0f);

					switch ( newPosition )
					{
						case ( WeaponPositionType.EQUIPPED ):
							weightEquipped.weight = 1.0f;
							break;
						case ( WeaponPositionType.BACK_UPWARDS ):
							weightBackUpwards.weight = 1.0f;
							break;
						case ( WeaponPositionType.BACK_DOWNWARDS ):
							weightBackDownwards.weight = 1.0f;
							break;
						default:
							Debug.LogWarning("Invalid position for weapons. ");
							break;
					}

					WeightedTransformArray newWeights = new WeightedTransformArray();
					newWeights.Add(weightEquipped);
					newWeights.Add(weightBackUpwards);
					newWeights.Add(weightBackDownwards);
					constraint.data.sourceObjects = newWeights;
				}
				else if ( position.Equals(EquipmentPosition.SHIELD) )
				{
					WeightedTransformArray sourceObjects = constraint.data.sourceObjects;
					// NOTE: the order of source objects in character model is important
					sourceObjects.SetWeight(0, 0f);
					sourceObjects.SetWeight(1, 0f);

					switch ( newPosition )
					{
						case ( WeaponPositionType.EQUIPPED ):
							sourceObjects.SetWeight(0, 1.0f);
							break;
						case ( WeaponPositionType.BACK ):
							sourceObjects.SetWeight(1, 1.0f);
							break;
						default:
							Debug.LogWarning("Invalid position for shields. ");
							break;
					}
				}
			}
		}
	}

	public void DisableEquipment(EquipmentPosition position, bool disable)
	{
		MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		foreach ( MeshRenderer renderer in renderers )
		{
			if ( renderer.tag.Equals(GetTagForEquipmentType(position)) )
				renderer.enabled = !disable;
		}
	}

	private string GetTagForEquipmentType(EquipmentPosition equipment)
	{
		string tag = "UnknownEquipment";
		switch ( equipment )
		{
			case EquipmentPosition.RIGHT:
				tag = "Equipment/WeaponR";
				break;
			case EquipmentPosition.LEFT:
				tag = "Equipment/WeaponL";
				break;
			case EquipmentPosition.SHIELD:
				tag = "Equipment/Shield";
				break;
		}
		return tag;
	}
}

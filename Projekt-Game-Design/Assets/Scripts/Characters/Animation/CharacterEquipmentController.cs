using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

/**
 * attached to equipment game object in character model 
 * to change weapon positions and models/textures
 */
public class CharacterEquipmentController : MonoBehaviour
{
	public void ChangeWeapon(EquipmentType type, Mesh newWeapon)
	{
		MeshFilter[] meshes = gameObject.GetComponentsInChildren<MeshFilter>();
		foreach ( MeshFilter mesh in meshes )
		{
			if ( mesh.tag.Equals(GetTagForEquipmentType(type)) )
				mesh.mesh = newWeapon;
		}
	}

	public void ChangeWeaponPosition(EquipmentType type, WeaponPositionType newPosition)
	{
		MultiParentConstraint[] constraints = gameObject.GetComponentsInChildren<MultiParentConstraint>();
		foreach ( MultiParentConstraint constraint in constraints )
		{
			if ( constraint.tag.Equals(GetTagForEquipmentType(type)) )
			{
				if ( type.Equals(EquipmentType.LEFT) || type.Equals(EquipmentType.RIGHT) )
				{
					WeightedTransformArray sourceObjects = constraint.data.sourceObjects;
					// NOTE: the order of source objects in character model is important
					sourceObjects.SetWeight(0, 0f);
					sourceObjects.SetWeight(1, 0f);
					sourceObjects.SetWeight(2, 0f);

					switch ( newPosition )
					{
						case ( WeaponPositionType.EQUIPPED ):
							sourceObjects.SetWeight(0, 1.0f);
							break;
						case ( WeaponPositionType.BACK_UPWARDS ):
							sourceObjects.SetWeight(1, 1.0f);
							break;
						case ( WeaponPositionType.BACK_DOWNWARDS ):
							sourceObjects.SetWeight(2, 1.0f);
							break;
						default:
							Debug.LogWarning("Ungueltige Position fuer Waffen. ");
							break;
					}
				}
				else if ( type.Equals(EquipmentType.SHIELD) )
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
							Debug.LogWarning("Ungueltige Position fuer Schilde. ");
							break;
					}
				}
			}
		}
	}

	public void DisableEquipment(EquipmentType type, bool disable)
	{
		MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		foreach ( MeshRenderer renderer in renderers )
		{
			if ( renderer.tag.Equals(GetTagForEquipmentType(type)) )
				renderer.enabled = !disable;
		}
	}

	private string GetTagForEquipmentType(EquipmentType equipment)
	{
		string tag = "UnknownEquipment";
		switch ( equipment )
		{
			case EquipmentType.RIGHT:
				tag = "Equipment/WeaponR";
				break;
			case EquipmentType.LEFT:
				tag = "Equipment/WeaponL";
				break;
			case EquipmentType.SHIELD:
				tag = "Equipment/Shield";
				break;
			case EquipmentType.QUIVER:
				tag = "Equipment/Quiver";
				break;
		}
		return tag;
	}
}

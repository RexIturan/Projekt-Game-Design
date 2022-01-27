using System;
using UnityEngine;

/**
 * changes body and head meshes etc.
 */
public class CharacterModelController : MonoBehaviour {
	
	//todo move to charquipmentcointroller or the other way around
	
	[SerializeField] private Mesh standardHead;
	[SerializeField] private Mesh standardBody;

	[SerializeField] private SkinnedMeshRenderer HeadMesh;
	[SerializeField] private SkinnedMeshRenderer BodyMesh;

	public void SetHeadMaterial(Material material) {
		HeadMesh.material = material;
	}

	public void SetBodyMaterial(Material material) {
		BodyMesh.material = material;
	}
	
	public void ChangeEquipment(EquipmentPosition position, Mesh newArmorPiece) {
		Mesh newMesh;
		switch ( position ) {
			case EquipmentPosition.HEAD:
				newMesh = newArmorPiece ?? standardHead;
				HeadMesh.sharedMesh = newMesh;
				break;
			case EquipmentPosition.BODY:
				newMesh = newArmorPiece ?? standardBody;
				BodyMesh.sharedMesh = newMesh;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(position), position, null);
		}
	}

	public void SetStandardHead(Mesh mesh) {
		standardHead = mesh;
	}

	public void SetStandardBody(Mesh mesh) {
		standardBody = mesh;
	}
}

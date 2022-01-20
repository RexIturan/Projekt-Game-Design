using UnityEngine;

/**
 * changes body and head meshes etc.
 */
public class CharacterModelController : MonoBehaviour
{
	public Mesh standardHead;
	public Mesh standardBody;

	[SerializeField] private SkinnedMeshRenderer HeadMesh;
	[SerializeField] private SkinnedMeshRenderer BodyMesh;

	public void SetHeadMaterial(Material material) {
		HeadMesh.material = material;
	}

	public void SetBodyMaterial(Material material) {
		BodyMesh.material = material;
	}
	
	public void ChangeEquipment(EquipmentPosition position, Mesh newArmorPiece)
	{
		SkinnedMeshRenderer[] meshes = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		string tag = GetTagForEquipmentType(position);
		
		foreach ( SkinnedMeshRenderer mesh in meshes )
		{
			if ( mesh.tag.Equals(tag) ) { 
				if(newArmorPiece)
					mesh.sharedMesh = newArmorPiece;
				else
					mesh.sharedMesh = position.Equals(EquipmentPosition.HEAD) ? standardHead : standardBody;
			}
		}
	}

	private string GetTagForEquipmentType(EquipmentPosition equipment)
	{
		string tag = "UnknownEquipment";
		switch ( equipment )
		{
			case EquipmentPosition.HEAD:
				tag = "Equipment/Head";
				break;
			case EquipmentPosition.BODY:
				tag = "Equipment/Body";
				break;
		}
		return tag;
	}

	public void SetStandardHead(Mesh mesh) {
		standardHead = mesh;
	}

	public void SetStandardBody(Mesh mesh) {
		standardBody = mesh;
	}
}

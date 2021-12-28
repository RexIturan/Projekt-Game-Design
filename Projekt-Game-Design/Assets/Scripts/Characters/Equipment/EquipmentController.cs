using UnityEngine;

namespace Characters.Equipment
{
		public class EquipmentController : MonoBehaviour
		{
				[SerializeField] private EquipmentContainerSO equipmentContainer;
				public int equipmentID;

				public void RefreshEquipment()
				{
						ModelController modelController = gameObject.GetComponent<ModelController>();

						if(modelController)
						{
								ItemSO itemLeft = equipmentContainer.GetItemLeft(equipmentID);
								ItemSO itemRight = equipmentContainer.GetItemRight(equipmentID);

								modelController.SetMeshLeft(itemLeft ? itemLeft.mesh : null);
								modelController.SetMeshRight(itemRight ? itemRight.mesh : null);
						}
				}
		}
}
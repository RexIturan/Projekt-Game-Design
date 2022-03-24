using UnityEngine;

namespace WorldObjects {

		/// Contains data to set in door game object
		[CreateAssetMenu(fileName = "newDoorType", menuName = "WorldObjects/Door/New Door Type")]
		public class DoorTypeSO : WorldObject.TypeSO {
				// The front face of the door should face the positive z-Axis vector
				public GameObject model;
				public string doorName;
				public string description;
				
				public string destructionSound;
				public string openingSound;
				public bool openManually;
				public bool destructable;
				public int hitPoints;

				public Door.DoorData ToComponentData() {
					Door.DoorData data = base.ToComponentData<Door.DoorData>();
		
					// playerData.EquipmentId = equipmentID;
					data.ReferenceData = ToReferenceData();
		
					return data;
				}
		}
}
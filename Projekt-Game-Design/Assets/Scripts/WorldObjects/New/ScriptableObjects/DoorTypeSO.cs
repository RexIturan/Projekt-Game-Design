using UnityEngine;

namespace WorldObjects
{
		/**
		 * Contains data to set in door game object
		 */
		[CreateAssetMenu(fileName = "newDoorType", menuName = "WorldObjects/Door/New Door Type")]
		public class DoorTypeSO : ScriptableObject
		{
				public int id;
				public GameObject prefab;
				// The front face of the door should face the positive z-Axis vector
				public GameObject model;
				public string doorName;
				public string description;
				public string destructionSound;
				public string openingSound;
				public bool openManually;
				public bool destructable;
				public int hitPoints;
		}
}
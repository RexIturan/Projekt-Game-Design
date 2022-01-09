using UnityEngine;

namespace WorldObjects
{
		/**
		 * Contains data to set in door game object
		 */
		[CreateAssetMenu(fileName = "newDoor", menuName = "WorldObjects/Door/New Door")]
		public class DoorSO : ScriptableObject
		{
				public int id;
				public GameObject prefab;
				public GameObject model;
				public string doorName;
				public string description;
				public bool destructable;
				public int hitPoints;
		}
}
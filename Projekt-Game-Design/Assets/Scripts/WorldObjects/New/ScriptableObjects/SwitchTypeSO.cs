using UnityEngine;

namespace WorldObjects
{
		/**
		 * Contains data to set in switch game object
		 */
		[CreateAssetMenu(fileName = "newSwitchType", menuName = "WorldObjects/Switch/New Switch Type")]
		public class SwitchTypeSO : ScriptableObject
		{
				public int id;
				public GameObject prefab;
				public GameObject model;
				public string switchName;
				public string description;
				public int range;
				public bool walkThrough;
		}
}
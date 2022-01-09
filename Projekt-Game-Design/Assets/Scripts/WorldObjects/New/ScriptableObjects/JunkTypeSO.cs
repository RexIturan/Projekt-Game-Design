using UnityEngine;

namespace WorldObjects
{
		/**
		 * Contains data to set in junk game object
		 */
		[CreateAssetMenu(fileName = "newJunkType", menuName = "WorldObjects/Junk/New Junk Type")]
		public class JunkTypeSO : ScriptableObject
		{
				public int id;
				public GameObject prefab;
				public GameObject model;
				public string junkName;
				public string description;
				public bool walkThrough;
				public bool destructable;
				public int hitPoints;
				public LootTable drop;
		}
}
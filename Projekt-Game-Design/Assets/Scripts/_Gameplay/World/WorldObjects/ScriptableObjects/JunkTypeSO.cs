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
				public string destructionSound;
				public bool walkThrough;
				public bool destructable;
				public int hitPoints;
				//todo(vincent) refactor
				// public LootTable drop;
		}
}
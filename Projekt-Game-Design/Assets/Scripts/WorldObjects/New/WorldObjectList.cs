using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
		public class WorldObjectList : MonoBehaviour
		{
				public List<GameObject> doors;
				public List<GameObject> switches;
				[SerializeField] private List<GameObject> trigger;
				public List<GameObject> junks;

				public static WorldObjectList FindWorldObjectList()
				{
						GameObject worldObjectList = GameObject.Find("WorldObjects");
						if ( worldObjectList )
								return worldObjectList.GetComponent<WorldObjectList>();
						else
								return null;
				}
		}
}
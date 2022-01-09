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
				[SerializeField] private List<GameObject> objects;

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
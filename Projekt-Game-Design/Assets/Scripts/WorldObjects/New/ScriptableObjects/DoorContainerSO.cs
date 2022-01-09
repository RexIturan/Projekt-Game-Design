using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
		[CreateAssetMenu(fileName = "DoorContainer", menuName = "WorldObjects/Door/Door Container")]
		public class DoorContainerSO : ScriptableObject
		{
				public List<DoorSO> doors;
		}
}
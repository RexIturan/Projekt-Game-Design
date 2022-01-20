using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
		[CreateAssetMenu(fileName = "JunkContainer", menuName = "WorldObjects/Junk/Junk Container")]
		public class JunkContainerSO : ScriptableObject
		{
				public List<JunkTypeSO> junks;
		}
}
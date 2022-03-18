using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
		[CreateAssetMenu(fileName = "SwitchContainer", menuName = "WorldObjects/Switch/Switch Container")]
		public class SwitchContainerSO : ScriptableObject
		{
				public List<SwitchTypeSO> switches;
		}
}
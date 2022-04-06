using System.Collections.Generic;
using UnityEngine;

namespace _Gameplay.Environment.FogOfWar.FogOfWarV2.Types {
	[CreateAssetMenu(fileName = "ViewCache", menuName = "FogOfWar/View Cache", order = 0)]
	public class ViewCacheSO : ScriptableObject {
		public List<string> view;
	}
}
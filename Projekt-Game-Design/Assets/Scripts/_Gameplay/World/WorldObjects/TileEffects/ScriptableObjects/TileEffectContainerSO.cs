using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDP01.TileEffects
{
		[CreateAssetMenu(fileName = "TileEffectContainer", menuName = "WorldObjects/Tile Effect Container")]
    public class TileEffectContainerSO : ScriptableObject
    {
				public List<GameObject> tileEffects;
		}
}

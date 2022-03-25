using UnityEngine;

namespace SaveSystem.SaveFormats
{
		[System.Serializable]
		public class TileEffect_Save
		{
				public int prefabID;
				public Vector3Int position;
				public int timeUntilActivation;
				public int timeToLive;
		}
}
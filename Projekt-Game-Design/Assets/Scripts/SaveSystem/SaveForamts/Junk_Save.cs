using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.SaveFormats
{
		[System.Serializable]
		public class Junk_Save
		{
				public int junkTypeId;
				public Vector3Int gridPos;
				public bool broken;
				public int currentHitPoints;
		}
}
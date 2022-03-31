using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.SaveFormats
{
		[System.Serializable]
		public class Door_Save {
			
				public int doorTypeId;
				public Vector3Int gridPos;
				public Vector3 orientation;
				public bool open;
				public List<int> keyIds;
				public List<int> switchIds;
				public List<int> triggerIds;
				public List<int> remainingSwitchIds;
				public List<int> remainingTriggerIds;
				public int currentHitPoints;
		}
}
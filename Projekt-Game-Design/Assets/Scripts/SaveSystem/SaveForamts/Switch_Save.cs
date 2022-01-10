using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.SaveFormats
{
		[System.Serializable]
		public class Switch_Save
		{
				public int switchId;
				public int switchTypeId;
				public Vector3Int gridPos;
				public Vector3 orientation;
		}
}
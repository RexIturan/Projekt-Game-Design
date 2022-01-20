using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem.SaveFormats
{
		[System.Serializable]
		public class Quest_Save
		{
				public int questId;
				
				public bool active;

				public int currentTaskIndex;
		}
}
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// stores the equipment of a player character
/// references the player inventory and saves IDs within it
/// </summary>
[System.Serializable]
public class Equipment {
		// first is weapon left
		// second is weapon right
		public List<int> items;

		public Equipment()
		{
				items = new List<int>();
				items.Add(-1);
				items.Add(-1);
		}
}
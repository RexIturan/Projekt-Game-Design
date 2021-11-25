using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// stores the equipment of a player character
/// references the player inventory and saves IDs within it
/// </summary>
[System.Serializable]
public class Equipment {
		public int itemLeftID;
		public int itemRightID;

		public List<int> equipmentToList()
		{
				List<int> itemIDs = new List<int>();
				itemIDs.Add(itemLeftID);
				itemIDs.Add(itemRightID);
				return itemIDs;
		}

		public void saveFromList(List<int> equipment)
		{
				if ( equipment.Count >= 2 )
				{
						itemLeftID = equipment[0];
						itemRightID = equipment[1];
				}
				else if ( equipment.Count == 1 )
						itemLeftID = equipment[0];
		}

		public static int GetListIDLeft() { return 0; }
		public static int GetListIDRight() { return 1; }
}

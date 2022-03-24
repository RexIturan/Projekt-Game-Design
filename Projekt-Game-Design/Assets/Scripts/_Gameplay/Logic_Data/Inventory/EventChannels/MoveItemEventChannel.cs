using System;
using Events.ScriptableObjects.Core;
using GDP01._Gameplay.Logic_Data.Inventory.Types;
using UnityEngine;

namespace GDP01._Gameplay.Logic_Data.Inventory.EventChannels {
	[CreateAssetMenu(fileName = "MoveItemEC", menuName = "Events/Inventory/MoveItemEventChannel")]
	public class MoveItemEventChannel : EventChannelBaseSO {
		public event Action<InventoryTarget, int, InventoryTarget, int, int> OnEventRaised;

		public void RaiseEvent(InventoryTarget fromTarget, int fromId, 
			InventoryTarget toTarget, int toID, int playerID) {
			OnEventRaised?.Invoke(fromTarget, fromId, toTarget, toID, playerID);
		}
	}
}
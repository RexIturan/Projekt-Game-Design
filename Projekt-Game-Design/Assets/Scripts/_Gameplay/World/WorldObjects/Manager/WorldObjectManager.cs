using GDP01.Gameplay.SaveTypes;
using Grid;
using UnityEngine;

namespace WorldObjects {
	public partial class WorldObjectManager : SaveObjectManager {
		[SerializeField] private GridDataSO _gridData;

		public void ClearAllComponents() {
			ClearDoors();
			ClearSwitches();
			ClearItems();
		}
	}
}
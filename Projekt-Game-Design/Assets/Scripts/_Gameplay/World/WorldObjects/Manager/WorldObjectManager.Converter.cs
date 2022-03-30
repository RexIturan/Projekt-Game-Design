using UnityEngine;
using Visual;

namespace WorldObjects {
	public partial class WorldObjectManager {
		[SerializeField] private WorldObjectList worldObjectList;
		[SerializeField] private PrefabGridDrawer itemDrawer;

		private void ConvertWorldObjects() {
			if ( worldObjectList is { } ) {
				//
				// foreach ( var doorObj in worldObjectList.doors ) {
				// 	Door doorComponent = doorObj.GetComponent<Door>();
				// 	if ( doorComponent is { } ) {
				// 		_doorComponents.Add(doorComponent);
				// 	}
				// }
				//
				// foreach ( var switchObj in worldObjectList.switches ) {
				// 	SwitchComponent switchComponent = switchObj.GetComponent<SwitchComponent>();
				// 	if ( switchComponent is { } ) {
				// 		_switchComponents.Add(switchComponent);
				// 	}
				// }
				//
				// foreach ( var itemObj in worldObjectList.items ) {
				// 	ItemComponent itemComponent = itemObj.GetComponent<ItemComponent>();
				// 	if ( itemComponent is { } ) {
				// 		_itemComponents.Add(itemComponent);
				// 	}
				// }
			}
		}
	}
}
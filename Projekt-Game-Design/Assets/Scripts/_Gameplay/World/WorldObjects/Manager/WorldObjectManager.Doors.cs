using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util.Extensions;

namespace WorldObjects {
	public partial class WorldObjectManager {
		[SerializeField] private DoorTypeSO defaultDoorTypeSO;
		[SerializeField] private Transform doorParent;
		[SerializeField] private List<Door> _doorComponents;

		private List<Door.DoorData> SaveDoors() {
			return SaveComponents(_doorComponents, managerData.DoorDataList);

			// var doorDatas = worldObjectManagerData.DoorDataList;
			//
			// foreach ( var door in _doorComponents ) {
			// 	Door.DoorData doorData = door.Save();
			//
			// 	int existingIdx = doorDatas.FindIndex(d => d.Id == doorData.Id);
			//
			// 	if ( existingIdx != -1 ) {
			// 		doorDatas[existingIdx] = doorData;
			// 	}
			// 	else {
			// 		doorDatas.Add(doorData);
			// 	}
			// }
			// return doorDatas;
		}

		private void LoadDoors(List<Door.DoorData> dataDoorDataList) {
			LoadComponents(
				ref _doorComponents,
				ref managerData.DoorDataList,
				dataDoorDataList,
				defaultDoorTypeSO.prefab,
				doorParent);

			// //clear and remove all _doors
			// _doorComponents.ClearGameObjectReferences();
			// _doorComponents = new List<Door>();
			//
			// //cache door data
			// worldObjectManagerData.DoorDataList = dataDoorDataList ?? new List<Door.DoorData>();
			//
			// foreach ( var doorData in worldObjectManagerData.DoorDataList ) {
			//
			// 	if ( doorData is { } ) {
			// 		doorData.Prefab = doorData.Type?.prefab ?? defaultDoorTypeSO.prefab;
			// 		Door door = CreateDoor(doorData);
			// 		
			// 		_doorComponents.Add(door);
			// 	}
		}
		
		[ContextMenu("Add Door")]
		private void AddDoor() {
			var data = defaultDoorTypeSO.ToComponentData();
			
			//todo refactor get next playerchar id
			data.Id = _doorComponents.Count + managerData.DoorDataList?.Count ?? 0;
			_doorComponents.Add(CreateComponent<Door, Door.DoorData>(data, doorParent));
		}
	}
}
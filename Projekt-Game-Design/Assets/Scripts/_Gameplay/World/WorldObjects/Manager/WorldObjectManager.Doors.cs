using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util.Extensions;

namespace WorldObjects {
	public partial class WorldObjectManager {
		[Header("Door Data")]
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
		private void AddNewDoor() {
			_doorComponents.Add(CreateDoor(defaultDoorTypeSO));
		}

		public void AddDoor(Door door) {
			door.transform.SetParent(doorParent ? doorParent : transform);
			door.Id = _doorComponents.Count;
			_doorComponents.Add(door);
		}
		
		private Door CreateDoor(DoorTypeSO doorType) {
			var data = doorType.ToComponentData();
			//todo refactor get next playerchar id
			data.Id = _doorComponents.Count + managerData.DoorDataList?.Count ?? 0;
			Door door = CreateComponent<Door, Door.DoorData>(data, doorParent);
			return door;
		}

		public Door GetDoorAt(Vector3 worldPos) {
			return GetDoorAt(_gridData.GetGridPos3DFromWorldPos(worldPos));
		}
		
		public Door GetDoorAt(Vector3Int gridPos) {
			return _doorComponents.FirstOrDefault(door => door.GridPosition.Equals(gridPos));
		}
		
		public void AddDoorAt(DoorTypeSO doorType, Vector3 worldPosition) {
			if ( GetDoorAt(worldPosition) == null ) {
				var door = CreateDoor(doorType);
				door.GridTransform.MoveTo(worldPosition);	
				_doorComponents.Add(door);
			}
		}
		
		public void RemoveDoorAt(Vector3 worldPos) {
			var door = GetDoorAt(worldPos);
			if ( door is { } ) {
				_doorComponents.Remove(door);
				Destroy(door.gameObject);
			}
		}

		private void ClearDoors() {
			_doorComponents.ClearMonoBehaviourGameObjectReferences();
			managerData.DoorDataList.Clear();
		}


		public List<Door> GetDoors() {
			return _doorComponents;
		}
	}
}
using System;
using System.Collections.Generic;
using SaveSystem.V2.Data;
using UnityEngine;

namespace WorldObjects {
	public partial class WorldObjectManager : ISaveState<WorldObjectManager.Data> {
		[Serializable]
		public class Data {
			public List<Door.DoorData> DoorDataList;
			public List<SwitchComponent.SwitchData> SwitchDataList;
			public List<ItemComponent.ItemData> ItemDataList;
			
			public Data(
				List<Door.DoorData> doors, 
				List<SwitchComponent.SwitchData> switches,
				List<ItemComponent.ItemData> items) {
				
				DoorDataList = doors;
				SwitchDataList = switches;
				ItemDataList = items;
			}
		}

		[SerializeField] private Data managerData;
		
		public Data Save() {
			return new Data(
				SaveDoors(),
				SaveSwitches(),
				SaveItems()
			);
		}

		public void Load(Data data) {
			this.managerData = data;
			
			//load doors
			LoadDoors(data.DoorDataList);
			
			//load switches
			LoadSwitches(data.SwitchDataList);
			
			//load items
			LoadItems(data.ItemDataList);
		}
	}
}
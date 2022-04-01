using System;
using System.Collections.Generic;
using GDP01.TileEffects;
using SaveSystem.V2.Data;
using UnityEngine;

namespace WorldObjects {
	public partial class WorldObjectManager : ISaveState<WorldObjectManager.Data> {
		[Serializable]
		public class Data {
			public List<Door.DoorData> DoorDataList;
			public List<SwitchComponent.SwitchData> SwitchDataList;
			public List<Junk.JunkData> JunkDataList;
			public List<ItemComponent.ItemData> ItemDataList;
			
			
			public Data(
				List<Door.DoorData> doors, 
				List<SwitchComponent.SwitchData> switches,
				List<Junk.JunkData> junks,
				List<ItemComponent.ItemData> items
			) {
				
				DoorDataList = doors;
				SwitchDataList = switches;
				JunkDataList = junks;
				ItemDataList = items;
			}
		}

		[SerializeField] private Data managerData;
		
		public Data Save() {
			if(worldObjectList is {})
				ConvertWorldObjects();

			return new Data(
				SaveDoors(),
				SaveSwitches(),
				SaveJunks(),
				SaveItems()
			);
		}

		public void Load(Data data) {
			this.managerData = data;
			
			//load doors
			LoadDoors(data.DoorDataList);
			
			//load switches
			LoadSwitches(data.SwitchDataList);
			
			//load junks
			LoadJunks(data.JunkDataList);
			
			//load items
			LoadItems(data.ItemDataList);
		}
	}
}
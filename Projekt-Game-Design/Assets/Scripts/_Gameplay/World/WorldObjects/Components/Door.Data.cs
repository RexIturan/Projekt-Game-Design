using System;
using System.Collections.Generic;
using SaveSystem.V2.Data;

namespace WorldObjects {
	public partial class Door : ISaveState<Door.DoorData> {
		[Serializable]
		public class DoorData : WorldObject.Data {
			// type
			// public new DoorTypeSO _type;
			// public new DoorTypeSO Type {
			// 	get { return ( DoorTypeSO )_type; }
			// 	set { _type = value;  }
			// }

			// state
			public bool open;
			public bool broken;
			public bool locked;
			
			public List<int> keyIds;
			public List<int> switchIds;
			public List<int> triggerIds;
			public List<int> remainingSwitches;
			public List<int> remainingTrigger;

			public void Init(DoorData data) {
				open = data.open;
				broken = data.broken;
				locked = data.locked;
				
				keyIds = data.keyIds;
				switchIds = data.switchIds;
				triggerIds = data.triggerIds;
				remainingSwitches = data.remainingSwitches;
				remainingTrigger = data.remainingTrigger;
				
				ReferenceData = data.ReferenceData;
			}
		}

		public override DoorData Save() {
			DoorData data = base.Save();

			doorData.ReferenceData = Type.ToReferenceData();
			//todo save door data
			data.Init(doorData);
			
			return data;
		}

		public override void Load(DoorData data) {
			base.Load(data);

			_type = ( DoorTypeSO )data.ReferenceData.obj;
			//todo load door data
			
			//todo does this work?
			doorData = data;
		}
	}
}
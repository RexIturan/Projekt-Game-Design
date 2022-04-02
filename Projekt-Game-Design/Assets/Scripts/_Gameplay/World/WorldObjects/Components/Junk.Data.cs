using System;
using SaveSystem.V2.Data;

namespace WorldObjects {
	public partial class Junk : ISaveState<Junk.JunkData> {
		[Serializable]
		public class JunkData : WorldObject.Data {
			// state
			public bool broken;

			public void Init(JunkData data) {
				broken = data.broken;
				
				ReferenceData = data.ReferenceData;
			}
		}

		public override JunkData Save() {
			JunkData data = base.Save();

			junkData.ReferenceData = Type.ToReferenceData();
			//todo save door data
			data.Init(junkData);
			
			return data;
		}

		public override void Load(JunkData data) {
			base.Load(data);

			Type = ( JunkTypeSO )data.ReferenceData.obj;
			
			this.Initialise(Type);
			
			//todo does this work?
			junkData = data;
		}
	}
}
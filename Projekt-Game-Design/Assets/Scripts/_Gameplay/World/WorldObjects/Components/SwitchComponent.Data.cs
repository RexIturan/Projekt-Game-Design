using System;
using FullSerializer;
using SaveSystem.V2.Data;
using UnityEngine;

namespace WorldObjects {
	public partial class SwitchComponent : ISaveState<SwitchComponent.SwitchData> {
		[Serializable]
		public class SwitchData : WorldObject.Data {

			[SerializeField] private bool active;
			[SerializeField] private int range = -1;
			
			public bool Active {
				get => active;
				set => active = value;
			}

			public int Range {
				get => range;
				set => range = value;
			}

			public void Init(SwitchData data) {
				active = data.active;
				range = data.range;
			}
		}
		
		public override SwitchData Save() {
			SwitchData data = base.Save();

			switchData.ReferenceData = Type.ToReferenceData();
			switchData.Range = Type.range;
			
			//todo save door data
			data.Init(switchData);
			
			return data;
		}

		public override void Load(SwitchData data) {
			base.Load(data);

			Type = ( SwitchTypeSO )data.ReferenceData.obj;
			//todo load door data
			
			//todo does this work?
			switchData = data;
		}
	}
}
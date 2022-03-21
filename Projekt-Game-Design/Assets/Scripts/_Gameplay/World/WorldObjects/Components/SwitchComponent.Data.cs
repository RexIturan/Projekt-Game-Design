using System;
using FullSerializer;
using SaveSystem.V2.Data;
using UnityEngine;

namespace WorldObjects {
	public partial class SwitchComponent : ISaveState<SwitchComponent.SwitchData> {
		[Serializable]
		public class SwitchData : WorldObject.Data {
			protected new SwitchTypeSO _type;
			public new SwitchTypeSO Type { get => _type; set => _type = value; }

			[SerializeField] private bool active;
			public bool Active {
				get => active;
				set => active = value;
			}
			public int range = -1;

			public void Init(SwitchData data) {
				_type = data.Type;
				active = data.active;
				range = data.range < 0 ? Type.range : data.range;
			}
		}
		
		public override SwitchData Save() {
			SwitchData data = base.Save();
			
			//todo save door data
			data.Init(switchData);
			
			return data;
		}

		public override void Load(SwitchData data) {
			base.Load(data);

			_type = data.Type;
			//todo load door data
			
			//todo does this work?
			switchData = data;
		}
	}
}
using System;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

namespace SaveSystem.V2.Converter {
	public class SpriteConverter : fsDirectConverter<Sprite> {
		public override Type ModelType => typeof(Sprite);

		protected override fsResult DoSerialize(Sprite model, Dictionary<string, fsData> serialized) {

			serialized["Name"] = new fsData(model.name);
			
			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Sprite model) {
			var result = fsResult.Success;
			
			if ((result += DeserializeMember(data, null, "Name", out string name)).Failed) return result;

			model = GameDataProvider.GetSpriteByName(name);
			
			return result;
		}
	}
}
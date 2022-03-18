using System;
using System.Collections.Generic;
using Characters;
using Characters.Types;
using FullSerializer;
using UnityEngine;

namespace SaveSystem.V2.Converter {
	public class StatisticsConverter : fsDirectConverter<Statistics> {
		public override Type ModelType => typeof(Statistics);

		protected override fsResult DoSerialize(Statistics model, Dictionary<string, fsData> serialized) {
			
			SerializeMember(serialized, null, "DisplayName", model.DisplayName);
			// SerializeMember(serialized, null, "Icon", model.DisplayImage);
			SerializeMember(serialized, null, "Stats", model.StatusValues.Stats);
			SerializeMember(serialized, null, "Faction", model.Faction);
			
			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Statistics model) {
			var result = fsResult.Success;
			
			if ((result += DeserializeMember(data, null, "DisplayName",  out string name)).Failed) return result;
			// if ((result += DeserializeMember(data, null, "Icon",  out Sprite icon)).Failed) return result;
			if ((result += DeserializeMember(data, null, "Stats",  out List<StatusValue> stats)).Failed) return result;
			if ((result += DeserializeMember(data, null, "Faction",  out Faction faction)).Failed) return result;

			model.DisplayName = name;
			model.StatusValues.InitValues(stats);
			model.SetFaction(faction);
			// model.DisplayImage = icon;
			
			return result;
		}
	}
}
using System;
using System.Collections.Generic;
using FullSerializer;
using Util.Types;

namespace SaveSystem.V2.Converter {
	public class RangedIntConverter : fsDirectConverter<RangedInt> {
		public override Type ModelType => typeof(RangedInt);

		public override object CreateInstance(fsData data, Type storageType) {
			return new RangedInt(0, 100, 100);
		}

		protected override fsResult DoSerialize(RangedInt model, Dictionary<string, fsData> serialized) {
			serialized["Min"] = new fsData(model.Min);
			serialized["Max"] = new fsData(model.Max);
			serialized["Value"] = new fsData(model.Value);
			
			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref RangedInt model) {
			fsResult result = fsResult.Success;

			if ((result += DeserializeMember(data, null, "Min",   out int min)).Failed) return result;
			if ((result += DeserializeMember(data, null, "Max",   out int max)).Failed) return result;
			if ((result += DeserializeMember(data, null, "Value", out int value)).Failed) return result;

			model = new RangedInt(min, max, value);
			
			return result;
		}
	}
}
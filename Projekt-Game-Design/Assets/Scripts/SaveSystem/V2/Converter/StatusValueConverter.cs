using System;
using System.Collections.Generic;
using Characters;
using FullSerializer;

namespace SaveSystem.V2.Converter {
	public class StatusValueConverter : fsDirectConverter<StatusValue> {
		public override Type ModelType => typeof(StatusValue);

		public override object CreateInstance(fsData data, Type storageType) {
			return new StatusValue(StatusType.None, 0, 100, 100);
		}

		protected override fsResult DoSerialize(StatusValue model, Dictionary<string, fsData> serialized) {
			SerializeMember(serialized, null, "Type", model.Type);
			SerializeMember(serialized, null, "Min", model.Min);
			SerializeMember(serialized, null, "Max", model.Max);
			SerializeMember(serialized, null, "Value", model.Value);
			
			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref StatusValue model) {
			var result = fsResult.Success;

			if ((result += DeserializeMember(data, null, "Type",  out StatusType type)).Failed) return result;
			if ((result += DeserializeMember(data, null, "Min",   out int min)).Failed) return result;
			if ((result += DeserializeMember(data, null, "Max",   out int max)).Failed) return result;
			if ((result += DeserializeMember(data, null, "Value", out int value)).Failed) return result;

			model = new StatusValue(type, min, max, value);
			
			return result;
		}
	}
}
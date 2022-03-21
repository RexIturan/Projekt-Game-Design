using System;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

namespace SaveSystem.V2.Converter {
	public class Vector3IntConverter : fsDirectConverter<Vector3Int> {
		public override Type ModelType => typeof(Vector3Int);

		public override object CreateInstance(fsData data, Type storageType) {
			return new Vector3Int();
		}

		protected override fsResult DoSerialize(Vector3Int model, Dictionary<string, fsData> serialized) {
			serialized["x"] = new fsData(model.x);
			serialized["y"] = new fsData(model.y);
			serialized["z"] = new fsData(model.z);
			
			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Vector3Int model) {
			fsResult result = fsResult.Success;

			if ((result += DeserializeMember(data, null, "x", out int x)).Failed) return result;
			if ((result += DeserializeMember(data, null, "y", out int y)).Failed) return result;
			if ((result += DeserializeMember(data, null, "z", out int z)).Failed) return result;

			model.x = x;
			model.y = y;
			model.z = z;
			
			return result;
		}
	}
}
using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.World.Character.Data;
using UnityEngine;
using WorldObjects;

namespace SaveSystem.V2.Converter.WorldObjects {
	public class WorldObjectConverter<T> : fsDirectConverter<T> where T : WorldObject.Data, new() {
		
		protected override fsResult DoSerialize(T model, Dictionary<string, fsData> serialized) {
			
			serialized["Id"] = new fsData(model.Id);

			SerializeMember(serialized, null, "GridPosition", model.GridPosition);
			SerializeMember(serialized, null, "Rotation", model.Rotation);
			
			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref T model) {
			
			if ((DeserializeMember(data, null, "Id", out int id)).Succeeded) model.Id = id;
			if ((DeserializeMember(data, null, "GridPosition", out Vector3Int position)).Succeeded) model.GridPosition = position;
			if ((DeserializeMember(data, null, "Rotation", out Vector3 rotation)).Succeeded) model.Rotation = rotation;
			
			return fsResult.Success;
		}
	}
}
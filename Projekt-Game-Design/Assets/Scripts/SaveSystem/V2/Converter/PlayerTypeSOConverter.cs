using System;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

namespace SaveSystem.V2.Converter {
	public class PlayerTypeSOConverter : fsDirectConverter<PlayerTypeSO> {
		public override Type ModelType => typeof(PlayerTypeSO);

		public override object CreateInstance(fsData data, Type storageType) {
			return ScriptableObject.CreateInstance<PlayerTypeSO>();
		}

		protected override fsResult DoSerialize(PlayerTypeSO model, Dictionary<string, fsData> serialized) {
			
			serialized["ItemName"] = new fsData(model.name);
			serialized["Guid"] = new fsData(model.Guid);
			
			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref PlayerTypeSO model) {
			var result = fsResult.Success;

			// if ((result += DeserializeMember(data, null, "ItemName", out string itemName)).Failed);
			// if ((result += DeserializeMember(data, null, "Guid", out string guid)).Failed);
			
			// todo
			// model = GameData.ItemContainer.GetItemByNameOrGuid(itemName, guid);
			
			return result;
		}
	}
}
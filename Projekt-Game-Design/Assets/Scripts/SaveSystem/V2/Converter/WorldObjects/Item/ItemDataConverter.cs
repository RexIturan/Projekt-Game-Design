using System;
using System.Collections.Generic;
using FullSerializer;
using SaveSystem.V2.Converter.WorldObjects;
using WorldObjects;

namespace SaveSystem.V2.Converter.Item {
	public class ItemDataConverter : WorldObjectConverter<ItemComponent.ItemData> {
		public override Type ModelType => typeof(ItemComponent.ItemData);

		protected override fsResult DoSerialize(ItemComponent.ItemData model, Dictionary<string, fsData> serialized) {
			base.DoSerialize(model, serialized);
			
			SerializeMember(serialized, 
				null, "Type", model.Type);
			
			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref ItemComponent.ItemData model) {
			var result = base.DoDeserialize(data, ref model);

			if ( ( DeserializeMember(data, null, "Type", out ItemSO.ItemTypeData itemType) ).Succeeded ) {
				//todo find item reference
				model.Type = itemType;
			}
			
			return result;
		}
	}
}
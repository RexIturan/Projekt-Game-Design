using System;
using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.Provider;

namespace SaveSystem.V2.Converter.Item {
	public class ItemTypeDataConverter : fsDirectConverter<ItemSO.ItemTypeData> {
		public override Type ModelType => typeof(ItemSO.ItemTypeData);

		public override object CreateInstance(fsData data, Type storageType) {
			return new ItemSO.ItemTypeData();
		}

		protected override fsResult DoSerialize(ItemSO.ItemTypeData model, Dictionary<string, fsData> serialized) {

			if ( model.name != null ) {
				serialized["Name"] = new fsData(model.name);		
			}

			if ( model.guid != null ) {
				serialized["Guid"] = new fsData(model.guid);
			}

			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref ItemSO.ItemTypeData model) {
			var result = fsResult.Success;

			ItemSO item = null;

			if (
				( DeserializeMember(data, null, "Guid", out string guid) ).Succeeded ) {
				model.guid = guid;
				item = GameplayDataProvider.Current.ItemContainerSO.GetItemTypeByGuid(guid) ?? item;
				
			}
			else if (
				( DeserializeMember(data, null, "Name", out string name) ).Succeeded ) {
				model.name = name;
				item = GameplayDataProvider.Current.ItemContainerSO.GetItemTypeByName(name) ?? item;
			}
			else {
				// result = fsResult.Fail("CharacterType cant be reconstructed");
			}

			model.obj = item;
			
			// instance = obj;
			
			// if(instance == null) result = fsResult.Fail("CharacterType cant be reconstructed");
			result = fsResult.Success;
			return result;
		}
	}
}
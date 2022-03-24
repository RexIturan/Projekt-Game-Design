using System;
using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.Provider;

namespace SaveSystem.V2.Converter.Item {
	public class ItemTypeDataConverter : fsDirectConverter<ItemTypeSO.ItemTypeData> {
		public override Type ModelType => typeof(ItemTypeSO.ItemTypeData);

		public override object CreateInstance(fsData data, Type storageType) {
			return new ItemTypeSO.ItemTypeData();
		}

		protected override fsResult DoSerialize(ItemTypeSO.ItemTypeData model, Dictionary<string, fsData> serialized) {

			if ( model.name != null ) {
				serialized["Name"] = new fsData(model.name);		
			}

			if ( model.guid != null ) {
				serialized["Guid"] = new fsData(model.guid);
			}

			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref ItemTypeSO.ItemTypeData model) {
			var result = fsResult.Success;

			ItemTypeSO itemType = null;

			if (
				( DeserializeMember(data, null, "Guid", out string guid) ).Succeeded ) {
				model.guid = guid;
				itemType = GameplayDataProvider.Current.ItemTypeContainerSO.GetItemTypeByGuid(guid) ?? itemType;
				
			}
			else if (
				( DeserializeMember(data, null, "Name", out string name) ).Succeeded ) {
				model.name = name;
				itemType = GameplayDataProvider.Current.ItemTypeContainerSO.GetItemTypeByName(name) ?? itemType;
			}
			else {
				// result = fsResult.Fail("CharacterType cant be reconstructed");
			}

			model.obj = itemType;
			
			// instance = obj;
			
			// if(instance == null) result = fsResult.Fail("CharacterType cant be reconstructed");
			result = fsResult.Success;
			return result;
		}
	}
}
using System;
using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.Provider;
using static GDP01.Util.SerializableScriptableObject;

namespace SaveSystem.V2.Converter.WorldObjects {
	public class ReferenceDataConverter : fsDirectConverter<ReferenceData> {
		public override Type ModelType => typeof(ReferenceData);

		public override object CreateInstance(fsData data, Type storageType) {
			return new ReferenceData();
		}

		protected override fsResult DoSerialize(ReferenceData model, Dictionary<string, fsData> serialized) {

			if ( model.name != null ) {
				serialized["Name"] = new fsData(model.name);		
			}

			if ( model.guid != null ) {
				serialized["Guid"] = new fsData(model.guid);
			}

			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref ReferenceData model) {
			var result = fsResult.Success;

			if (
				( DeserializeMember(data, null, "Guid", out string guid) ).Succeeded ) {
				model.guid = guid;
			}
			else if (
				( DeserializeMember(data, null, "Name", out string name) ).Succeeded ) {
				model.name = name;
			}
			else {
				// result = fsResult.Fail("CharacterType cant be reconstructed");
			}
			
			//todo get reference ??
			model.obj = GameplayDataProvider.Current.FindSOByNameAndGuid(model.guid, model.name);
			
			// if(instance == null) result = fsResult.Fail("CharacterType cant be reconstructed");
			
			return result;
		}
	}
}
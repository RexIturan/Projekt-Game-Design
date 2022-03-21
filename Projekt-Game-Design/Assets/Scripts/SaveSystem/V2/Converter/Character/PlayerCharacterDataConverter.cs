using System;
using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.World.Character.Data;

namespace SaveSystem.V2.Converter {
	public class PlayerCharacterDataConverter : CharacterDataConverter<PlayerCharacterData> {

		private const string enteringField = "Entering";
		private const string locationField = "LocationName";
		
		public override Type ModelType => typeof(PlayerCharacterData);
		
		public override object CreateInstance(fsData data, Type storageType) {
			return new PlayerCharacterData();
		}
		
		protected override fsResult DoSerialize(PlayerCharacterData model, Dictionary<string, fsData> serialized) {
			fsResult result = base.DoSerialize(model, serialized);

			serialized[enteringField] = new fsData(model.EnteringNewLocation);
			serialized[locationField] = new fsData(model.LocationName);
			
			return result;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref PlayerCharacterData model) {
			fsResult result = base.DoDeserialize(data, ref model);
			
			if ( DeserializeMember(data, null, enteringField, out bool entering).Succeeded )
				model.EnteringNewLocation = entering;

			if ( DeserializeMember(data, null, locationField, out string locationName).Succeeded )
				model.LocationName = locationName;
			
			return result;
		}
	}
}
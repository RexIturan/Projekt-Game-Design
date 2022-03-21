using System;
using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.World.Character.Data;

namespace SaveSystem.V2.Converter {
	public class EnemyCharacterDataConverter : CharacterDataConverter<EnemyCharacterData> {
		
		public override Type ModelType => typeof(EnemyCharacterData);
		
		public override object CreateInstance(fsData data, Type storageType) {
			return new EnemyCharacterData();
		}
		
		protected override fsResult DoSerialize(EnemyCharacterData model, Dictionary<string, fsData> serialized) {
			fsResult result = base.DoSerialize(model, serialized);

			// serialized[enteringField] = new fsData(model.EnteringNewLocation);
			
			return result;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref EnemyCharacterData model) {
			fsResult result = base.DoDeserialize(data, ref model);
			
			// if ( DeserializeMember(data, null, enteringField, out bool entering).Succeeded )
			// 	model.EnteringNewLocation = entering;
			
			return result;
		}
	}
}
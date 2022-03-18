using System;
using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using GDP01._Gameplay.World.Character.Data;
using UnityEngine;

namespace SaveSystem.V2.Converter {
	public class CharacterTypeSOConverter : fsConverter {
		
		// protected override fsResult DoSerialize(CharacterTypeSO<T> model, Dictionary<string, fsData> serialized) {
		// 	
		// 	//save name & guid & id?
		// 	
		// 	return fsResult.Success; 
		// }
		//
		// protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref CharacterTypeSO<T> model) {
		// 	var result = fsResult.Success;
		//
		// 	if ((result += DeserializeMember(data, null, "Name", out string name)).Failed) return result;
		// 	if ((result += DeserializeMember(data, null, "Guid", out string guid)).Failed) return result;
		// 	
		// 	model = GameplayDataProvider.Current.CharacterTypeContainerSO.Get
		// 	
		// 	return result;
		// }

		public override object CreateInstance(fsData data, Type storageType) {
			return GameplayDataProvider.Current.CharacterTypeContainerSO.DefaultCharacterType;
		}

		public override bool CanProcess(Type type) {
			bool canProcess =
				typeof(CharacterTypeSO).IsAssignableFrom(type);
				// type == typeof(CharacterTypeSO);// || 
				// type == typeof(PlayerTypeSO); 
			return canProcess;
		}
		
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType) {
			var model = (CharacterTypeSO)instance;
			
			var serializedDictionary = new Dictionary<string, fsData> {
				["Name"] = new fsData(model.name),
				["Guid"] = new fsData(model.Guid)
			};

			serialized = new fsData(serializedDictionary);
			return fsResult.Success;
		}

		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType) {
			var result = fsResult.Success;
			if ((result += CheckType(data, fsDataType.Object)).Failed) return result;

			var obj = (CharacterTypeSO)instance;
			
			if (
				(DeserializeMember(data.AsDictionary, null, "Guid", out string guid)).Succeeded ) {
				obj = GameplayDataProvider.Current.CharacterTypeContainerSO.GetCharacterTypeByGuid(guid);
				
			} else if (
				(DeserializeMember(data.AsDictionary, null, "Name", out string name)).Succeeded ) {
				obj =GameplayDataProvider.Current.CharacterTypeContainerSO.GetCharacterTypeByName(name);
				
			}
			else {
				// result = fsResult.Fail("CharacterType cant be reconstructed");
			}

			instance = obj;
			
			// if(instance == null) result = fsResult.Fail("CharacterType cant be reconstructed");
			result = fsResult.Success;
			return result;
		}
	}
}
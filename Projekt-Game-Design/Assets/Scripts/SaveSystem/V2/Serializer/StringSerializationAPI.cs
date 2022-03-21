using System;
using FullSerializer;
using SaveSystem.V2.Converter;
using SaveSystem.V2.Converter.Item;
using SaveSystem.V2.Converter.WorldObjects;

namespace SaveSystem.V2.Serializer {
	public static class StringSerializationAPI {
		private static readonly fsSerializer _serializer = CreateSerializer();

		private static fsSerializer CreateSerializer() {
			var serializer = new fsSerializer();
			//Add Converter here
			// serializer.AddConverter(new Converter());
			
			serializer.AddConverter(new Vector3IntConverter());
			// serializer.AddConverter(new PlayerTypeSOConverter());
			serializer.AddConverter(new RangedIntConverter());
			serializer.AddConverter(new StatusValueConverter());
			serializer.AddConverter(new StatisticsConverter());
			
			serializer.AddConverter(new CharacterTypeSOConverter());
			serializer.AddConverter(new PlayerCharacterDataConverter());
			serializer.AddConverter(new EnemyCharacterDataConverter());
			
			serializer.AddConverter(new ReferenceDataConverter());
			serializer.AddConverter(new ItemTypeDataConverter());
			// serializer.AddConverter(new ItemDataConverter());
			// serializer.AddConverter(new InventoryConverter());
			
			//todo serialize char icon instead
			// serializer.AddConverter(new SpriteConverter());
			
			return serializer;
		}
		
		public static string Serialize(Type type, object value, bool pretty = true) {
			// serialize the data
			fsData data;
			_serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

			// emit the data via JSON
			return pretty ? fsJsonPrinter.PrettyJson(data) : fsJsonPrinter.CompressedJson(data);
		}

		public static object Deserialize(Type type, string serializedState) {
			// step 1: parse the JSON data
			fsData data = fsJsonParser.Parse(serializedState);

			// step 2: deserialize the data
			object deserialized = null;
			_serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

			return deserialized;
		}
	}
}
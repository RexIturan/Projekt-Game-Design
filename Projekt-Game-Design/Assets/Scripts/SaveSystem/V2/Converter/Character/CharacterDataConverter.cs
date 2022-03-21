using System;
using System.Collections.Generic;
using Characters;
using FullSerializer;
using GDP01._Gameplay.World.Character;
using GDP01._Gameplay.World.Character.Data;
using UnityEngine;

namespace SaveSystem.V2.Converter {
	public class CharacterDataConverter<T> : fsDirectConverter<T> where T : CharacterData, new() {

		private Dictionary<string, fsData> GetStatusValueDifference(StatusValue current, StatusValue baseValue ) {
			Dictionary<string, fsData> difference = new Dictionary<string, fsData>();

			if ( !current.Min.Equals(baseValue.Min) ) {
				difference["Min"] = new fsData(current.Min);
			}
			
			if ( !current.Max.Equals(baseValue.Max) ) {
				difference["Max"] = new fsData(current.Max);
			}
			
			if ( !current.Value.Equals(baseValue.Value) ) {
				difference["Value"] = new fsData(current.Value);
			}
			
			return difference;
		}
		
		private void SerializeStatsOverride(ref Dictionary<string, fsData> serialized, T model) {
			var statsOverride = new Dictionary<string, fsData>();
			var currentStats = model.Stats.GetStatusValues();
			StatusValues baseStats = new StatusValues().InitValues(model.Type.baseStatusValues);

			foreach ( StatusValue currentStat in currentStats ) {

				StatusValue baseStat = baseStats.GetValue(currentStat.Type);
				
				if ( !baseStat.Equals(currentStat) ) {
					statsOverride[currentStat.Name] = new fsData(GetStatusValueDifference(currentStat, baseStat));
				}
			}

			if ( statsOverride.Count == 0 ) statsOverride = null;
			serialized[statsField] = new fsData(statsOverride);
		}

		private fsResult DeserializeDict(Dictionary<string, fsData> data, string name, out Dictionary<string, fsData> value) {
			fsData memberData;
			if (data.TryGetValue(name, out memberData) == false) {
				value = default(Dictionary<string, fsData>);
				return fsResult.Fail("Unable to find member \"" + name + "\"");
			}
			
			var result = fsResult.Success;
			value = memberData.IsDictionary ? memberData.AsDictionary : null;
			return result;
		}
		
		private void DeserializeStatsOverride(Dictionary<string, fsData> data, ref T model) {

			if(DeserializeDict(data, statsField, out Dictionary<string, fsData> statsOverrideDict).Failed) return;
			
			if(statsOverrideDict == null) return;

			foreach ( var statsOverrideKey in statsOverrideDict.Keys ) {
				
				if ( statsOverrideDict[statsOverrideKey] != null &&
				     Enum.TryParse(statsOverrideKey, out StatusType type) ) {
					
					Dictionary<string, fsData> stat = statsOverrideDict[statsOverrideKey].AsDictionary;
					
					StatusValue baseValue = model.Stats.GetValue(type);
		
					if ( DeserializeMember(stat, null, "Min", out int min).Succeeded ) {
						baseValue.Min = min;
					}
		
					if ( DeserializeMember(stat, null, "Max", out int max).Succeeded ) {
						baseValue.Max = max;
					}
		
					if ( DeserializeMember(stat, null, "Value", out int value).Succeeded ) {
						baseValue.Value = value;
					}
		
					model.Stats.SetValue(baseValue);	
				}
			}
		}
		
///// fs Methodes //////////////////////////////////////////////////////////////////////////////////		

		private const string statsField = "StatsOverride";

		public override Type ModelType => typeof(CharacterData);

		public override object CreateInstance(fsData data, Type storageType) {
			return new CharacterData();
		}

		protected override fsResult DoSerialize(T model, Dictionary<string, fsData> serialized) {
			serialized["Id"] = new fsData(model.Id);
			serialized["Name"] = new fsData(model.Name);
			serialized["Active"] = new fsData(model.Active);

			SerializeMember(serialized, null, "GridPosition", model.GridPosition);
			SerializeMember(serialized, null, "Type", model.Type);
			
			// --- Overrides ---
			//serialize overrides
			//todo stats
			//todo base abilities -> list if other then Type
			//todo MovementPointsPerEnergy
			//todo MovementCostPerTile
			
			// Stats
			SerializeStatsOverride(ref serialized, model);
			
			return fsResult.Success;
		}

		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref T model) {
			fsResult result = fsResult.Success;
			
			// if ((result += DeserializeMember(data, null, "Name", out string name)).Failed) return result;
			// if ((result += DeserializeMember(data, null, "Active", out bool active)).Failed) return result;
			// if ((result += DeserializeMember(data, null, "Id", out int id)).Failed) return result;
			// if ((result += DeserializeMember(data, null, "GridPosition", out int gridPosition)).Failed) return result;
			// if ((result += DeserializeMember(data, null, "Type", out CharacterTypeSO type)).Failed) return result;

			if ((DeserializeMember(data, null, "Name", out string name)).Succeeded) model.Name = name;
			if ((DeserializeMember(data, null, "Active", out bool active)).Succeeded) model.Active = active;
			if ((DeserializeMember(data, null, "Id", out int id)).Succeeded) model.Id = id;
			if ((DeserializeMember(data, null, "GridPosition", out Vector3Int gridPosition)).Succeeded) model.GridPosition = gridPosition;
			
			if ( ( DeserializeMember(data, null, "Type", out CharacterTypeSO type) ).Succeeded ) {
				model.Type = type;
				model.Stats = new StatusValues().InitValues(type.baseStatusValues);
			}
			
			// model.Id = id;
			// model.Name = name;
			// model.Active = active;
			// model.Type = type;
			
			//todo set override values??

			DeserializeStatsOverride(data, ref model);
			
			return fsResult.Success;
		}
	}
}
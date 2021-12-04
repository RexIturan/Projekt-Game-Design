using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters {
	[Serializable]
	public class StatusValues {

		//health
		// public StatusValue hitPoints;
		// public StatusValue temporaryHitPoints;
		//
		// //attributes
		// public StatusValue strength;
		// public StatusValue dexterity;
		// public StatusValue intelligence;
		//
		// //energy
		// public StatusValue energy;
		//
		// //movement
		// public StatusValue movementRange;
		//
		// //view
		public StatusValue viewDistance {
			get { return GetValue(StatusType.ViewDistance); }
		}
		
//////////////////////////////////////////////////////////////////

		[SerializeField] private List<StatusValue> _statusValues;
		private Dictionary<StatusType, StatusValue> _values;

//////////////////////////////////////////////////////////////////

		public StatusValues() {
			InitialiseStatusValuesDictionary();
		}

		public void InitialiseStatusValuesDictionary() {
			_statusValues = new List<StatusValue>();
			_values = new Dictionary<StatusType, StatusValue>();
			var types = Enum.GetValues(typeof(StatusType));
			foreach ( var type in types ) {
				var stat = new StatusValue(( StatusType )type, 0, 0, 0);
				_values.Add(( StatusType )type, stat);
				_statusValues.Add(stat);
			}
		}

		public void InitValues(List<StatusValue> values) {
			foreach ( var value in values ) {
				SetValue(value.type, value);
			}
		}
		
		// getter setter
		public void SetValue(StatusType type, StatusValue value) {
			if ( _values.ContainsKey(type) ) {
				_values[type] = value;	
			}
			else {
				throw new InvalidOperationException();
			}
		}
		
		public StatusValue GetValue(StatusType type) {
			if ( _values.ContainsKey(type) ) {
				return _values[type];	
			}
			else {
				throw new InvalidOperationException();
			}
		}

		//todo add Modifier
		public void ModifyStatusValue(Modifier modifier) {
			throw new NotImplementedException();
		}
		
		public void RemoveModification(StanceType type) {
			throw new NotImplementedException();
		}
	}
}
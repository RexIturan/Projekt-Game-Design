﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters {
	[Serializable]
	public class StatusValues {

		//health
		public StatusValue HitPoints {
			get { return GetValue(StatusType.HitPoints); }
		}
		// public StatusValue temporaryHitPoints;
		
		//attributes
		public StatusValue Strength {
			get { return GetValue(StatusType.Strength); }
		}
		
		public StatusValue Dexterity {
			get { return GetValue(StatusType.Dexterity); }
		}
		
		public StatusValue Intelligence {
			get { return GetValue(StatusType.Intelligence); }
		}
		
		// //energy
		public StatusValue Energy {
			get { return GetValue(StatusType.Energy); }
		}
		
		// armor
		public StatusValue Armor {
			get { return GetValue(StatusType.Armor); }
		}
		
		//movement
		public StatusValue MovementRange {
			get { return GetValue(StatusType.MovementRange); }
		}
		
		// //view
		public StatusValue ViewDistance {
			get { return GetValue(StatusType.ViewDistance); }
		}
		
		//level
		public StatusValue Level {
			get { return GetValue(StatusType.Level); }
		}

		public List<StatusValue> Stats => _statusValues;
		
//////////////////////////////////////////////////////////////////

		[SerializeField] private List<StatusValue> _statusValues;
		private Dictionary<StatusType, int> _values;

//////////////////////////////////////////////////////////////////

		//empty StatusValues		
		public StatusValues() {
			// InitialiseStatusValuesDictionary();
		}

		public void InitialiseStatusValuesDictionary() {
			_statusValues = new List<StatusValue>();
			_values = new Dictionary<StatusType, int>();
			var types = Enum.GetValues(typeof(StatusType));
			
			for ( int i = 0; i < types.Length; i++ ) {
				var stat = new StatusValue(( StatusType )types.GetValue(i), 0, 0, 0);
				_values.Add(( StatusType )types.GetValue(i), i);
				_statusValues.Add(stat);
			}
		}

		public StatusValues InitValues(List<StatusValue> values) {
			InitialiseStatusValuesDictionary();

			if ( values != null ) {
				foreach ( var value in values ) {
					SetValue(value);
				}	
			}
			
			return this;
		}
		
		// getter setter
		// uses a deep copy of value and not actually same value
		// TODO: check if this is the expected behavior of function
		public void SetValue(StatusValue value) {

			if ( value != null ) {
				if ( _values.ContainsKey(value.Type) ) {
					_statusValues[_values[value.Type]] = value.Copy();
				}
				else {
					throw new InvalidOperationException();
				}	
			}
		}
		
		public StatusValue GetValue(StatusType type) {
			if ( _values is { } ) {
				if ( _values.ContainsKey(type) ) {
					return _statusValues[_values[type]];
				}
				else {
					throw new InvalidOperationException();
				}	
			}
			else {
				// _values isnt initialised
				// Debug.LogError("_values isnt initialised");
				return null;
			}
		}

    public StatusValue[] GetStatusValues() {
		  StatusValue[] values = new StatusValue[_statusValues.Count];
      _statusValues.CopyTo(values);
      return values;
    }
	}
}
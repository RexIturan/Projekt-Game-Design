using System;
using UnityEngine;

namespace Characters {
	[Serializable]
	public class StatusValue {
		[SerializeField] private string _name;
		[SerializeField] private StatusType _type;
		
		[SerializeField] private int _value;
		[SerializeField] private int _max;
		[SerializeField] private int _min;

		public string name { 
			get => _name;
			set { _name = value; } 
		}

		public StatusType type {
			get => _type;
			set { _type = value; }
		}
		
		public int value {
			get { return _value; }
			set {
				if ( value < min ) {
					_value = min;
				}
				else if( value < max ) {
					_value = max;
				}
				else {
					_value = value;
				}
			}
		}

		public int max {
			get { return _max; }
			set { _max = value < min ? min : value; }
		}

		public int min {
			get { return _min; }
			set { _min = value > max ? max : value; }
		}

		public void Increse(int incValue) {
			value += incValue;
		}

		public void Decrease(int decValue) {
			value -= decValue;
		}

		public bool IsMax() {
			return value == max;
		}

		public bool IsMin() {
			return value == min;
		}

		public void Fill() {
			value = max;
		}
		
		//todo name, type
		public StatusValue(StatusType type, int min, int value, int max) {
			this.type = type;
			this.min = min;
			this.max = max;
			this.value = value;
			
			//todo overthink this plz
			this._name = Enum.GetName(typeof(StatusType), type);
			this.name = _name;
		}
	}
}
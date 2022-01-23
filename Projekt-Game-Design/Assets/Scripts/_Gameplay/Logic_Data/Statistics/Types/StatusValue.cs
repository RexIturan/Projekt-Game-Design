using System;
using UnityEngine;
using Util.Types;

namespace Characters {
	[Serializable]
	public class StatusValue {
		[SerializeField] private string _name;
		[SerializeField] private StatusType _type;
		
		[SerializeField] private int _value;
		[SerializeField] private int _max;
		[SerializeField] private int _min;
		
		public event Action OnValueChanged = delegate { };

		public string Name { 
			get => _name;
			set { _name = value; } 
		}

		public StatusType Type {
			get => _type;
			set { _type = value; }
		}
		
		public int Value {
			get { return _value; }
			set {
				if ( value < Min ) {
					_value = Min;
				}
				else if( value > Max ) {
					_value = Max;
				}
				else {
					_value = value;
				}
				ValueChanged();
			}
		}

		public int Max {
			get { return _max; }
			set { _max = value < Min ? Min : value; }
		}

		public int Min {
			get { return _min; }
			set { _min = value > Max ? Max : value; }
		}

///// Private Functions ////////////////////////////////////////////////////////////////////////////		

		private void ValueChanged() {
			OnValueChanged?.Invoke();
		}
		
///// Public Functions /////////////////////////////////////////////////////////////////////////////			
			
		public void Increse(int incValue) {
			Value += incValue;
		}

		public void Decrease(int decValue) {
			Value -= decValue;
		}

		public bool IsMax() {
			return Value == Max;
		}

		public bool IsMin() {
			return Value == Min;
		}

		public void Fill() {
			Value = Max;
		}
		
		//todo name, type
		public StatusValue(StatusType type, int min, int value, int max) {
			this.Type = type;
			this.Min = min;
			this.Max = max;
			this.Value = value;
			
			//todo overthink this plz
			this._name = Enum.GetName(typeof(StatusType), type);
			this.Name = _name;
		}

		// make deep copy
		public StatusValue Copy() {
			return new StatusValue(this.Type, this.Min, this.Value, this.Max);
		}

		public RangedInt GetAsRangedInt() {
			return new RangedInt(Min, Max, Value);
		}
	}
}
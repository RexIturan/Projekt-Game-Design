using System;
using UnityEngine;
using Util.Types;

namespace Characters {
	[Serializable]
	public class StatusValue : RangedInt {
		[SerializeField] private StatusType _type;
		
		public string Name { 
			get => name;
			set { name = value; } 
		}

		public StatusType Type {
			get => _type;
			set { _type = value; }
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

		//todo name, type
		public StatusValue(StatusType type, int min, int value, int max) : base(min, max, value) {
			this.Type = type;
			
			//todo overthink this plz
			this.Name = Enum.GetName(typeof(StatusType), type);
		}

		// make deep copy
		public StatusValue Copy() {
			return new StatusValue(this.Type, this.Min, this.Value, this.Max);
		}

		/// <inheritdoc />
		public override bool Equals(object obj) {
			bool equal;
			
			if ( obj is StatusValue otherValue ) {
				equal = this.Type.Equals(otherValue.Type) &&
								this.Min.Equals(otherValue.Min) && 
								this.Max.Equals(otherValue.Max) &&
								this.Value.Equals(otherValue.Value);
			}
			else {
				equal = false;
			}
			
			return equal;
		}
	}
}
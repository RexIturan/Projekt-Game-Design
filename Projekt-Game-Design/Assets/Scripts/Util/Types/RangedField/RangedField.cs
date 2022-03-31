using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Util.Types {
	[Serializable]
	public abstract class RangedField<T> {
		[SerializeField] protected string name;
		[SerializeField] private T _min;
		[SerializeField] private T _max;
		[SerializeField] private T _value;

		public event Action OnValueChanged = delegate { };
		
		public T Value {
			get { return _value; }
			set {
				T newValue;
				
				// value < min
				if ( Smaller(value, Min) ) {
					newValue = Min;
				}
				//value > max
				else if( Grater(value, Max)) {
					newValue = Max;
				}
				else {
					newValue = value;
				}

				if ( !_value.Equals(newValue) ) {
					_value = newValue;
					ValueChanged();
				}
			}
		}

		public T Max {
			get { return _max; }
			set { 
				if(Smaller(value, Min)) {
					_max = Min;
					Value = Min;
				}
				else {
					T newMax = value;
					_max = value;
					if ( Grater(Value, Max) ) {
						Value = Max;
					}
					else if(!newMax.Equals(value))
						ValueChanged();
				}
			}
		}

		public T Min {
			get { return _min; }
			set {
				if(Grater(value, Max)) {
					_min = Max;
					Value = Max;
				}
				else {
					T newMin = value;
					_min = value;
					if ( Smaller(Value, Min) ) {
						Value = Min;
					}
					else if(!_min.Equals(newMin))
						ValueChanged();
				}
			}
		}

		public bool IsFull => Equal(Value, Max);
		public bool IsEmpty => Equal(Value, Min);

///// Private Functions ////////////////////////////////////////////////////////////////////////////		
		
		private void ValueChanged() {
			OnValueChanged?.Invoke();
		}

///// Public Functions /////////////////////////////////////////////////////////////////////////////
 
		public void Fill() {
			Value = Max;
		}
		
		public void Empty() {
			Value = Min;
		}
		
		protected RangedField(T min, T max, T value) {
			Min = min;
			Max = max;
			Value = value;
		}
		
		protected abstract bool Grater(T value, T other);
		protected abstract bool Smaller(T value, T other);
		protected abstract bool Equal(T value, T other);
	}
}
namespace Util.Types {
	public abstract class RangedField<T> {
		private T _min;
		private T _max;
		private T _value;

		public T Value {
			get { return _value; }
			set {
				// value < min
				if ( Smaller(value, Min) ) {
					_value = Min;
				}
				//value > max
				else if( Grater(value, Max)) {
					_value = Max;
				}
				else {
					_value = value;
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
					_max = value;
					if ( Grater(Value, Max) ) {
						Value = Max;
					}
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
					_min = value;
					if ( Smaller(Value, Min) ) {
						Value = Min;
					}
				}
			}
		}

		public bool IsFull => Equal(Value, Max);
		public bool IsEmpty => Equal(Value, Min);

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
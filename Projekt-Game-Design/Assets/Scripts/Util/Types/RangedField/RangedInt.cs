using System;

namespace Util.Types {
	[Serializable]
	public class RangedInt : RangedField<int> {
		public RangedInt(int min, int max, int value) : base(min, max, value) { }
		protected override bool Grater(int value, int other) {
			return value > other;
		}

		protected override bool Smaller(int value, int other) {
			return value < other;
		}

		protected override bool Equal(int value, int other) {
			return value.Equals(other);
		}
	}
}
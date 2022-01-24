using System;
using UnityEngine;

// source: https://forum.unity.com/threads/custom-attributes.359888/

namespace GDP01.Util.Attributes {
	public class MinMaxSliderAttribute : PropertyAttribute {
		private Vector2 _minMax = new Vector2(0,1);
		private Type _type;

		public MinMaxSliderAttribute(float min, float max) {
			_minMax.x = min;
			_minMax.y = max;
		}
		
		public MinMaxSliderAttribute(Type type) {
			_type = type;
		}

		public void SetMinMax(float min, float max) {
			_minMax.x = min;
			_minMax.y = max;
		}
		
		public void SetMinMax(Vector2Int vector) {
			_minMax.x = vector.x;
			_minMax.y = vector.y;
		}

		public Type GetSliderType() {
			return _type;
		}
		
		public float Min
		{
			get { return _minMax.x; }
		}
 
		public float Max
		{
			get { return _minMax.y; }
		}
	}
}
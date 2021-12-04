using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Characters {
	public class Modifier {
		private int _id;
		private Order _order = Order.Middle;
		private Operation _operation = Operation.Add;
		private StackingType _stackingType = StackingType.Linear;
		private StatusType _affectedStatusType = StatusType.None;

		private AffectValuetype _affectValues = AffectValuetype.Value;
		private float value { get; set; }
		private bool stackable { get; set; }
		private int count = 0;
		private int _lifetime = 0;
		private int _roundAdded = 0;

		//todo setter getter

		public static void ApplyMultipliers(List<Modifier> modifiers, StatusValue statusValue) {
			modifiers.Sort((modifier, modifier1) => modifier._order.CompareTo(modifier1));

			foreach ( var modifier in modifiers ) {
				ApplyModifier(modifier, statusValue);
			}
		}
		
		// apply modifier
		public static void ApplyModifier(Modifier modifier, StatusValue statusValue) {
			var modValue = modifier.value;
			
			if ( modifier.stackable ) {
				switch ( modifier._stackingType ) {
					// a = mod value
					// x = mod count
					
					// f(x) = 1 + a * x
					case StackingType.Linear:
						modValue = modifier.value * modifier.count;
						break;

					// f(x) = 1 - 1 / ( 1 + a * x)
					case StackingType.Hyperbloic: 
						modValue = 1 - 1 / (1 + modifier.value * modifier.count);
						break;
					
					//f(x) = a ^ x
					case StackingType.Exponential: 
						modValue = Mathf.Pow(modifier.value , modifier.count);
						break;
				}	
			}

			ChangeStatusValue(modifier, statusValue, modValue);
		}

		private static void ChangeStatusValue(Modifier modifier, StatusValue statusValue, float modValue) {

			if ( modifier._affectValues == AffectValuetype.None ) {
				return;
			}

			//todo dont change statusValue.value directly, cache modified value in statusValue  
			
			switch (modifier._operation) {
				case Operation.Add:
					if ( ( modifier._affectValues & AffectValuetype.Min ) != 0 ) {
						statusValue.min += ( int ) modValue;
					}
					if ( ( modifier._affectValues & AffectValuetype.Max ) != 0 ) {
						statusValue.max += ( int ) modValue;
					}
					if ( ( modifier._affectValues & AffectValuetype.Value ) != 0 ) {
						statusValue.value += ( int ) modValue;
					}
					break;
				case Operation.Multiply:
					if ( ( modifier._affectValues & AffectValuetype.Min ) != 0 ) {
						statusValue.min *= ( int ) modValue;
					}
					if ( ( modifier._affectValues & AffectValuetype.Max ) != 0 ) {
						statusValue.max *= ( int ) modValue;
					}
					if ( ( modifier._affectValues & AffectValuetype.Value ) != 0 ) {
						statusValue.value *= ( int ) modValue;
					}
					break;
				case Operation.Replace:
					if ( ( modifier._affectValues & AffectValuetype.Min ) != 0 ) {
						statusValue.min = ( int ) modValue;
					}
					if ( ( modifier._affectValues & AffectValuetype.Max ) != 0 ) {
						statusValue.max = ( int ) modValue;
					}
					if ( ( modifier._affectValues & AffectValuetype.Value ) != 0 ) {
						statusValue.value = ( int ) modValue;
					}
					break;
			}
		}

		[Flags]
		public enum AffectValuetype {
			None  = 0x0,
			Max   = 0x1,
			Min   = 0x2,
			Value = 0x4,
		}

		public enum Operation {
			Add,
			Multiply,
			Replace
		}

		public enum Order {
			First,
			Middle,
			End,
		}

		public enum StackingType {
			// a = mod value
			// x = mod count

			Linear, // f(x) = 1 + a * x
			Hyperbloic, // f(x) = 1 - 1 / ( 1 + a * x)
			Exponential, // f(x) = a ^ x
		}
	}
}
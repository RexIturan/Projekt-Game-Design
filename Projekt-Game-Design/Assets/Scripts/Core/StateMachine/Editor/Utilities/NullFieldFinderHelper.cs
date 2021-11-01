using System.Collections.Generic;
using UnityEditor;
using UOP1.StateMachine.ScriptableObjects;

namespace UOP1.StateMachine.Editor {
	public static class NullFieldFinderHelper {
		public static bool checkForNullValues(TransitionTableSO transitionTable) {

			List<string> nullValues = new List<string>();
			bool found = false;
			
			for ( int i = 0; i < transitionTable._transitions.Length; i++ ) {
				//check from state
				if ( transitionTable._transitions[i].FromState != null ) {
					if ( checkForNullValuesInState(transitionTable._transitions[i].FromState) ) {
						found = true;
						nullValues.Add($"transitionItem[{i}] , FromState, Actions");
					}	
				}
				else {
					found = true;
					nullValues.Add($"transitionItem[{i}] , fromState");
				}
				
				if ( transitionTable._transitions[i].ToState != null ) {
					if ( checkForNullValuesInState(transitionTable._transitions[i].ToState) ) {
						found = true;
						nullValues.Add($"transitionItem[{i}] , ToState, Actions");	
					}	
				}
				else {
					found = true;
					nullValues.Add($"transitionItem[{i}] , ToState");
				}

				if ( transitionTable._transitions[i].Conditions != null ) {
					if ( checkForNullValuesInConditions(transitionTable._transitions[i]
						.Conditions) ) {
						nullValues.Add($"transitionItem[{i}] , Conditions, condition");
					}
				}
				else {
					found = true;
					nullValues.Add($"transitionItem[{i}] , Conditions");
				}
			}

			return found;
		}

		public static bool checkForNullValuesInState(StateSO state) {
			bool found = false;
			
			var actions = state.GetStateActions;
			for ( int i = 0; i < actions.Length; i++ ) {
				if ( actions[i] == null ) {
					found = true;
				}
			}
			
			return found;
		}
		
		public static bool checkForNullValuesInConditions(TransitionTableSO.ConditionUsage[] conditions) {
			bool found = false;
			for ( int i = 0; i < conditions.Length; i++ ) {
				if ( conditions[i].Condition == null ) {
					found = true;
				}
			}
			return found;
		}

		public static bool checkForNullValuesInConditionsProperty(SerializedProperty conditionsProperty) {
			bool found = false;

			
			
			for ( int i = 0; i < conditionsProperty.arraySize; i++ ) {
				var e = conditionsProperty.GetArrayElementAtIndex(i);
				var c = conditionsProperty.GetArrayElementAtIndex(i).FindPropertyRelative("Condition");
				if (e == null || c == null ) {
					found = true;
				}
			}
			return found;
		}
	}
}
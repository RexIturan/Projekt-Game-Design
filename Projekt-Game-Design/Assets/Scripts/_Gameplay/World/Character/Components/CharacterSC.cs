using Characters.EnemyCharacter.ScriptableObjects;
using UnityEngine;

public class CharacterSC : MonoBehaviour {
	
////////////////////////////////////////// Character State ////////////////////////////////////

	//gameplay character
	public bool isOnTurn; // it's Enemy's turn
	public bool isDone; 

/////////////////////////////////////////// Enemy State ////////////////////////////////////////////

	#region Enemy State

	public EnemyBehaviorSO behavior;

	// public bool abilitySelected;
	// public bool abilityExecuted;
	public bool noTargetFound;
	public bool rangeChecked;

	public void RefillEnemy() {
		isDone = false;
		isOnTurn = false;
	}

	#endregion

//////////////////////////////////////////////////////////////////////////////////////////////
	
}
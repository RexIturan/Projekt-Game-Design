using GDP01._Gameplay.World.Character.Components;
using GDP01._Gameplay.World.Character.Data;
using UnityEngine;


public class CharacterSC : Character<CharacterSC, CharacterData> {
	
////////////////////////////////////////// Character State ////////////////////////////////////

	//gameplay character
	public bool isOnTurn; // it's Enemy's turn
	public bool isDone; 

/////////////////////////////////////////// Enemy State ////////////////////////////////////////////

	#region Enemy State


	public bool noTargetFound;
	public bool rangeChecked;

	public void RefillEnemy() {
		isDone = false;
		isOnTurn = false;
	}

	#endregion

//////////////////////////////////////////////////////////////////////////////////////////////

	public override CharacterData Save() {
		return new CharacterData();
	}

	public override void Load(CharacterData data) {
		Debug.Log("Load CharacterSC");
	}

//////////////////////////////////////////////////////////////////////////////////////////////
}
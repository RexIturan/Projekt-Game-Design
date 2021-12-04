using Characters.EnemyCharacter.ScriptableObjects;
using UnityEngine;

public class CharacterSC : MonoBehaviour {
	
////////////////////////////////////////// Character State ////////////////////////////////////
	
	//gameplay character
	public bool isOnTurn; // it's Enemy's turn
	public bool isDone; 
	
	//ability
	// public bool abilitySelected;
	// public bool abilityConfirmed;
	// public bool abilityExecuted;
	
////////////////////////////////////////// Init Player ////////////////////////////////////

	#region Init Player

	// public PlayerSpawnDataSO playerSpawnData;

	// public void Initialize() {
		// movementPointsPerEnergy = playerSpawnData.movementPointsPerEnergy;

		// RefreshAbilities();
		// RefreshEquipment();
	// }

	#endregion

////////////////////////////////////////// Init Enemy ////////////////////////////////////////////////

	#region Init Enemy

	public EnemyTypeSO enemyType;
	public EnemySpawnDataSO enemySpawnData;

	#endregion

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
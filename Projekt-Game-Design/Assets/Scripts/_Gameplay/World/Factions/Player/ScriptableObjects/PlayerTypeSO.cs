using System.Collections.Generic;
using Characters;
using UnityEngine;

/// <summary>
///player type containing constant initial data
/// for individual types of players (e.g. warrior, mage)
/// </summary> 
[CreateAssetMenu(fileName = "New PlayerType", menuName = "Character/PlayerType")]
public class PlayerTypeSO : ScriptableObject {
	public int id;
	public Sprite icon;
	
	//base prefab
	public GameObject prefab;
	public GameObject modelPrefab;
	public Mesh headModel;
	public Mesh bodyModel;

	//stats
	public List<StatusValue> baseStatusValues;
	//todo save somewhere else
	public int movementPointsPerEnergy;
	public int movementCostPerTile = 1;

	//equipment
	public int startingEquipmentID;
	
	// ability
	public AbilitySO[] basicAbilities; // actions at all time available
}
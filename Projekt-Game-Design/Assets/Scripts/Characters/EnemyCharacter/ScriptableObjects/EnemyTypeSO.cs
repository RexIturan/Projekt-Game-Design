using Characters;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy type containing constant data for individual types of enemies
/// such as stats and drops
/// </summary>
[CreateAssetMenu(fileName = "New EnemyType", menuName = "Character/Enemy/EnemyType")]
public class EnemyTypeSO : ScriptableObject {
	public int id;

	//base prefab
	public GameObject prefab;
	public GameObject modelPrefab;

	//stats
	public List<StatusValue> baseStatusValues;
	//todo save somewhere else
	public int movementPointsPerEnergy;

	//equipment
  public LootTable drops;
	// maybe we want to display what weapon an enemy has (and may drop) when they are clicked 
	// ... but not now
	// public ScriptableObject item; // standard equipped Item 

	// ability
	public AbilitySO[] basicAbilities; // actions at all time available
}

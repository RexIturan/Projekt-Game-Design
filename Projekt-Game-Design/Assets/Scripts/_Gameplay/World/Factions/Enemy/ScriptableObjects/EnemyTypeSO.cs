using Characters;
using System.Collections.Generic;
using GDP01.Loot.ScriptableObjects;
using UnityEngine;

/// <summary>
/// Enemy type containing constant data for individual types of enemies
/// such as stats and drops
/// </summary>
[CreateAssetMenu(fileName = "New EnemyType", menuName = "Character/Enemy/EnemyType")]
public class EnemyTypeSO : ScriptableObject {
	public int id;
	public EnemyBehaviorSO behaviour;

	//base prefab
	public GameObject prefab;
	public GameObject modelPrefab;
	public Mesh headModel;
	public Mesh bodyModel;

	//stats
	public List<StatusValue> baseStatusValues;
	//todo save somewhere else
	public int movementPointsPerEnergy;

	//equipment
  // public LootTable drops;
  public LootTableSO lootTable;
	// public ScriptableObject item; // standard equipped Item 

	// ability
	public AbilitySO[] basicAbilities; // actions at all time available
	public ItemSO weapon;
}

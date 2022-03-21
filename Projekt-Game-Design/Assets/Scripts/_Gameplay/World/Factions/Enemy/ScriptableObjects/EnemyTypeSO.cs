using Characters;
using System.Collections.Generic;
using GDP01._Gameplay.World.Character;
using GDP01._Gameplay.World.Character.Data;
using GDP01.Loot.ScriptableObjects;
using UnityEngine;

/// <summary>
/// Enemy type containing constant data for individual types of enemies
/// such as stats and drops
/// </summary>
[CreateAssetMenu(fileName = "NewEnemyType", menuName = "Character/Enemy/EnemyType")]
public class EnemyTypeSO : CharacterTypeSO {
	public EnemyBehaviorSO behaviour;

	//equipment
  // public LootTable drops;
  public LootTableSO lootTable;
	// public ScriptableObject item; // standard equipped Item 

	// ability
	public ItemSO weapon;
	
	public EnemyCharacterData ToData() {
		EnemyCharacterData data = base.ToData<EnemyCharacterData>();
		data.Type = this;
		return data;
	}
}

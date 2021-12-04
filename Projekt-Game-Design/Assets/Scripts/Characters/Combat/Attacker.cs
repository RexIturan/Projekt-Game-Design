using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Combat {
	public class Attacker : MonoBehaviour {
		//todo usefull
		[SerializeField] private Targetable target;
		//todo idk
		[SerializeField] private List<PathNode> tilesInRange;
		[SerializeField] private List<Vector3Int> tileInRangeOfTarget;
		[SerializeField] private bool waitForAttackToFinish = false;
		
		//enemy
		public CharacterList characterList;
	
		public float attackRange;
		public int attackDamage;
		
	
		public void InitializeEnemyCombat() {
			//todo get [range | damage] from equiped items
			// attackRange = enemySpawnData.range;
			// attackDamage = enemySpawnData.attack;
		}
	
		public void StartEnemyCombat() {
			characterList = characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
		}	
		
		//player
		public PlayerCharacterSC playerTarget;
		public EnemyCharacterSC enemyTarget;
	}
}
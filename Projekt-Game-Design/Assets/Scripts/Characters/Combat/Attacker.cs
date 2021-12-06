using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Combat {
	public class Attacker : MonoBehaviour {
		//todo usefull
		[SerializeField] private Targetable target;
		
		[SerializeField] private List<Vector3Int> tileInRangeOfTarget;
		//todo propertys
		public List<PathNode> tilesInRange;
		public bool waitForAttackToFinish = false;
		
		//enemy
		public CharacterList characterList;
	
		public float attackRange;
		public int attackDamage;
		
		//player
		public Targetable playerTarget;
		public Targetable enemyTarget;
		
		
		public void InitializeEnemyCombat() {
			//todo get [range | damage] from equiped items
			// attackRange = enemySpawnData.range;
			// attackDamage = enemySpawnData.attack;
		}
	
		public void StartEnemyCombat() {
			characterList = characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
		}

		public void ClearTilesInRange() {
			tilesInRange.Clear();
		}
	}
}
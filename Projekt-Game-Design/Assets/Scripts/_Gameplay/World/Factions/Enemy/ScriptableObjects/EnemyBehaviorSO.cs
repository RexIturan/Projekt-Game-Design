using GDP01;
using System.Collections.Generic;
using UnityEngine;

/// <summary>container that influences the AI of the Enemy Character</summary>
[CreateAssetMenu(fileName = "New EnemyBehavior", menuName = "Character/Enemy/EnemyBehavior")]
public class EnemyBehaviorSO : ScriptableObject
{
		public List<AIAction> actionPriorities;
		public bool skipIfOutOfRange;
		public int rangeOfInterestMovement; // only characters this far away from the enemy character
																				// are considered in enemy's decisions
		public int keepDistance; // the characters won't come any closer to a player than this distance
}

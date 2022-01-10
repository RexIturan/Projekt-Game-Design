using UnityEngine;

/// <summary>container that influences the AI of the Enemy Character</summary>
[CreateAssetMenu(fileName = "New EnemyBehavior", menuName = "Character/Enemy/EnemyBehavior")]
public class EnemyBehaviorSO : ScriptableObject
{
		// todo: later create BehaviorContainer
    public bool alwaysSkip;
		public int rangeOfInterestMovement; // only characters (players) this far away from the enemy character
																				// are considered in enemy's decisions
		public int keepDistance; // the characters won't come any closer to a player than this distance
}

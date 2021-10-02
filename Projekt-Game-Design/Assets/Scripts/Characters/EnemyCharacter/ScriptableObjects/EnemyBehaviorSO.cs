using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// container that influences the AI of the Enemy Character
//
[CreateAssetMenu(fileName = "New EnemyBehavior", menuName = "Character/Enemy/EnemyBehavior")]
public class EnemyBehaviorSO : ScriptableObject
{
    public bool alwaysSkip;

    // public int attackRange;
    // public int preferredEngagementDistance;
}

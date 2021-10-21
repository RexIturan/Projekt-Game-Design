using UnityEngine;

/// <summary>container that influences the AI of the Enemy Character</summary>
[CreateAssetMenu(fileName = "New EnemyBehavior", menuName = "Character/Enemy/EnemyBehavior")]
public class EnemyBehaviorSO : ScriptableObject
{
    public bool alwaysSkip;
}

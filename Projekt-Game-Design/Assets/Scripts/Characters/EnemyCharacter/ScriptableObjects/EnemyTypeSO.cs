using UnityEngine;

/// <summary>
/// Enemy type containing constant data for individual types of enemies
/// such as stats and drops
/// </summary>
[CreateAssetMenu(fileName = "New EnemyType", menuName = "Character/Enemy/EnemyType")]
public class EnemyTypeSO : ScriptableObject {
    public int id;
    public GameObject prefab;
    [SerializeField] public ScriptableObject item; // standard equipped Item 
    [SerializeField] public CharacterStats stats;
    [SerializeField] public LootTable drops;
    [SerializeField] public AbilitySO[] basicAbilities; // actions at all time available

    [Header("Visuals")]
    public float TIME_OF_ATTACK_ANIMATION;
}

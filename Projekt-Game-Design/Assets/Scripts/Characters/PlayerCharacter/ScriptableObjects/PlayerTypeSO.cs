using UnityEngine;

/// <summary>
///player type containing constant initial data
/// for individual types of players (e.g. warrior, mage)
/// </summary> 
[CreateAssetMenu(fileName = "New PlayerType", menuName = "Character/PlayerType")]
public class PlayerTypeSO : ScriptableObject
{
    public int id;
	public GameObject prefab;
	public CharacterStats stats;
    // todo remove or change
    // public CharacterStats gainPerLevel; // TODO: gain is Linear in this case
    // public ScriptableObject startWeapon; // is not necessarily equipped weapon
    public AbilitySO[] basicAbilities; // actions at all time available

    [Header("Visuals")]
	public GameObject model;
	public Sprite profilePicture;
}

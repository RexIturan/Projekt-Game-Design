using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>CharacterList</c>
/// Is intended as a Component of a GameObject in a Location Scene 
/// stores two lists of GameObjects, players and enemies
/// </summary>
public class CharacterList : MonoBehaviour
{
    [SerializeField] public List<GameObject> playerContainer;
		[SerializeField] public List<GameObject> friendlyContainer;
    [SerializeField] public List<GameObject> enemyContainer;
		[SerializeField] public List<GameObject> deadEnemies;

    public static CharacterList FindInstant() {
	    return GameObject.Find("Characters").GetComponent<CharacterList>();
    }
}

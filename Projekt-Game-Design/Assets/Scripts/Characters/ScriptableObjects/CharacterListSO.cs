using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stores two lists of game objects, players and enemies
// 
[CreateAssetMenu(fileName = "New CharacterList", menuName = "CharacterList")]
public class CharacterListSO : ScriptableObject
{
    [SerializeField] public List<GameObject> playerCharacters;
    [SerializeField] public List<GameObject> enemyCharacters;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stores two lists of game objects, players and enemies
// 
public class CharacterList : MonoBehaviour
{
    [SerializeField] public List<GameObject> playerCharacters;
    [SerializeField] public List<GameObject> enemyCharacters;

    public void Start()
    {
        foreach(GameObject enemy in enemyCharacters)
        {
            enemy.GetComponent<EnemyCharacterSC>().characterList = this;
        }

    }
}

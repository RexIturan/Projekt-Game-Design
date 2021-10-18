using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stores two lists of game objects, players and enemies
// 
public class CharacterList : MonoBehaviour
{
    [SerializeField] public List<GameObject> playerContainer;
    [SerializeField] public List<GameObject> enemyContainer;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to a game object of an item
// saves reference to its scriptable Object 
//
public class Item : MonoBehaviour
{
    [SerializeField] public ItemSO itemSO;
}

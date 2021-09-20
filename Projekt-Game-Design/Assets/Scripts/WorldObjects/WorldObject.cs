using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script attached to each world object
// contains data for current stats and type
//
public class WorldObject : MonoBehaviour
{
    [SerializeField] public WorldObjectTypeSO type;

    [SerializeField] public Vector2Int position;
    [SerializeField] public int lifePoints;
    [SerializeField] public EWorldObjectFlags currentProperties; 
}

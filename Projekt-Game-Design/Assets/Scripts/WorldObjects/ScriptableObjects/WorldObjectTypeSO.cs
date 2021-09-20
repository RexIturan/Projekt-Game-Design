using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// type of world object 
//
[CreateAssetMenu(fileName = "New WorldObject", menuName = "WorldObject/WorldObjectType")]
public class WorldObjectTypeSO : ScriptableObject
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public EWorldObjectFlags initialProperties; // visiable, destructable, etc. 
    [SerializeField] public int maxLifePoints;

    [SerializeField] public object[] actions; // not player actions;
                                              // events with conditions and effect
}

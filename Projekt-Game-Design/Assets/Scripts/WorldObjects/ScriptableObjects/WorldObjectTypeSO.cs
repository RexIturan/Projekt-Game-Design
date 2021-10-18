using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// type of world object 
//
[CreateAssetMenu(fileName = "New WorldObject", menuName = "WorldObject/WorldObjectType")]
public class WorldObjectTypeSO : ScriptableObject {
    // public string name;
    
    public GameObject prefab;
    public EWorldObjectFlags initialProperties; // visiable, destructable, etc. 

    public int maxLifePoints;
    public object[] actions; // not player actions;
                             // events with conditions and effect
}

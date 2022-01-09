using UnityEngine;

// type of world object 
//
[CreateAssetMenu(fileName = "New WorldObject", menuName = "WorldObject/WorldObjectType")]
public class WorldObjectTypeSO : ScriptableObject {
    // public string name;
    
    public GameObject prefab;
    public WorldObjectFlags initialProperties; // visiable, destructable, etc. 

    public int maxLifePoints;
    public object[] actions; // not player actions;
                             // events with conditions and effect
}

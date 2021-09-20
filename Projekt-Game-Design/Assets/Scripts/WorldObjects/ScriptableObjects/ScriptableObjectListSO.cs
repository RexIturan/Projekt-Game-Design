using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WorldObjectList", menuName = "WorldObject/WorldObjectList")]
public class ScriptableObjectListSO : ScriptableObject
{
    [SerializeField] public List<GameObject> worldObjects;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WorldObjectList", menuName = "WorldObject/WorldObjectList")]
public class WorldObjectContainerSO : ScriptableObject
{
    [SerializeField] public List<WorldObjectTypeSO> worldObjects;
}

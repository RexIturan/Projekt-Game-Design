using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stores the costs of an action 
//
[System.Serializable]
public struct Costs
{
    [SerializeField] int lifePoints;
    [SerializeField] int manaPoints;
    [SerializeField] int movementPoints;
    [SerializeField] int energyPoints;
}

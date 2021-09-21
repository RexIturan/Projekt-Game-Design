using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAction
{
    // stores the costs of an action 
    //
    [System.Serializable]
    public struct Costs
    {
        [SerializeField] int lifePoints;
        [SerializeField] int manaPoints;
        [SerializeField] int movementPoints; // additional movement points, 
                                             // some points are always spent depending on the tiles that are to pass
        [SerializeField] int energyPoints;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// General structure to store information about drops
//
[System.Serializable]
public struct LootTable
{
    [SerializeField] public int maxGold;
    [SerializeField] public int minGold;

    [SerializeField] public int experience; 

    [System.Serializable]
    public struct itemDropPair
    {
        [SerializeField] ScriptableObject item;
        [SerializeField] float probability; // value between 0 and 1
    }

    [SerializeField] public itemDropPair [] itemDropList;

}

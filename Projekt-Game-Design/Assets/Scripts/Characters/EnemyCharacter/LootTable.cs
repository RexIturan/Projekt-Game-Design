using UnityEngine;

/// <summary>General structure to store information about drops</summary>
[System.Serializable]
public struct LootTable
{
    [SerializeField] public int maxGold;
    [SerializeField] public int minGold;

    [SerializeField] public int experience; 

    [System.Serializable]
    public struct ItemDropPair
    {
        public ScriptableObject item;
        public float probability; // value between 0 and 1
    }

    [SerializeField] public ItemDropPair [] itemDropList;

}

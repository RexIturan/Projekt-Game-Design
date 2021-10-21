namespace Ability
{
    // stores the costs of an action 
    //
    [System.Serializable]
    public struct Costs
    {
        public int hitPoints;
        public int manaPoints;
        public int movementPoints; // additional movement points, 
                                   // some points are always spent depending on the tiles that are to pass
        public int energyPoints;
    }
}

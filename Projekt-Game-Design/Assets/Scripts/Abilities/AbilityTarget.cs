namespace Ability
{
    // possible targets of a character action
    //
    [System.Serializable, System.Flags]
    public enum AbilityTarget
    {
        Self = 1,
        Ally = 2,
        Enemy = 4,
        Ground = 8,
        WorldObject = 16
    }
}
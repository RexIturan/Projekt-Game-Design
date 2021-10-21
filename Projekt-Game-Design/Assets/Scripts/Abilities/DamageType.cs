namespace Ability
{
    // types for damage/healing
    [System.Serializable]
    public enum DamageType
    {
        Healing, // inverts damage to healing
        Normal,
        Piercing,
        Siege,
        Magic,
        Divine
    }
}
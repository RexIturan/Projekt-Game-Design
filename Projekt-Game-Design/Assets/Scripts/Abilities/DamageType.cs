namespace Ability
{
		//todo could be scriptable object
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
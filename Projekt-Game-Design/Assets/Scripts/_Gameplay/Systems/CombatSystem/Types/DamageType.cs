namespace Ability
{
		//todo could be scriptable object
    // types for damage/healing
    [System.Serializable]
    public enum DamageType {
	    //todo Healing is not a damage type
        Healing, // inverts damage to healing
        Normal,
        Piercing,
        Siege,
        Magic,
        Divine
    }
}
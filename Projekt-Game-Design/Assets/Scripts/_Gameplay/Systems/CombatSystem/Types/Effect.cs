namespace Ability
{
	/// <summary>
	/// effect with information about damage/heal, attribute boni, 
	/// applied status effects and evniromental effects
	/// </summary>
    [System.Serializable]
    public struct Effect {
        public DamageType type;
        public int baseDamage; //todo i dont like this -> // healing if type is HEALING
        //todo list of status value and how they are effected
        public float strengthBonus, dexterityBonus, intelligenceBonus; // Percentage of attributes that is added
        // public List<ScriptableObject> statusEffects; // status effects added to target
        // public ScriptableObject environmentalEffect; // if environment is changed, e.g. tiles types are changed
    }
}
using System.Collections.Generic;
using UnityEngine;
using Ability;
using Level.Grid;
using UOP1.StateMachine.ScriptableObjects;
using Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability/new Ability")]
public class AbilitySO : ScriptableObject {

	public int id;
    public Sprite icon;
		public string abilityName;
    public string description;
		public CharacterAnimation Animation;
		public SoundSO activationSound;
		// todo move to projectile?
		public GameObject projectilePrefab;
		public float timeUntilDamage;
		public bool repeated;
		public bool damaging;

    public TargetRelationship targets; // targetable objects: allies, enemies, etc. 
    public bool moveToTarget; // moves action taker to field of target
		public bool targetableTilesAreCross; // set to true to make the four adjacent tiles the tiles in range 
    public int range; 
    public TileProperties conditions; // restrictions for target in regards of the tiles between
    public int costs; 
    public TargetedEffect [] targetedEffects;
    
    // cooldown
    public int cooldown = 0;
    
    // charges
    //todo ability instance / item instance 

    public List<StateActionSO> selectedActions;
    public List<StateActionSO> executingActions;

///// Properties ///////////////////////////////////////////////////////////////////////////////////    
    
	public bool HasCoolDown => cooldown > 0;
	public int Cooldown => cooldown;

	///// Unity Functions ////////////////////////////////////////////////////////////////////////////// 	
	
    private void OnEnable() {
	    foreach ( var effect in targetedEffects ) {
		    effect.area.InitFromStringPattern();
	    }
    }
    
// #if UNITY_EDITOR
    private void OnValidate() {
	    foreach ( var effect in targetedEffects ) {
		    effect.area.InitFromStringPattern();
	    }
    }
//     private void OnEnable() {
// 	    var abilityContainerGuid = AssetDatabase.FindAssets($"t:{nameof(AbilityContainerSO)}");
//
// 	    foreach ( var containerGuid in abilityContainerGuid ) {
// 		    var containerPath = AssetDatabase.GUIDToAssetPath(containerGuid);
// 		    var abilityContainer = AssetDatabase.LoadAssetAtPath<AbilityContainerSO>(containerPath);
// 			    
// 		    if ( !abilityContainer.abilities.Contains(this) ) {
// 			    abilityContainer.abilities.Add(this);
// 			    abilityContainer.UpdateItemList();
// 		    }  
// 	    }
//     }
// #endif
}

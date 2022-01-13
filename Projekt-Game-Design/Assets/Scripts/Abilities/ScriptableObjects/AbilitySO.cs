using System;
using System.Collections.Generic;
using UnityEngine;
using Ability;
using Ability.ScriptableObjects;
using Level.Grid;
using UnityEditor;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability/new Ability")]
public class AbilitySO : ScriptableObject
{
		public int id;
	
    public Sprite icon;
    public string description;
		public CharacterAnimation Animation;
		public string activationSound;
		public GameObject projectilePrefab;
		public float timeUntilDamage;
		public bool repeated;
		public bool damaging;

    public AbilityTarget targets; // targetable objects: allies, enemies, etc. 
    public bool moveToTarget; // moves action taker to field of target
    public int range; 
    public TileProperties conditions; // restrictions for target in regards of the tiles between
    public int costs; 
    public TargetedEffect [] targetedEffects;

    public List<StateActionSO> selectedActions;
    public List<StateActionSO> executingActions;

    private void OnValidate() {
	    foreach ( var effect in targetedEffects ) {
		    effect.area.InitFromStringPattern();
	    }
    }
    
#if UNITY_EDITOR
    private void OnEnable() {
	    var abilityContainerGuid = AssetDatabase.FindAssets($"t:{nameof(AbilityContainerSO)}");

	    foreach ( var containerGuid in abilityContainerGuid ) {
		    var containerPath = AssetDatabase.GUIDToAssetPath(containerGuid);
		    var abilityContainer = AssetDatabase.LoadAssetAtPath<AbilityContainerSO>(containerPath);
			    
		    if ( !abilityContainer.abilities.Contains(this) ) {
			    abilityContainer.abilities.Add(this);
			    abilityContainer.UpdateItemList();
		    }  
	    }
    }
#endif
}

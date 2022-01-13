using System.Collections.Generic;
using UnityEngine;
using Ability;
using Level.Grid;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability/new Ability")]
public class AbilitySO : ScriptableObject
{
    public Sprite icon;
    public string description;
		public CharacterAnimation Animation;
		public string activationSound;
		public GameObject projectilePrefab;
		public float projectileLivingTime;
		public bool repeated;
		public bool damaging;

    public AbilityTarget targets; // targetable objects: allies, enemies, etc. 
    public bool moveToTarget; // moves action taker to field of target
    public int range; 
    public TileProperties conditions; // restrictions for target in regards of the tiles between
    public int costs; 
    public TargetedEffect [] targetedEffects;
    public int abilityID;

    public List<StateActionSO> selectedActions;
    public List<StateActionSO> executingActions;
}

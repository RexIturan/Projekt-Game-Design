using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ability;
using Grid;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability/new Ability")]
public class AbilitySO : ScriptableObject
{
    public Sprite icon;
    public string description;

    public EAbilityTargetFlags targets; // targetable objects: allies, enemies, etc. 
    public bool moveToTarget; // moves action taker to field of target
    public int range; 
    public ETileFlags conditions; // restrictions for target in regards of the tiles between
    public int costs; 
    public TargetedEffect [] targetedEffects;
    public int abilityID;

    public List<StateActionSO> selectedActions;
    public List<StateActionSO> executingActions;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ability;
using Grid;

[CreateAssetMenu(fileName = "New CharacterAction", menuName = "CharacterAction/CharacterAction")]
public class AbilitySO : ScriptableObject
{
    public EAbilityTargetFlags targets; // targetable objects: allies, enemies, etc. 
    public bool moveToTarget; // moves action taker to field of target
    public int range; 
    public ETileFlags conditions; // restrictions for target in regards of the tiles between
    public int costs; 
    public TargetedEffect [] targetedEffects;
}

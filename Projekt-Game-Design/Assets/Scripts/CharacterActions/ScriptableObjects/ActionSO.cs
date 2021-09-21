using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterAction;

[CreateAssetMenu(fileName = "New CharacterAction", menuName = "CharacterAction/CharacterAction")]
public class ActionSO : ScriptableObject
{
    [SerializeField] public EActionTargetFlags targets; // targetable objects: allies, enemies, etc. 
    [SerializeField] public bool moveToTarget; // moves action taker to field of target
    [SerializeField] public int range; 
    [SerializeField] public EActionReachableFlags conditions; // restrictions for target 
    [SerializeField] public Costs costs; 
    [SerializeField] public TargetedEffect [] targetedEffects;
}

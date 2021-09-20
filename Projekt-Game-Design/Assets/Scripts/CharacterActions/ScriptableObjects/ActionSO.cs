using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterAction", menuName = "CharacterAction/CharacterAction")]
public class ActionSO : ScriptableObject
{
    [SerializeField] public EActionTargetFlags targets; // targetable objects: allies, enemies, etc. 
    [SerializeField] public int range; 
    [SerializeField] public Costs costs; 
    [SerializeField] public TargetedEffect [] targetedEffects;
}

using System.Collections.Generic;
using UnityEngine;

namespace Ability.ScriptableObjects {
    [CreateAssetMenu(fileName = "newAbilityContainerSO", menuName = "Ability/Ability Container", order = 0)]
    public class AbilityContainerSO : ScriptableObject {
        [SerializeField] private List<AbilitySO> abilitys;
    }
}
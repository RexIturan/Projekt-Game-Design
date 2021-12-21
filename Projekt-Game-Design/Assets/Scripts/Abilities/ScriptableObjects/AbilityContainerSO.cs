using System.Collections.Generic;
using UnityEngine;

namespace Ability.ScriptableObjects {
    [CreateAssetMenu(fileName = "newAbilityContainerSO", menuName = "Ability/Ability Container", order = 0)]
    public class AbilityContainerSO : ScriptableObject {
        public List<AbilitySO> abilities;

				public void Awake()
				{
						Debug.Log("Initialising Abilities");
						foreach ( AbilitySO ability in abilities )
						{
								foreach ( TargetedEffect effect in ability.targetedEffects )
								{
										if ( effect.area != null && !effect.area.Init() )
										{
												Debug.LogError("Ability "+ ability.abilityID + 
														" Effect has had invalid pattern! Setting to single target. ");
												effect.area.SetSingleTarget();
										}
								}
						}
				}
		}
}
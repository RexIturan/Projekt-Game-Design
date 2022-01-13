﻿using System.Collections.Generic;
using UnityEngine;

namespace Ability.ScriptableObjects {
	[CreateAssetMenu(fileName = "newAbilityContainerSO", menuName = "Ability/Ability Container",
		order = 0)]
	public class AbilityContainerSO : ScriptableObject {
		public List<AbilitySO> abilities;

		public void InitAbilities() {
			Debug.Log("Initialising Abilities");
			foreach ( AbilitySO ability in abilities ) {
				// pattern initialisation
				//
				foreach ( TargetedEffect effect in ability.targetedEffects ) {
					if ( effect.area != null && !effect.area.IsValid() ) {
						Debug.LogError("Ability " + ability.id +
						               " Effect has had invalid pattern! Setting to single target. ");
						effect.area.SetSingleTarget();
					}
				}

				// timing initialisation
				//
				if(!ability.projectilePrefab) {
					ability.timeUntilDamage = CharacterAnimationController.TimeUntilHit(ability.Animation);
				}
			}
		}

		public void UpdateItemList() {
			//todo remove magic
			for ( int i = 0; i < abilities.Count; ) {
				if ( abilities[i] == null ) {
					abilities.RemoveAt(i);
				}
				else {
					abilities[i].id = i;
					i++;
				}
			}
		}

		private void OnValidate() {
			UpdateItemList();
		}
	}
}
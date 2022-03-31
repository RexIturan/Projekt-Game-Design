﻿using System.Collections.Generic;
using UnityEngine;
using Util.Extensions;

namespace Ability.ScriptableObjects {
	[CreateAssetMenu(fileName = "newAbilityContainerSO", menuName = "Ability/Ability Container",
		order = 0)]
	public class AbilityContainerSO : ScriptableObject {
		public List<AbilitySO> abilities;

		private void UpdateItemList() {
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

///// Public Functions /////////////////////////////////////////////////////////////////////////////		
		
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
				
				//todo define animation data in Animation/
				// timing initialisation
				if(!ability.projectilePrefab) {
					ability.timeUntilDamage = CharacterAnimationController.TimeUntilHit(ability.Animation);
				}
			}
		}

		public AbilitySO TryGetAbility(int id) {
			return abilities.IsValidIndex(id) ? abilities[id] : null;
		}
	}
}
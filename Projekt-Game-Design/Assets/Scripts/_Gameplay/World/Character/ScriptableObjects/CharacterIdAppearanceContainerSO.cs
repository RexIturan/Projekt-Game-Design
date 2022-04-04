using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
		[CreateAssetMenu(fileName = "CharacterIdAppearanceContainer", menuName = "Character/Character Identification Appearance Container")]
    public class CharacterIdAppearanceContainerSO : ScriptableObject
    {
				[SerializeField] private CharacterIdentificationAppearance defaultPlayer;
				// [SerializeField] private CharacterIdentificationAppearance defaultEnemy;

				[SerializeField] private List<CharacterIdentificationAppearance> playerAppearances;
				// [SerializeField] private List<CharacterIdentificationAppearance> enemyAppearances;

				public CharacterIdentificationAppearance GetAppearanceToID(int id) {
						if ( id < playerAppearances.Count && id >= 0 )
								return playerAppearances[id];
						else
								return defaultPlayer;
				}
		}
}

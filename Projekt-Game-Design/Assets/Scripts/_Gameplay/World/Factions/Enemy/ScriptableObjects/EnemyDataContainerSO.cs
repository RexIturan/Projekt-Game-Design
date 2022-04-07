using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.EnemyCharacter.ScriptableObjects {
    [CreateAssetMenu(fileName = "New EnemyDataContainerSO", menuName = "Character/EnemyDataContianerSO")]
    public class EnemyDataContainerSO : ScriptableObject {
        public List<EnemyTypeSO> enemyTypes;
				public List<EnemyBehaviorSO> enemyBehaviours;
				// maybe add one for behavior later on

			#if UNITY_EDITOR
				private void OnValidate() {
					for ( int i = 0; i < enemyTypes.Count; i++ ) {
						if ( enemyTypes[i] is { } ) {
							enemyTypes[i].id = i;
						}
					}
				}
			#endif
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Characters.EnemyCharacter.ScriptableObjects {
    [CreateAssetMenu(fileName = "New EnemyDataContainerSO", menuName = "Character/EnemyDataContianerSO")]
    public class EnemyDataContainerSO : ScriptableObject {
        public List<EnemyTypeSO> enemyTypes;
        public List<EnemySpawnDataSO> enemySpawnData;
				// maybe add one for behavior later on
    }
}
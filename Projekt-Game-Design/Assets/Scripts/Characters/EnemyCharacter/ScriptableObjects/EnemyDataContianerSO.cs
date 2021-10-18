using System.Collections.Generic;
using UnityEngine;

namespace Characters.EnemyCharacter.ScriptableObjects {
    [CreateAssetMenu(fileName = "New EnemyDataContianerSO", menuName = "Character/EnemyDataContianerSO")]
    public class EnemyDataContianerSO : ScriptableObject {
        public List<EnemyTypeSO> EnemyTypes;
        public List<EnemySpawnDataSO> EnemySpawnData;
    }
}
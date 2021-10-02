using System.Collections.Generic;
using UnityEngine;

namespace Characters.ScriptableObjects {
    [CreateAssetMenu(fileName = "newCharacterContainer", menuName = "Character/Character Container", order = 0)]
    public class CharacterContainerSO : ScriptableObject {
        public List<PlayerCharacterSC> playerContainer;
        public List<EnemyCharacterSC> enemyContainer;

        public void FillContainer() {
            playerContainer = new List<PlayerCharacterSC>(FindObjectsOfType<PlayerCharacterSC>());
            enemyContainer = new List<EnemyCharacterSC>(FindObjectsOfType<EnemyCharacterSC>());
        }
    }
}
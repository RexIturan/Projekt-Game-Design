using System.Collections.Generic;
using UnityEngine;

namespace Characters.ScriptableObjects {
    [CreateAssetMenu(fileName = "newCharacterContainer", menuName = "Character/Character Container", order = 0)]
    public class CharacterContainerSO : ScriptableObject {
        public List<GameObject> playerContainer;
        public List<GameObject> enemyContainer;

        public void FillContainer() {
            playerContainer.Clear();
            enemyContainer.Clear();
            var players = new List<PlayerCharacterSC>(FindObjectsOfType<PlayerCharacterSC>());
            foreach (var player in players) {
                playerContainer.Add(player.gameObject);    
            }

            var enemies = new List<EnemyCharacterSC>(FindObjectsOfType<EnemyCharacterSC>());
            foreach (var enemy in enemies) {
                enemyContainer.Add(enemy.gameObject);    
            }
        }
    }
}
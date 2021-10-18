using System.Collections.Generic;
using Characters.EnemyCharacter.ScriptableObjects;
using Characters.PlayerCharacter.ScriptableObjects;
using Characters.ScriptableObjects;
using SaveSystem.SaveForamts;
using UnityEngine;

namespace Characters {
    public class CharacterInitialiser : MonoBehaviour {
        private CharacterList characterList;
        [SerializeField] private Transform playerParent;
        [SerializeField] private Transform enemyParent;
            
        [SerializeField] private EnemyDataContianerSO enemyDataContianerSo;
        [SerializeField] private PlayerDataContainerSO playerDataContainerSo;

        public void Initialise(List<PC_Save> saveDataPlayers, List<Enemy_Save> saveDataEnemys) {
            characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
            
            characterList.enemyContainer.Clear();
            characterList.playerContainer.Clear();
            
            //todo remove this
            playerParent = GameObject.Find("Characters/players").transform;
            enemyParent = GameObject.Find("Characters/enemys").transform;
            
            // throw new System.NotImplementedException();
            foreach (var playerSave in saveDataPlayers) {
                var type = playerDataContainerSo.playerTypes[playerSave.plyerTypeId];
                var spawnData = playerDataContainerSo.playerpSpawnData[playerSave.plyerSpawnDataId];
                var gameobject = Instantiate(type.prefab, playerParent, true);
                var playerSC = gameobject.GetComponent<PlayerCharacterSC>();
                playerSC.playerType = type;
                playerSC.playerSpawnData = spawnData;
                playerSC.gridPosition = playerSave.pos;
                playerSC.Initialize();
                characterList.playerContainer.Add(playerSC.gameObject);
            }
            
            foreach (var enemySave in saveDataEnemys) {
                var type = enemyDataContianerSo.EnemyTypes[enemySave.enemyTypeId];
                var spawnData = enemyDataContianerSo.EnemySpawnData[enemySave.enemySpawnDataId];
                var gameobject = Instantiate(type.prefab, enemyParent, true);
                var enemySC = gameobject.GetComponent<EnemyCharacterSC>();
                enemySC.enemyType = type;
                enemySC.enemySpawnData = spawnData;
                enemySC.gridPosition = enemySave.pos;
                enemySC.Initialize();
                characterList.enemyContainer.Add(enemySC.gameObject);
            }
        }
    }
}
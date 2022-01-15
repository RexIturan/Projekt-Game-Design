﻿using System.Collections.Generic;
using Characters.EnemyCharacter.ScriptableObjects;
using Characters.PlayerCharacter.ScriptableObjects;
using SaveSystem.SaveFormats;
using UnityEngine;

namespace Characters {
	public class CharacterInitialiser : MonoBehaviour {
		private CharacterList _characterList;
		[SerializeField] private Transform playerParent;
		[SerializeField] private Transform enemyParent;

		[SerializeField] private EnemyDataContainerSO enemyDataContainerSO;
		[SerializeField] private PlayerDataContainerSO playerDataContainerSo;

		public void Initialise(List<PlayerCharacter_Save> saveDataPlayers,
			List<Enemy_Save> saveDataEnemys) {
			_characterList = CharacterList.FindInstant();

			if ( _characterList ) {
				_characterList.enemyContainer.Clear();
				_characterList.playerContainer.Clear();

				//todo remove this
				playerParent = GameObject.Find("Characters/players").transform;
				enemyParent = GameObject.Find("Characters/enemys").transform;

				// throw new System.NotImplementedException();
				foreach ( var playerSave in saveDataPlayers ) {
					var type = playerDataContainerSo.playerTypes[playerSave.plyerTypeId];
					// var spawnData = playerDataContainerSo.playerSpawnData[playerSave.plyerSpawnDataId];
					var obj = Instantiate(type.prefab, playerParent, true);
					var playerSC = obj.GetComponent<PlayerCharacterSC>();
					playerSC.playerType = type;
					playerSC.InitializeFromSave(playerSave);

					if (playerSC.active)
						_characterList.playerContainer.Add(playerSC.gameObject);
					else
						_characterList.friendlyContainer.Add(playerSC.gameObject);
				}

				foreach ( var enemySave in saveDataEnemys ) {
					var type = enemyDataContainerSO.enemyTypes[enemySave.enemyTypeId];
					// var spawnData = enemyDataContainerSO.enemySpawnData[enemySave.enemySpawnDataId];
					var obj = Instantiate(type.prefab, enemyParent, true);
					var enemySC = obj.GetComponent<EnemyCharacterSC>();
          var enemyGridTransform = obj.GetComponent<GridTransform>();
					enemySC.enemyType = type;
					enemySC.InitializeFromSave(enemySave);
					_characterList.enemyContainer.Add(enemySC.gameObject);
				}
			}
		}
	}
}
using System.Collections.Generic;
using Characters.EnemyCharacter.ScriptableObjects;
using Characters.PlayerCharacter.ScriptableObjects;
using Characters.Types;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using GDP01._Gameplay.World.Character.Data;
using SaveSystem.SaveFormats;
using UnityEngine;
using Util.Extensions;

namespace Characters {
	public class CharacterInitialiser : MonoBehaviour {
		
		[SerializeField] private Transform playerParent;
		[SerializeField] private Transform enemyParent;

		[SerializeField] private EnemyDataContainerSO enemyDataContainerSO;
		[SerializeField] private PlayerDataContainerSO playerDataContainerSo;

		[SerializeField] private CharacterIdAppearanceContainerSO idAppearanceContainerSO;

		private CharacterManager CharacterManager => GameplayProvider.Current.CharacterManager;
		

		/// Playercharcter = PC | EnemyCharacter = EC  
		public void Initialise(List<PlayerCharacter_Save> saveDataPlayers,
			List<Enemy_Save> saveDataEnemys) {

			if ( CharacterManager ) {

				CharacterManager.ClearPlayerCharacters();
				CharacterManager.ClearEnemyCharacters();
				
				//laod PC
				List<PlayerCharacterData> playerData = new List<PlayerCharacterData>();
				foreach ( var playerSave in saveDataPlayers ) {
					
					var type = playerDataContainerSo.playerTypes[playerSave.plyerTypeId];

					// var spawnData = playerDataContainerSo.playerSpawnData[playerSave.plyerSpawnDataId];
					var obj = Instantiate(type.prefab);
					var playerSC = obj.GetComponent<PlayerCharacterSC>();
					playerSC.Type = type;
					playerSC.InitializeFromSave(playerSave);

					InitAppearance(obj, idAppearanceContainerSO.GetAppearanceToID(playerSC.id));

					CharacterManager.AddPlayerCharacter(playerSC);
				}
				
				
				//laod EC
				foreach ( var enemySave in saveDataEnemys ) {
					var type = enemyDataContainerSO.enemyTypes[enemySave.enemyTypeId];
					// var spawnData = enemyDataContainerSO.enemySpawnData[enemySave.enemySpawnDataId];
					var obj = Instantiate(type.prefab);
					var enemySC = obj.GetComponent<EnemyCharacterSC>();
					var enemyGridTransform = obj.GetComponent<GridTransform>();
					enemySC.Type = type;
					enemySC.InitializeFromSave(enemySave);
					CharacterManager.AddEnemyCharacter(enemySC);
				}
			}
			
			//
			// if ( _characterList ) {
			// 	_characterList.enemyContainer.ClearGameObjectReferences();
			// 	_characterList.playerContainer.ClearGameObjectReferences();
   //
			// 	//todo remove this
			// 	playerParent = GameObject.Find("Characters/players").transform;
			// 	enemyParent = GameObject.Find("Characters/enemys").transform;
   //
			// 	// throw new System.NotImplementedException();
			// 	foreach ( var playerSave in saveDataPlayers ) {
			// 		var type = playerDataContainerSo.playerTypes[playerSave.plyerTypeId];
			// 		// var spawnData = playerDataContainerSo.playerSpawnData[playerSave.plyerSpawnDataId];
			// 		var obj = Instantiate(type.prefab, playerParent, true);
			// 		var playerSC = obj.GetComponent<PlayerCharacterSC>();
			// 		playerSC.Type = type;
			// 		playerSC.InitializeFromSave(playerSave);
   //
			// 		if (playerSC.active)
			// 			_characterList.playerContainer.Add(playerSC.gameObject);
			// 		else
			// 			_characterList.friendlyContainer.Add(playerSC.gameObject);
			// 	}
   //
			// 	foreach ( var enemySave in saveDataEnemys ) {
			// 		var type = enemyDataContainerSO.enemyTypes[enemySave.enemyTypeId];
			// 		// var spawnData = enemyDataContainerSO.enemySpawnData[enemySave.enemySpawnDataId];
			// 		var obj = Instantiate(type.prefab, enemyParent, true);
			// 		var enemySC = obj.GetComponent<EnemyCharacterSC>();
   //        var enemyGridTransform = obj.GetComponent<GridTransform>();
			// 		enemySC.Type = type;
			// 		enemySC.InitializeFromSave(enemySave);
			// 		_characterList.enemyContainer.Add(enemySC.gameObject);
			// 	}
			// }
		}

		private void InitAppearance(GameObject character, CharacterIdentificationAppearance appearance) {
			Statistics stats = character.GetComponent<Statistics>();
			stats.DisplayName = appearance.CharacterName;
			stats.DisplayImage = appearance.CharacterIcon;
		}
	}
}

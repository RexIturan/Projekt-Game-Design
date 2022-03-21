using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Equipment.ScriptableObjects;
using Events.ScriptableObjects;
using FullSerializer;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character.Data;
using GDP01.Gameplay.SaveTypes;
using GDP01.Structure;
using GDP01.Structure.Provider;
using SaveSystem.V2.Data;
using UnityEngine;
using Util.Extensions;

namespace GDP01._Gameplay.World.Character {
	public struct CharacterManagerData {
		public List<PlayerCharacterData> PlayerCharacterDataList { get; set; }
		public List<EnemyCharacterData> EnemyCharacterDataList { get; set; }
	}
	
	public class CharacterManager : SaveObjectManager, ISaveState<CharacterManagerData> {
		
		[SerializeField, fsIgnore] private Transform playerCharacterParent;
		[SerializeField, fsIgnore] private Transform enemyCharacterParent;
		
		[SerializeField, fsIgnore] private PlayerTypeSO defaultPlayerData;
		[SerializeField, fsIgnore] private EnemyTypeSO defaultEnemyData;
		
		// Player Character
		[SerializeField, fsIgnore] private List<PlayerCharacterSC> playerCharacterComponents;
		[SerializeField, fsIgnore] private List<PlayerCharacterData> _playerCharacterData;
		
		
		// [SerializeField, fsIgnore] private List<CharacterSC> nonPlayerCharacters;
		
		[SerializeField, fsIgnore] private List<EnemyCharacterSC> enemyCharacterComponents;
		[SerializeField, fsIgnore] private List<EnemyCharacterData> _enemyCharacterData;
		
		//char data
		
		// [SerializeField] private List<CharacterData> _nonPlayerCharacterData;
		// [SerializeField] private List<AiCharacterData> _aiCharacterData;

		[Header("Recieving Events On"), SerializeField] private IntEventChannelSO useConnectorEC;
		
///// Properties ///////////////////////////////////////////////////////////////////////////////////
		
		private EquipmentContainerSO EquipmentContainerSO => GameplayDataProvider.Current.EquipmentContainerSO;
		private LevelManager LevelManager => StructureProvider.Current.LevelManager;

///// Public Functions /////////////////////////////////////////////////////////////////////////////
	
///// Player ////////////////////////////////////////////////////////////////////////////////////////
		#region Player Character
		
		public List<PlayerCharacterData> SavePlayerCharData() {

			return SaveComponents(playerCharacterComponents, _playerCharacterData);
			
			// List<PlayerCharacterData> playerDatas = new List<PlayerCharacterData>();
			// playerDatas = _playerCharacterData;
			// foreach ( var player in playerCharacterComponents ) {
			// 	var data = player.Save();
			//
			// 	var existingIndex = playerDatas.FindIndex(characterData => characterData.Id == player.id);
			// 	
			// 	if ( existingIndex != -1 ) {
			// 		playerDatas[existingIndex] = data;
			// 	}
			// 	else {
			// 		playerDatas.Add(data);	
			// 	}
			// }
			// return playerDatas;
		}
		
		public void LoadPlayerCharacterData(List<PlayerCharacterData> playerCharactersData) {
			
			//clear playerchars
			playerCharacterComponents.ClearGameObjectReferences();
			playerCharacterComponents = new List<PlayerCharacterSC>();
			
			_playerCharacterData = playerCharactersData;

			//todo ID check
			//todo equipment check
			
			playerCharactersData.Sort((data, second) => data.Id.CompareTo(second.Id));
			
			foreach ( var playerData in playerCharactersData ) {
				
				// create player
				playerData.Prefab = playerData?.Type?.prefab ?? defaultPlayerData.prefab;
				PlayerCharacterSC player = CreatePlayerCharacter(playerData);

				// check if player is in the current level
				if( playerData.LocationName == null || 
				    LevelManager.IsCurrentLevel(playerData.LocationName) || 
				    playerData.LocationName.Equals(String.Empty)) {
					
					playerCharacterComponents.Add(player);	
				}
				else {
					Destroy(player.gameObject);
				}
			}
		}

		[ContextMenu("Add Player")]
		private void AddPlayerCharacter() {
			var data = defaultPlayerData.ToData();
			
			//todo refactor get next playerchar id
			data.Id = playerCharacterComponents.Count + _playerCharacterData.Count;
			playerCharacterComponents.Add(CreatePlayerCharacter(data));
		}

		private PlayerCharacterSC CreatePlayerCharacter(PlayerCharacterData data) {
			PlayerCharacterSC playerSC = PlayerCharacterSC.CreateAndLoad(data);

			//todo better id check
			//check and set character ID
			if ( data.Id < playerCharacterComponents.Count ) {
				data.Id = playerCharacterComponents.Count;
			}

			playerSC.transform.SetParent(playerCharacterParent ? playerCharacterParent : transform);
			return playerSC;
		}
		
		#endregion
		
///// Enemy ////////////////////////////////////////////////////////////////////////////////////////
		#region Enemy Character

		public List<EnemyCharacterData> SaveEnemyCharacterData() {
			return SaveComponents(enemyCharacterComponents, _enemyCharacterData);
			
			// List<EnemyCharacterData> enemyCharacterDatas = new List<EnemyCharacterData>();
			// enemyCharacterDatas = _enemyCharacterData;
			// foreach ( var enemy in enemyCharacterComponents ) {
			// 	var data = enemy.Save();
			//
			// 	//override existing data if ids are matching
			// 	var existingIndex = enemyCharacterDatas.FindIndex(characterData => characterData.Id == enemy.id);
			// 	
			// 	if ( existingIndex != -1 ) {
			// 		enemyCharacterDatas[existingIndex] = data;
			// 	}
			// 	else {
			// 		enemyCharacterDatas.Add(data);	
			// 	}
			// }
			// return enemyCharacterDatas;
		}
		
		public void LoadEnemyCharacterData(List<EnemyCharacterData> enemyCharacterDatas) {
			
			LoadComponents(ref enemyCharacterComponents,
				ref _enemyCharacterData,
				enemyCharacterDatas,
				defaultEnemyData.prefab,
				enemyCharacterParent);
			
			//clear playerchars
			// enemyCharacterComponents.ClearGameObjectReferences();
			// enemyCharacterComponents = new List<EnemyCharacterSC>();
			//
			// _enemyCharacterData = enemyCharacterDatas ?? new List<EnemyCharacterData>();
			//
			// //todo ID check
			// //todo equipment check
			//
			// //todo could be: if ( _enemyCharacterData == null ) return;
			// if ( _enemyCharacterData != null ) {
			// 	foreach ( var enemyData in _enemyCharacterData ) {
			// 	
			// 		// create player
			// 		enemyData.Prefab = enemyData?.Type?.prefab ?? defaultEnemyData.prefab;
			// 		EnemyCharacterSC enemy = CreateEnemyCharacter(enemyData);
			//
			// 		enemyCharacterComponents.Add(enemy);
			// 	}	
			// }
		}

		[ContextMenu("Add Enemy")]
		private void AddEnemyCharacter() {
			var data = defaultEnemyData.ToData();
			
			//todo refactor get next playerchar id
			data.Id = enemyCharacterComponents.Count + _enemyCharacterData?.Count ?? 0;
			enemyCharacterComponents.Add(CreateComponent<EnemyCharacterSC, EnemyCharacterData>(data, enemyCharacterParent));
		}
		
		// private EnemyCharacterSC CreateEnemyCharacter(EnemyCharacterData data) {
		// 	EnemyCharacterSC enemy = EnemyCharacterSC.CreateAndLoad(data);
		// 	enemy.transform.SetParent(enemyCharacterParent != null ? enemyCharacterParent : transform);
		// 	//todo local or global count??
		// 	enemy.id = enemyCharacterComponents.Count;
		// 	return enemy;
		// }

		#endregion
		

		//todo update character visuals

///// Callbacks ////////////////////////////////////////////////////////////////////////////////////		
		
		private void HandleBeforeUseConnector(int connectorId) {
			
			var connector = LevelManager.CurrentLevel.Connectors.FirstOrDefault(connector => connector.Id == connectorId);
			if ( connector is { IsExit: true } && connector.Target != null ) {
				var locationData = LevelManager.GetEnteringLocationData(connectorId);
				
				//todo which characters should use the connector?
				//1. move all player characters
				foreach ( var player in playerCharacterComponents ) {
					player.ConnectorId = locationData.id;
					player.LocationName = locationData.name;
					player.EnterNewLocation = true;
				}
			}
		}
		
///// Save Load ////////////////////////////////////////////////////////////////////////////////////
		
		public CharacterManagerData Save() {
			// todo Implement
			// todo build
			// - player char data
			// - npc char data
			// - enemy char data
			// _playerCharacterData = SavePlayerCharData();
			
			return new CharacterManagerData {
				PlayerCharacterDataList = SavePlayerCharData(),
				EnemyCharacterDataList = SaveEnemyCharacterData()
			};
		}

		public void Load(CharacterManagerData data) {
			// todo Implement
			//todo create Game Objects from:
			// - player char data
			// - npc char data
			// - enemy char data
			
			if(data.PlayerCharacterDataList != null)
				LoadPlayerCharacterData(data.PlayerCharacterDataList);
			
			if(data.EnemyCharacterDataList != null)
				LoadEnemyCharacterData(data.EnemyCharacterDataList);
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void OnEnable() {
			useConnectorEC.BeforeEventRaised += HandleBeforeUseConnector;
		}

		private void OnDisable() {
			useConnectorEC.BeforeEventRaised -= HandleBeforeUseConnector;
		}
	}
}
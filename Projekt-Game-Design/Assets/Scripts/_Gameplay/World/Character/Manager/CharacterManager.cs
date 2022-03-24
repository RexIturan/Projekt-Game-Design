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
	public partial class CharacterManager : SaveObjectManager, ISaveState<CharacterManger.Data> {
		
		[Header("Recieving Events On"), SerializeField] private IntEventChannelSO useConnectorEC;
		
///// Properties ///////////////////////////////////////////////////////////////////////////////////
		
		private EquipmentContainerSO EquipmentContainerSO => GameplayDataProvider.Current.EquipmentContainerSO;
		private LevelManager LevelManager => StructureProvider.Current.LevelManager;

///// Public Functions /////////////////////////////////////////////////////////////////////////////
	

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
		
		public CharacterManger.Data Save() {
			// todo Implement
			// todo build
			// - player char data
			// - npc char data
			// - enemy char data
			// _playerCharacterData = SavePlayerCharData();

			if ( _characterList is { } ) {
				ConvertPlayerCharacter();
				ConvertEnemyCharacter();
			}
			
			return new CharacterManger.Data {
				PlayerCharacterDataList = SavePlayerCharData(),
				EnemyCharacterDataList = SaveEnemyCharacterData()
			};
		}

		public void Load(CharacterManger.Data data) {
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
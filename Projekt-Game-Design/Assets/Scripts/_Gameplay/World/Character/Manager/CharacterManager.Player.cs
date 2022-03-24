using System;
using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.World.Character.Data;
using UnityEngine;
using Util.Extensions;

namespace GDP01._Gameplay.World.Character {
	public partial class CharacterManager {
		[Header("Player Character Data")]
		[SerializeField, fsIgnore] private Transform playerCharacterParent;
		[SerializeField, fsIgnore] private PlayerTypeSO defaultPlayerData;
		[SerializeField, fsIgnore] private List<PlayerCharacterSC> playerCharacterComponents;
		[SerializeField, fsIgnore] private List<PlayerCharacterData> _playerCharacterData;
		
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
	}
}
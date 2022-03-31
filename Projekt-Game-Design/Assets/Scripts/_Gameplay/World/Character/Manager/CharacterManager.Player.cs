using System;
using System.Collections.Generic;
using System.Linq;
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
		#region Player Character Save/Laod/Create
		
		public List<PlayerCharacterData> SavePlayerCharData() {
			return SaveComponents(playerCharacterComponents, _playerCharacterData);
		}
		
		public void LoadPlayerCharacterData(List<PlayerCharacterData> playerCharactersData) {
			//clear playerchars
			playerCharacterComponents.ClearMonoBehaviourGameObjectReferences();
			playerCharacterComponents = new List<PlayerCharacterSC>();
			
			_playerCharacterData = playerCharactersData;

			//todo ID check
			//todo equipment check
			
			playerCharactersData.Sort((data, second) => data.Id.CompareTo(second.Id));
			
			foreach ( var playerData in playerCharactersData ) {
				
				// create player
				playerData.Prefab = playerData?.Type?.prefab ?? defaultPlayerData.prefab;
				PlayerCharacterSC player = CreatePlayerCharacterComponent(playerData);

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

		[ContextMenu("Add New Player")]
		private void AddNewPlayerCharacter() {
			playerCharacterComponents.Add(CreatePlayerCharacter(defaultPlayerData));
		}

		public void AddPlayerCharacter(PlayerCharacterSC playerComponent) {
			playerComponent.transform.SetParent(playerCharacterParent ? playerCharacterParent : transform);
			playerComponent.id = playerCharacterComponents.Count; 
			playerCharacterComponents.Add(playerComponent);
		}

		private PlayerCharacterSC CreatePlayerCharacter(PlayerTypeSO playerTypeSO) {
			var data = defaultPlayerData.ToData();
			data.Id = playerCharacterComponents.Count + _playerCharacterData.Count;
			return CreatePlayerCharacterComponent(data);
		}
		
		private PlayerCharacterSC CreatePlayerCharacterComponent(PlayerCharacterData data) {
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

		public void ActivatePlayerCharacterAt(Vector3Int gridPos) {
			var playerToActivate = GetPlayerAtPos(gridPos);
			if ( playerToActivate is { IsActive: false } ) {
				playerToActivate.Activate();
			}
		}

		public PlayerCharacterSC GetPlayerAtPos(Vector3Int gridPos) {
			return playerCharacterComponents.FirstOrDefault(player => player.GridPosition.Equals(gridPos));
		}
		
		public PlayerCharacterSC GetPlayerAtPos(Vector3 worldPos) {
			var found = playerCharacterComponents.FirstOrDefault(
				player => {
					var otherPos = _gridData.GetGridPos3DFromWorldPos(worldPos);
					return player.GridPosition.Equals(otherPos);
				});
			return found;
		}

		public IEnumerable<PlayerCharacterSC> GetPlayerCharactersWhere(Func<PlayerCharacterSC, bool> predicate) {
			return playerCharacterComponents.Where(predicate);
		}

		public List<PlayerCharacterSC> GetPlayerCharacters() {
			return playerCharacterComponents;
		}

		public void ClearPlayerCharacters() {
			playerCharacterComponents.ClearMonoBehaviourGameObjectReferences();
			_playerCharacterData.Clear();
		}

		public void AddPlayerCharacterAt(PlayerTypeSO playerType, Vector3 worldPosition) {
			var playerAtPos = GetPlayerAtPos(worldPosition); 
			if(playerAtPos == null) {
				var playerComponent = CreatePlayerCharacter(playerType);
      	playerComponent.GridTransform.MoveTo(worldPosition);
        playerCharacterComponents.Add(playerComponent);
			}
		}

		public void RemovePlayerCharacterAt(Vector3 worldPos) {
			var player = GetPlayerAtPos(worldPos);
			if ( player is { } ) {
				playerCharacterComponents.Remove(player);
				Destroy(player.gameObject);
			}
		}

		public PlayerCharacterSC GetFirstPlayerCharacter() {
			return playerCharacterComponents[0];
		}
	}
}
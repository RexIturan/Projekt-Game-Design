using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

namespace GDP01._Gameplay.World.Character {
	public partial class CharacterManager {
		[Header("V1 Data"), SerializeField, fsIgnore] private CharacterList _characterList;

		private void ConvertPlayerCharacter() {
			if ( _characterList is { } ) {
				foreach ( var obj in _characterList.playerContainer ) {
					var playerComponent = obj.GetComponent<PlayerCharacterSC>();
					playerCharacterComponents.Add(playerComponent);
				}

				foreach ( var obj in _characterList.friendlyContainer ) {
					var playerComponent = obj.GetComponent<PlayerCharacterSC>();
					playerCharacterComponents.Add(playerComponent);
				}
			}
		}

		private void ConvertEnemyCharacter() {
			if ( _characterList is { } ) {
				foreach ( var obj in _characterList.enemyContainer ) {
					var enemyCharacterSC = obj.GetComponent<EnemyCharacterSC>();
					enemyCharacterComponents.Add(enemyCharacterSC);
				}

				foreach ( var obj in _characterList.deadEnemies ) {
					var enemyCharacterSC = obj.GetComponent<EnemyCharacterSC>();
					enemyCharacterComponents.Add(enemyCharacterSC);
				}
			}
		}
	}
}
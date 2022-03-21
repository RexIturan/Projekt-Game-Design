using System;
using UnityEngine;
using Util;

namespace Level.Grid.CharacterGrid {
	[Serializable]
	public class CharacterGrid : GenericGrid1D<CharPlaceholder> {
		public CharacterGrid(
			int width, int depth, float cellSize, Vector3 originPosition) : 
			base(width, depth, cellSize,
			originPosition, (grid, x, y) => new CharPlaceholder(), false) { }
	}

	[Serializable]
	public class CharPlaceholder {
		// public Faction faction;
		// public PlayerCharacterSC playerData;
		// public EnemyCharacterSC enemyData;
		//
		// public void SetCharData(Faction faction,
		// 	PlayerCharacterSC playerCharacterSC = null,
		// 	EnemyCharacterSC enemyCharacterSC = null) {
		// 	this.faction = faction;
		// 	playerData = playerCharacterSC;
		// 	enemyData = enemyCharacterSC;
		// }
	}
}
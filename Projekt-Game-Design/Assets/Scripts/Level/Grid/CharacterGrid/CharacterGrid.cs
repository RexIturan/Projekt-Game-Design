using System;
using UnityEngine;
using Util;

namespace Level.Grid.CharacterGrid {
	public class CharacterGrid : GenericGrid1D<CharPlaceholder> {
		public CharacterGrid(
			int width, int height, float cellSize, Vector3 originPosition) : 
			base(width, height, cellSize,
			originPosition, (grid, x, y) => new CharPlaceholder(), false) { }
	}

	public class CharPlaceholder {
		public Faction faction;
		public PlayerCharacterSC playerData;
		public EnemyCharacterSC enemyData;

		public void SetCharData(Faction faction,
			PlayerCharacterSC playerCharacterSC = null,
			EnemyCharacterSC enemyCharacterSC = null) {
			this.faction = faction;
			playerData = playerCharacterSC;
			enemyData = enemyCharacterSC;
		}
	}
}
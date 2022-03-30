using System.Collections.Generic;
using GDP01._Gameplay.World.Character.Data;

namespace GDP01._Gameplay.World.Character {
	public partial class CharacterManager {
		public struct Data {
			public List<PlayerCharacterData> PlayerCharacterDataList { get; set; }
			public List<EnemyCharacterData> EnemyCharacterDataList { get; set; }
		}
	}
}
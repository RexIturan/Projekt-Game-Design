using System;

namespace GDP01._Gameplay.World.Character.Data {
	[Serializable]
	public class EnemyCharacterData : CharacterData {
		public EnemyBehaviorSO AiBehaviour { get; set; }
	}
}
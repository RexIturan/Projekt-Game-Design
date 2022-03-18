using Characters.Types;

namespace SaveSystem.V2.Data {
	public class TurnData : ISaveState {

		public int CurrentTurn { get; set; } = 0;
		public Faction CurrentFaction { get; set; } = Faction.Player;
		
		public void Save() {}
		public void Load() {}
	}
}
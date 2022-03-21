namespace SaveSystem.V2.Data {
	public class LocationData : ISaveState {
		public string Name { get; set; } = "Level-C";

		public LocationData(){}
		
		public LocationData(string name) {
			Name = name;
		}
		
		public void Save() {}
		public void Load() {}
	}
}
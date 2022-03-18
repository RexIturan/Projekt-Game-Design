using System.Collections.Generic;
using FullSerializer;
using GDP01.Structure;
using GDP01.Structure.Provider;

namespace SaveSystem.V2.Data {
	public class SaveData : ISaveState {
		
		[fsIgnore] private LevelManager LevelManager => StructureProvider.Current.LevelManager;
		
		public GameData GameData { get; set; } = new GameData();
		public Dictionary<string, LevelData> LevelData { get; set; } = new Dictionary<string, LevelData>();

		public SaveData() {
			//create default save data
			GameData = new GameData();
			LevelData = new Dictionary<string, LevelData>();
		}
		
		public void Save() {
			GameData.Save();
			// LevelData.ForEach(data => data.Save());
			if (LevelManager.CurrentLevel is {}) {
				LevelData[LevelManager.CurrentLevel.name] = new LevelData().Save();	
			}
		}

		public void Load() {
			GameData.Load();
		}

		public void LoadLevel() {
			string key = LevelManager.CurrentLevel.name;
			if ( LevelData.ContainsKey(key) ) {
				var levelData = LevelData[key];
				levelData.Load(levelData);
			} 
			else {
				//todo laod default save for that level
			}

			GameData.OnLevelLoaded();
		}
	}
}
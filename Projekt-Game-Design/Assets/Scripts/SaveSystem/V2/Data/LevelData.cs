﻿using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using GDP01._Gameplay.World.Character.Data;
using SaveSystem.V2.TestComponents;
using WorldObjects;

namespace SaveSystem.V2.Data {
	public class LevelData : ISaveState<LevelData> {

		[fsIgnore] private CharacterManager CharacterManager => 
			GameplayProvider.Current.CharacterManager;
		
		[fsIgnore] private WorldObjectManager WorldObjectManager => 
			GameplayProvider.Current.WorldObjectManager;
		
		private CollectableManager CollectableManager => GameManagerProvider.Current.CollectableManager;   
		
		public List<CollectableData> Collectabeles { get; set; }
		public List<EnemyCharacterData> EnemyCharacterDatas { get; set; }
		public List<Door.DoorData> Doors;
		public List<SwitchComponent.SwitchData> Switches;
		public List<ItemComponent.ItemData> Items;

		public LevelData Save() {
			if ( CollectableManager is {} )
				Collectabeles = CollectableManager.Save() ?? new List<CollectableData>();
			
			EnemyCharacterDatas = CharacterManager.SaveEnemyCharacterData();
			
			var worldObjectData = WorldObjectManager.Save();
			Doors = worldObjectData.DoorDataList;
			Switches = worldObjectData.SwitchDataList;
			Items = worldObjectData.ItemDataList;
			
			return this;
		}

		public void Load(LevelData data) {
			if ( CollectableManager is {} )
				CollectableManager.Load(Collectabeles);
			
			CharacterManager.LoadEnemyCharacterData(EnemyCharacterDatas);
			
			WorldObjectManager?.Load(new WorldObjectManager.Data(Doors, Switches, Items));
		}
	}
}
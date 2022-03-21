using System;
using System.Linq;
using Characters.Equipment.ScriptableObjects;
using GDP01._Gameplay.World.Character;
using GDP01.Util;
using WorldObjects;
using static GDP01.Util.Singelton.ProviderHelper;

namespace GDP01._Gameplay.Provider {
	public class GameplayDataProvider {
		#region Lazy Singelton
		//https://csharpindepth.com/Articles/Singleton

		private static readonly Lazy<GameplayDataProvider> lazy =
			new Lazy<GameplayDataProvider>(() => new GameplayDataProvider());
		
		public static GameplayDataProvider Current => lazy.Value;
		private GameplayDataProvider() {}
		
		#endregion
		
		//CharacterTypes
		//PlayerTypes
		//EnemyTypes
		private CharacterTypeContainerSO _characterTypeConteinerSO;
		public CharacterTypeContainerSO CharacterTypeContainerSO => LoadAndCache(ref _characterTypeConteinerSO);
		
		private ItemContainerSO _itemContainerSO;
		public ItemContainerSO ItemContainerSO => LoadAndCache(ref _itemContainerSO);

		private WorldObjectTypeContainerSO _worldObjectTypeContainerSO;
		public WorldObjectTypeContainerSO WorldObjectTypeContainerSO =>
			LoadAndCache(ref _worldObjectTypeContainerSO);
		
		private EquipmentContainerSO _equipmentContainerSO;
		public EquipmentContainerSO EquipmentContainerSO => LoadAndCache(ref _equipmentContainerSO);
		
		private InventorySO _inventorySO;
		public InventorySO InventorySO => LoadAndCache(ref _inventorySO);

		
		
		
		// item
		// doors
		// switches
		// char type
		
		public SerializableScriptableObject FindSOByNameAndGuid(string guid, string name) {
			SerializableScriptableObject foundSO = null;

			foundSO = ItemContainerSO.GetItemTypeByGuid(guid) ?? ItemContainerSO.GetItemTypeByName(name);
			if ( foundSO == null ) {
				foundSO = WorldObjectTypeContainerSO.GetItemTypeByGuid(guid) ??
				          WorldObjectTypeContainerSO.GetItemTypeByName(name);
			}

			return foundSO;
		}
	}
}
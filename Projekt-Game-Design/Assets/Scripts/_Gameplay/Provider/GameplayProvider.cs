using System;
using GDP01._Gameplay.World.Character;
using UnityEngine.SceneManagement;
using WorldObjects;
using static GDP01.Util.Singelton.ProviderHelper;

namespace GDP01._Gameplay.Provider {
	public sealed class GameplayProvider {
		#region Lazy Singelton
		//https://csharpindepth.com/Articles/Singleton

		private static readonly Lazy<GameplayProvider> lazy =
			new Lazy<GameplayProvider>(() => new GameplayProvider());
		
		public static GameplayProvider Current => lazy.Value;

		#endregion
		
		private GameplayProvider() {}

		//todo remove when transitioned to new save system
		private CharacterList _characterList;
		public CharacterList CharacterList => TryGetAndCacheComponentOfTypeInActiveScene(ref _characterList);
		
		private CharacterManager _characterManager;
		public CharacterManager CharacterManager => TryGetAndCacheComponentOfTypeInActiveScene(ref _characterManager);

		private WorldObjectManager _worldObjectManager;
		public WorldObjectManager WorldObjectManager => TryGetAndCacheComponentOfTypeInActiveScene(ref _worldObjectManager);
	}
}
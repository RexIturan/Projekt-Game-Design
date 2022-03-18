using System;
using SaveSystem.V2.Component;
using SaveSystem.V2.TestComponents;
using static GDP01.Util.Singelton.ProviderHelper;

namespace SaveSystem.V2 {
	//singelton
	public sealed class GameManagerProvider {
		#region Lazy Singelton
		//https://csharpindepth.com/Articles/Singleton

		private static readonly Lazy<GameManagerProvider> lazy =
			new Lazy<GameManagerProvider>(() => new GameManagerProvider());

		public static GameManagerProvider Current => lazy.Value;
		
		#endregion
		
		private GameManagerProvider() {}
		
		private SaveSystem_V2 _saveSystemV2;
		public SaveSystem_V2 SaveSystem_V2 =>
			TryGetAndCacheObjectOfType(ref _saveSystemV2);

		private CollectableManager _collectableManager;
		public CollectableManager CollectableManager =>
			TryGetAndCacheObjectOfType(ref _collectableManager);
	}
}
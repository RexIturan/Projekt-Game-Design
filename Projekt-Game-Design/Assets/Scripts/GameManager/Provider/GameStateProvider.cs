using System;
using static GDP01.Util.Singelton.ProviderHelper;

namespace GameManager.Provider {
	public class GameStateProvider {
		#region Lazy Singelton
		//https://csharpindepth.com/Articles/Singleton

		private static readonly Lazy<GameStateProvider> lazy =
			new Lazy<GameStateProvider>(() => new GameStateProvider());
		
		public static GameStateProvider Current => lazy.Value;

		#endregion
		
		private GameStateProvider(){}
		
		//should work because gaemsc should be an singelton todo make gamesc a aingelton
		private GameSC _gameSC;
		public GameSC GameSC => TryGetAndCacheObjectOfType(ref _gameSC);

		private GameEndConditionHolder _gameEndConditionHolder;
		public GameEndConditionHolder GameEndConditionHolder =>
			TryGetAndCacheComponentOfTypeInActiveScene(ref _gameEndConditionHolder);
	}
}
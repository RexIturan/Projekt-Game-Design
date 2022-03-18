using System;
using FullSerializer;
using GDP01.Structure;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SaveSystem.V2 {
	//Singelton
	public sealed class GameDataProvider {
		#region Lazy Singelton
		//https://csharpindepth.com/Articles/Singleton

		private static readonly Lazy<GameDataProvider> lazy =
			new Lazy<GameDataProvider>(() => new GameDataProvider());

		public static GameDataProvider Current => lazy.Value;
		
		#endregion
		
		private GameDataProvider() {}
		
		private static T LoadData<T>(string name) {
			return Addressables.LoadAssetAsync<T>(name).WaitForCompletion();
		}

		
		//LevelData Conteiner
		// private LevelDataContainerSO _levelDataContainerSO;
		// public LevelDataContainerSO LevelDataContainer =>
		// 	_levelDataContainerSO ??= 
		// 	LoadData<LevelDataContainerSO>(nameof(LevelDataContainerSO));
		
		
		
		
		//todo remove
		public static Sprite GetSpriteByName(string value) {
			//todo Implement

			//lookup in sprite container?
			//lookup in char icons?
			//todo load addreesable 
			
			return null;
		}
	}
}
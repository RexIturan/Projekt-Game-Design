using System;
using static GDP01.Util.Singelton.ProviderHelper;

namespace GDP01.Structure.Provider {
	public sealed class StructureProvider {
		#region Lazy Singelton
		//https://csharpindepth.com/Articles/Singleton

		private static readonly Lazy<StructureProvider> lazy =
			new Lazy<StructureProvider>(() => new StructureProvider());
		
		public static StructureProvider Current => lazy.Value;

		#endregion
		
		private StructureProvider() {}
		
		//LevelData Conteiner
		private LevelDataContainerSO _levelDataContainerSO;
		public LevelDataContainerSO LevelDataContainer =>
			_levelDataContainerSO ??= LoadData<LevelDataContainerSO>(nameof(LevelDataContainerSO));
		
		private LevelManager _levelManager;
		public LevelManager LevelManager =>
			TryGetAndCacheObjectOfType(ref _levelManager);
	}
}
using System;
using System.Collections.Generic;
using SceneManagement.ScriptableObjects;
using UnityEngine;

namespace SceneManagement.Types {
	
	[Serializable]
	public struct SceneLoadingData {
		
		[Serializable]
		public struct SceneDataDependencySettings {
			public GameSceneSO sceneData;
			public bool reset;
		}
		
		// main scene
		[SerializeField] private GameSceneSO mainSceneData;

		// dependent scenes
		[SerializeField] private List<SceneDataDependencySettings> dependentScenes;
		
		//todo introduce loadingscreenm settings
		[SerializeField] private bool showLoadingScreen;
		[SerializeField] private bool closeLoadingScreenOnLoad;
		[SerializeField] private bool activateOnLoad;
		[SerializeField] private bool dontUnload;
		[SerializeField] private string name;

		///// Properties ///////////////////////////////////////////////////////////////////////////////////
	
		public GameSceneSO MainSceneData => mainSceneData;
		public List<SceneDataDependencySettings> Dependencies => dependentScenes;
		public bool ShowLoadingScreen => showLoadingScreen;
		public bool CloseLoadingScreenOnLoad => closeLoadingScreenOnLoad;
		public bool ActivateOnLoad => activateOnLoad;
		public bool DontUnload => dontUnload;
		public string Name { get => name; set => name = value; }
	}
}
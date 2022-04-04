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
		[SerializeField] private bool updateSave;

		///// Properties ///////////////////////////////////////////////////////////////////////////////////
	
		public GameSceneSO MainSceneData { get => mainSceneData; set => mainSceneData = value;}

		public List<SceneDataDependencySettings> Dependencies {
			get { return dependentScenes; }
			set { dependentScenes = value;  }
		}

		public bool ShowLoadingScreen => showLoadingScreen;
		public bool CloseLoadingScreenOnLoad => closeLoadingScreenOnLoad;
		public bool ActivateOnLoad { get => activateOnLoad; set => activateOnLoad = value; }
		public bool DontUnload { get => dontUnload; set => dontUnload = value;}
		public string Name { get => name; set => name = value; }
		public bool UpdateSave { get => updateSave; set => updateSave = value; }
	}
}
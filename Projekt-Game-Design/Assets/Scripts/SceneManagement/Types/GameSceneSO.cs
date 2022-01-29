using GDP01.Util;
using UnityEngine.AddressableAssets;

namespace SceneManagement.ScriptableObjects {
    /// <summary>
    /// This class is a base class which contains what is common to all game scenes (Locations, Menus, Managers)
    /// </summary>
    public class GameSceneSO : DescriptionBaseSO
    {
        public GameSceneType sceneType;
        public AssetReference sceneReference; //Used at runtime to load the scene from the right AssetBundle
        // public AudioCueSO musicTrack;

        public GameSceneSO[] loadAdditional;
	        
        /// <summary>
        /// Used by the SceneSelector tool to discern what type of scene it needs to load
        /// </summary>
        public enum GameSceneType {
	        //Playable scenes
          Tactics, //SceneSelector tool will also load PersistentManagers and Gameplay
          Menu, //SceneSelector tool will also load Gameplay
          Editor,

          //Special scenes
          Initialisation,
          PersistentManagers,
          Control,
          UI,

          //Work in progress scenes that don't need to be played
          Art,
          Test,
        }
    }
}
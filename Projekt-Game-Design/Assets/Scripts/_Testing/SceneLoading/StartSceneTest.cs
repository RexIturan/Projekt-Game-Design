using GDP01.SceneManagement.EventChannels;
using SceneManagement.Types;
using UnityEngine;

namespace _Testing.SceneLoading {
	public class StartSceneTest : MonoBehaviour {
		[SerializeField] private SceneLoadingInfoEventChannelSO loadSceneEC;
		[SerializeField] private SceneLoadingData sceneLoadingData;
		public void RequesetLoadGame() {
			//load gameScene
			loadSceneEC.RaiseEvent(sceneLoadingData);
		}
	}
}
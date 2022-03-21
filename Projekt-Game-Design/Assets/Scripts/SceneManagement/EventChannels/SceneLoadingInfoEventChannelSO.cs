using System;
using Events.ScriptableObjects.Core;
using SceneManagement.Types;
using UnityEngine;

namespace GDP01.SceneManagement.EventChannels {
	/// <summary>
	/// NEW
	/// This class is a used for scene loading events.
	/// Takes an SceneLoadingInfo Struct, which specifys the details of the loading process.
	/// </summary>
	[CreateAssetMenu(menuName = "Events/Scene Management/ new Load Scene Event Channel")]
	public class SceneLoadingInfoEventChannelSO : EventChannelBaseSO {
		
		public event Action<SceneLoadingData> BeforeLoadingRequested;
		public event Action<SceneLoadingData> OnLoadingRequested;

		public void RaiseEvent(SceneLoadingData sceneLoadingData) {
			BeforeLoadingRequested?.Invoke(sceneLoadingData);
			
			if (OnLoadingRequested != null) {
				OnLoadingRequested.Invoke(sceneLoadingData);
			} else {
				Debug.LogWarning("A Scene loading was requested, but nobody picked it up. " +
				                 "Check why there is no SceneLoader already present, " +
				                 "and make sure it's listening on this Load Event channel.");
			}
		}
		
	}
}
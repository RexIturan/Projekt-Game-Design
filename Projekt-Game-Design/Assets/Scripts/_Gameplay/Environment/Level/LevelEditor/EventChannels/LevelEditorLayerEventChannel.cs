using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace LevelEditor.EventChannels {
	[CreateAssetMenu(fileName = "LevelEditorLayerEC", menuName = "Events/LevelEditor/EditorLayer")]
	public class LevelEditorLayerEventChannel : EventChannelBaseSO {
		public event Action<LevelEditor.LayerType> OnEventRaised;

		public void RaiseEvent(LevelEditor.LayerType layerType) {
			if (OnEventRaised != null)
				OnEventRaised.Invoke(layerType);
		}
	}
}
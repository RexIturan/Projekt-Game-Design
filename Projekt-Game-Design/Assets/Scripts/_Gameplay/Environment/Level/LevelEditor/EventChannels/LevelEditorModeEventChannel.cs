using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace LevelEditor.EventChannels {
	[CreateAssetMenu(fileName = "LevelEditorModeEC", menuName = "Events/LevelEditor/EditorMode")]
	public class LevelEditorModeEventChannel : EventChannelBaseSO {
		public event Action<LevelEditor.EditorMode> OnEventRaised;

		public void RaiseEvent(LevelEditor.EditorMode mode) {
			if (OnEventRaised != null)
				OnEventRaised.Invoke(mode);
		}
	}
}

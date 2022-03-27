using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace LevelEditor.EventChannels {
	[CreateAssetMenu(fileName = "LevelEditorStateEC", menuName = "Events/LevelEditor/StateChanged")]
	public class LevelEditorStateEventChannel : EventChannelBaseSO {
		public event Action<LevelEditor.EditorState> OnEventRaised;

		public void RaiseEvent(LevelEditor.EditorState state) {
			if (OnEventRaised != null)
				OnEventRaised.Invoke(state);
		}
	}
}

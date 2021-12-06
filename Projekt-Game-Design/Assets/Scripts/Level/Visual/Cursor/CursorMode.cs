using System;

namespace LevelEditor {
	[Serializable]
	public enum CursorMode {
		None,
		Select,
		Add,
		Remove,
		Error,
	}
}
using System;
using GDP01.Gameplay.SaveTypes;
using UnityEngine;
using static GDP01.Util.SerializableScriptableObject;

namespace WorldObjects {
	public abstract partial class WorldObject {
		[Serializable]
		public abstract class Data : SaveObjectCreatorData {
			[SerializeField] protected ReferenceData _type = null;
			public ReferenceData Type { get => _type; set => _type = value; }
		}
	}
}
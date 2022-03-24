using System;
using GDP01.Gameplay.SaveTypes;
using UnityEngine;
using static GDP01.Util.SerializableScriptableObject;

namespace WorldObjects {
	public abstract partial class WorldObject {
		[Serializable]
		public abstract class Data : SaveObjectCreatorData {
			[SerializeField] protected ReferenceData referenceData = null;
			public ReferenceData ReferenceData { get => referenceData; set => referenceData = value; }
		}
	}
}
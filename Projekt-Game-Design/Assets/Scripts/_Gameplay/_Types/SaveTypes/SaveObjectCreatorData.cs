using System;
using FullSerializer;
using UnityEngine;

namespace GDP01.Gameplay.SaveTypes {
	[Serializable]
	public abstract class SaveObjectCreatorData {
		[SerializeField] private int id = -1;
		public int Id { get => id; set => id = value; }
		
		[SerializeField] private Vector3Int gridPosition = Vector3Int.zero;
		public Vector3Int GridPosition { get => gridPosition; set => gridPosition = value; }
		
		[SerializeField] private Vector3 gridrotation = Vector3.zero;
		public Vector3 Rotation { get => gridrotation; set => gridrotation = value; }
		
		//todo add type field here, chartype should inherit it		
		//todo has to come from Object Base type
		//todo move to base object type
		[fsIgnore] public GameObject Prefab { get; set; } = null;
	}
}
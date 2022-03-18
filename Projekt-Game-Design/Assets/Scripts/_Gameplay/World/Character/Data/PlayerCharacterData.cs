using System;
using FullSerializer;
using UnityEngine;

namespace GDP01._Gameplay.World.Character.Data {
	[Serializable]
	public class PlayerCharacterData : CharacterData {
		// [SerializeField] protected new PlayerTypeSO _type = null;
		// public new PlayerTypeSO Type { get => _type; set => _type = value; }
		
		// [SerializeField, fsIgnore] protected int equipmentId = -1;
		// public int EquipmentId { get => equipmentId; set => equipmentId = value; }
		
		//location data
		public bool EnteringNewLocation { get; set; } = false;
		public string LocationName { get; set; }
		public int ConnectionID { get; set; } 
	}
}
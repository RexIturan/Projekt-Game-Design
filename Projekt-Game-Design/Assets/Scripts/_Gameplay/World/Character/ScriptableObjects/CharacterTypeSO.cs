using System.Collections.Generic;
using Characters;
using GDP01._Gameplay.World.Character.Data;
using GDP01.Util;
using UnityEngine;

namespace GDP01._Gameplay.World.Character {
	/// <summary>
	/// Default Values For A Character
	/// </summary>
	public abstract class CharacterTypeSO : SerializableScriptableObject {

		//character type id
		public int id;
		public bool active;
		public Sprite icon;
	
		//base prefab
		public GameObject prefab;
		public GameObject modelPrefab;
		public Mesh headModel;
		public Mesh bodyModel;

		//stats
		public List<StatusValue> baseStatusValues;
		//todo save somewhere else
		public int movementPointsPerEnergy = 20;
		public int movementCostPerTile = 1;
	
		// ability
		public AbilitySO[] basicAbilities; // actions at all time available

		protected T ToData<T>() where T : CharacterData, new() {
			return new T {
				Id = id,
				Active = active,
				Prefab = prefab,
				Type = this,
				Stats = new StatusValues().InitValues(baseStatusValues),
			};
		}
		
		// public static explicit operator CharacterData (CharacterTypeSO typeSO) {
		// 	return new CharacterData {
		// 		Id = typeSO.id,
		// 		Active = typeSO.active,
		// 		//todo the rest
		// 	};
		// }  
	}
}
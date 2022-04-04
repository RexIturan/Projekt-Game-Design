using Ability;
using Audio;
using GDP01.Loot.ScriptableObjects;
using UnityEngine;

namespace WorldObjects {

		/// Contains data to set in junk game object
		[CreateAssetMenu(fileName = "newJunkType", menuName = "WorldObjects/Junk/New Junk Type")]
		public class JunkTypeSO : WorldObject.TypeSO {
				public GameObject model;
				public string junkName;
				public string description;

				public SoundSO destructionSound;
				public bool walkThrough;
				public bool destructable;
				public ArmorType armorType;
				public int hitPoints;
				public LootTableSO loot;

				public Junk.JunkData ToComponentData() {
					Junk.JunkData data = base.ToComponentData<Junk.JunkData>();
		
					// playerData.EquipmentId = equipmentID;
					data.ReferenceData = ToReferenceData();
		
					return data;
				}
		}
}
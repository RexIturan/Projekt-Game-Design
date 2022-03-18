using UnityEngine;

namespace WorldObjects
{
		/**
		 * Contains data to set in switch game object
		 */
		[CreateAssetMenu(fileName = "newSwitchType", menuName = "WorldObjects/Switch/New Switch Type")]
		public class SwitchTypeSO : WorldObject.TypeSO
		{
				public GameObject model;
				public string switchName;
				public string description;
				
				public string activationSound;
				//activation range
				public int range;
				
				//todo activation type
				// - char in range
				// - select to activate
				// - ui confirm
				
				//todo show range
				
				//todo add tile flags 
				public bool walkThrough;

				public SwitchComponent.SwitchData ToComponentData() {
					SwitchComponent.SwitchData switchData = base.ToComponentData<SwitchComponent.SwitchData>();
		
					// playerData.EquipmentId = equipmentID;
					switchData.Type = this;
		
					return switchData;
				}
		}
}
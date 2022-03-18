using System.Collections.Generic;
using System.Linq;
using GDP01.Util;
using UnityEngine;

namespace WorldObjects {
	// [CreateAssetMenu(fileName = "WorldObjectTypeContainer", menuName = "WorldObjectTypeContainer", order = 0)]
	public class WorldObjectTypeContainerSO : ScriptableObject {
		public List<DoorTypeSO> doors;
		public List<SwitchTypeSO> switches;
		// public List<JunkTypeSO> junks;
		
		
		public SerializableScriptableObject GetItemTypeByGuid(string guid) {
			return (SerializableScriptableObject) doors.FirstOrDefault(type => type.Guid.Equals(guid)) ?? 
			       switches.FirstOrDefault(type => type.Guid.Equals(guid));
		}

		public SerializableScriptableObject GetItemTypeByName(string name) {
			return (SerializableScriptableObject) doors.FirstOrDefault(type => type.name.Equals(name)) ?? 
			       switches.FirstOrDefault(type => type.name.Equals(name));
		}
	}
}
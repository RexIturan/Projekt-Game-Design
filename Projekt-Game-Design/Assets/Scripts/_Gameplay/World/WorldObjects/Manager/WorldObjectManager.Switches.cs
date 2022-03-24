using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects {
	public partial class WorldObjectManager {
		[Header("Switch Data")]
		[SerializeField] private SwitchTypeSO defaultSwitchTypeSO;
		[SerializeField] private Transform switchParent;
		[SerializeField] private List<SwitchComponent> _switchComponents;

		private List<SwitchComponent.SwitchData> SaveSwitches() {
			return SaveComponents(_switchComponents, managerData.SwitchDataList);
		}

		private void LoadSwitches(List<SwitchComponent.SwitchData> switchDataList) {
			LoadComponents(
				ref _switchComponents, 
				ref managerData.SwitchDataList, 
				switchDataList, 
				defaultSwitchTypeSO.prefab, 
				switchParent);
		}
		
		[ContextMenu("Add Switch")]
		private void AddSwitch() {
			var data = defaultSwitchTypeSO.ToComponentData();
			
			//todo refactor get next playerchar id
			data.Id = _switchComponents.Count + managerData.SwitchDataList?.Count ?? 0;
			_switchComponents.Add(CreateComponent<SwitchComponent, SwitchComponent.SwitchData>(data, switchParent));
		}
	}
}
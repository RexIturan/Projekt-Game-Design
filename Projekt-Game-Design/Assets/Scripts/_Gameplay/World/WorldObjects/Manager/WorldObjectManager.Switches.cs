using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util.Extensions;

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
		private void AddNewSwitch() {
			_switchComponents.Add(CreateSwitch(defaultSwitchTypeSO));
		}

		private SwitchComponent CreateSwitch(SwitchTypeSO switchTypeSO) {
			var data = switchTypeSO.ToComponentData();
      data.Id = _switchComponents.Count + managerData.SwitchDataList?.Count ?? 0;
	    return CreateComponent<SwitchComponent, SwitchComponent.SwitchData>(data, switchParent);
		}

		public SwitchComponent GetSwitchAt(Vector3 worldPos) {
			return GetSwitchAt(_gridData.GetGridPos3DFromWorldPos(worldPos));
		}
		
		public SwitchComponent GetSwitchAt(Vector3Int gridPos) {
			return _switchComponents.FirstOrDefault(component =>
				component.GridPosition.Equals(gridPos));
		}
		
		public void AddSwitchAt(SwitchTypeSO switchType, Vector3 worldPos) {
			if ( GetSwitchAt(worldPos) == null ) {
				var switchComponent = CreateSwitch(switchType);
				switchComponent.GridTransform.MoveTo(worldPos);
				_switchComponents.Add(switchComponent);
			}
		}
		
		public void RemoveSwitchAt(Vector3 worldPos) {
			var switchComp = GetSwitchAt(worldPos);
			if ( switchComp is { } ) {
				_switchComponents.Remove(switchComp);
				Destroy(switchComp.gameObject);
			}
		}

		private void ClearSwitches() {
			_switchComponents.ClearMonoBehaviourGameObjectReferences();
			managerData.SwitchDataList.Clear();
		}

		public void AddSwitch(SwitchComponent switchComponent) {
			switchComponent.transform.SetParent(switchParent ? switchParent : transform);
			switchComponent.Id = _switchComponents.Count;
			_switchComponents.Add(switchComponent);
		}

		public List<SwitchComponent> GetSwitchWhere(Func<SwitchComponent, bool> predicate) {
			return _switchComponents.Where(predicate).ToList();
		}

		public List<SwitchComponent> GetSwitches() {
			return _switchComponents;
		}
	}
}
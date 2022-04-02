using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util.Extensions;

namespace WorldObjects {
	public partial class WorldObjectManager {
		[Header("Junk Data")]
		[SerializeField] private JunkTypeSO defaultJunkTypeSO;
		[SerializeField] private Transform junkParent;
		[SerializeField] private List<Junk> _junkComponents;

		private List<Junk.JunkData> SaveJunks() {
			return SaveComponents(_junkComponents, managerData.JunkDataList);
		}

		private void LoadJunks(List<Junk.JunkData> junkDataList) {
			LoadComponents(
				ref _junkComponents, 
				ref managerData.JunkDataList, 
				junkDataList, 
				defaultJunkTypeSO.prefab, 
				junkParent);
		}
		
		[ContextMenu("Add Junk")]
		private void AddNewJunk() {
			Junk junk = CreateJunk(defaultJunkTypeSO);
			_junkComponents.Add(junk);
			junk.worldObjectManager = this;
		}

		private Junk CreateJunk(JunkTypeSO junkTypeSO) {
			var data = junkTypeSO.ToComponentData();
      data.Id = _junkComponents.Count + managerData.JunkDataList?.Count ?? 0;
	    return CreateComponent<Junk, Junk.JunkData>(data, junkParent);
		}

		public Junk GetJunkAt(Vector3 worldPos) {
			return GetJunkAt(_gridData.GetGridPos3DFromWorldPos(worldPos));
		}
		
		public Junk GetJunkAt(Vector3Int gridPos) {
			return _junkComponents.FirstOrDefault(component =>
				component.GridPosition.Equals(gridPos));
		}
		
		public void AddJunkAt(JunkTypeSO junkType, Vector3 worldPos) {
			if ( GetJunkAt(worldPos) == null ) {
				var junk = CreateJunk(junkType);
				junk.GridTransform.MoveTo(worldPos);
				_junkComponents.Add(junk);
				junk.worldObjectManager = this;
			}
		}
		
		public void RemoveJunkAt(Vector3 worldPos) {
			var junk = GetJunkAt(worldPos);
			if ( junk is { } ) {
				_junkComponents.Remove(junk);
				Destroy(junk.gameObject);
			}
		}

		private void ClearJunks() {
			_junkComponents.ClearMonoBehaviourGameObjectReferences();
			managerData.JunkDataList.Clear();
		}

		public void AddJunk(Junk junk) {
			junk.transform.SetParent(junkParent ? junkParent : transform);
			junk.Id = _junkComponents.Count;
			_junkComponents.Add(junk);
			junk.worldObjectManager = this;
		}

		public List<Junk> GetJunkWhere(Func<Junk, bool> predicate) {
			return _junkComponents.Where(predicate).ToList();
		}

		public List<Junk> GetJunks() {
			return _junkComponents;
		}
	}
}
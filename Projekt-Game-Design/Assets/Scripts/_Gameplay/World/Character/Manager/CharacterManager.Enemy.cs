using System;
using System.Collections.Generic;
using System.Linq;
using FullSerializer;
using GDP01._Gameplay.World.Character.Data;
using UnityEngine;
using Util.Extensions;

namespace GDP01._Gameplay.World.Character {
	public partial class CharacterManager {
		[Header("Enemy Character Data")]
		[SerializeField, fsIgnore] private Transform enemyCharacterParent;
		[SerializeField, fsIgnore] private EnemyTypeSO defaultEnemyData;
		[SerializeField, fsIgnore] private List<EnemyCharacterSC> enemyCharacterComponents;
		[SerializeField, fsIgnore] private List<EnemyCharacterData> _enemyCharacterData;
		
		///// Enemy ////////////////////////////////////////////////////////////////////////////////////////
		#region Enemy Character Save/Load/Create

		public List<EnemyCharacterData> SaveEnemyCharacterData() {
			return SaveComponents(enemyCharacterComponents, _enemyCharacterData);
		}
		
		public void LoadEnemyCharacterData(List<EnemyCharacterData> enemyCharacterDatas) {
			LoadComponents(ref enemyCharacterComponents,
				ref _enemyCharacterData,
				enemyCharacterDatas,
				defaultEnemyData.prefab,
				enemyCharacterParent);
		}

		[ContextMenu("Add Enemy")]
		private void AddNewEnemyCharacter() {
			enemyCharacterComponents.Add(CreateEnemyCharacter(defaultEnemyData));
		}
		
		public void AddEnemyCharacter(EnemyCharacterSC enemyComponent) {
			enemyComponent.transform.SetParent(enemyCharacterParent ? enemyCharacterParent : transform);
			enemyComponent.id = enemyCharacterComponents.Count; 
			enemyCharacterComponents.Add(enemyComponent);
		}

		private EnemyCharacterSC CreateEnemyCharacter(EnemyTypeSO enemyTypeSO) {
			var data = enemyTypeSO.ToData();
			data.Id = enemyCharacterComponents.Count + _enemyCharacterData?.Count ?? 0;
			return CreateComponent<EnemyCharacterSC, EnemyCharacterData>(data, enemyCharacterParent);
		}
		
		// private EnemyCharacterSC CreateEnemyCharacter(EnemyCharacterData data) {
		// 	EnemyCharacterSC enemy = EnemyCharacterSC.CreateAndLoad(data);
		// 	enemy.transform.SetParent(enemyCharacterParent != null ? enemyCharacterParent : transform);
		// 	//todo local or global count??
		// 	enemy.id = enemyCharacterComponents.Count;
		// 	return enemy;
		// }

		#endregion

		#region From CharList

		public int GetEnemyCountByType(EnemyTypeSO enemyType) {
			var count = 0;
			
			var deadEnemies = enemyCharacterComponents.Where(enemy => enemy.IsDead).ToList();
			count = deadEnemies.Count;

			if ( enemyType != null ) {
				foreach ( var enemy in deadEnemies ) {
					if ( enemy != null ) {
						var component = enemy.GetComponent<EnemyCharacterSC>();
						
						if ( component.Type == enemyType ) {
							count++;
						}  
					}
				}  
			}

			return count;
		}

		#endregion

		public List<EnemyCharacterSC> GetEnemyCahracters() {
			return enemyCharacterComponents;
		}

		public IEnumerable<EnemyCharacterSC> GetEnemyCahractersWhere(Func<EnemyCharacterSC, bool> predicate) {
			return enemyCharacterComponents.Where(predicate);
		}

		public EnemyCharacterSC GetEnemyCharacterAt(Vector3 worldPos) {
			return GetEnemyCharacterAt(_gridData.GetGridPos3DFromWorldPos(worldPos));
		}
		
		public EnemyCharacterSC GetEnemyCharacterAt(Vector3Int gridPos) {
			return enemyCharacterComponents.FirstOrDefault(enemy => enemy.GridPosition.Equals(gridPos));
		}
		
		public void ClearEnemyCharacters() {
			enemyCharacterComponents.ClearMonoBehaviourGameObjectReferences();
			_enemyCharacterData.Clear();
		}

		public void AddEnemyCharacterAt(EnemyTypeSO enemyType, Vector3 worldPosition) {
			if ( GetEnemyCharacterAt(worldPosition) == null ) {
				var enemy = CreateEnemyCharacter(enemyType);
				enemy.GridTransform.MoveTo(worldPosition);	
				enemyCharacterComponents.Add(enemy);
			}
		}
		
		public void RemoveEnemyCharacterAt(Vector3 worldPos) {
			var enemy = GetEnemyCharacterAt(worldPos);
			if ( enemy is { } ) {
				enemyCharacterComponents.Remove(enemy);
				Destroy(enemy.gameObject);
			}
		}
	}
}
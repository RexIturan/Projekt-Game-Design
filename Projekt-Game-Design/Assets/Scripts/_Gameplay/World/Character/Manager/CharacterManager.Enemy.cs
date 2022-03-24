using System.Collections.Generic;
using FullSerializer;
using GDP01._Gameplay.World.Character.Data;
using UnityEngine;

namespace GDP01._Gameplay.World.Character {
	public partial class CharacterManager {
		[Header("Enemy Character Data")]
		[SerializeField, fsIgnore] private Transform enemyCharacterParent;
		[SerializeField, fsIgnore] private EnemyTypeSO defaultEnemyData;
		[SerializeField, fsIgnore] private List<EnemyCharacterSC> enemyCharacterComponents;
		[SerializeField, fsIgnore] private List<EnemyCharacterData> _enemyCharacterData;
		
		///// Enemy ////////////////////////////////////////////////////////////////////////////////////////
		#region Enemy Character

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
		private void AddEnemyCharacter() {
			var data = defaultEnemyData.ToData();
			
			//todo refactor get next playerchar id
			data.Id = enemyCharacterComponents.Count + _enemyCharacterData?.Count ?? 0;
			enemyCharacterComponents.Add(CreateComponent<EnemyCharacterSC, EnemyCharacterData>(data, enemyCharacterParent));
		}
		
		// private EnemyCharacterSC CreateEnemyCharacter(EnemyCharacterData data) {
		// 	EnemyCharacterSC enemy = EnemyCharacterSC.CreateAndLoad(data);
		// 	enemy.transform.SetParent(enemyCharacterParent != null ? enemyCharacterParent : transform);
		// 	//todo local or global count??
		// 	enemy.id = enemyCharacterComponents.Count;
		// 	return enemy;
		// }

		#endregion
	}
}
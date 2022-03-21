using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GDP01.Structure {
	[CreateAssetMenu(fileName = "LevelDataContainerSO", menuName = "Level/Level Data Container", order = 0)]
	public class LevelDataContainerSO : ScriptableObject {
		[SerializeField] private List<LevelDataSO> levelDataList = new List<LevelDataSO>();
		public List<LevelDataSO> LevelDataList { get => levelDataList; set => levelDataList = value; }

		public LevelDataSO GetLevelDataByName(string levelName) {
			return LevelDataList.FirstOrDefault(levelData => levelData.name.Equals(levelName));
		}
	}
}
using System;
using Grid;
using UnityEngine;

namespace FieldOfView.ScriptableObjects {
	[CreateAssetMenu(fileName = "FieldOfViewCacheSO", menuName = "FOV/Cache SO", order = 0)]
	public class FieldOfViewCacheSO : ScriptableObject {
		[SerializeField] private GridDataSO gridDataSO;

		public Vector2Int mapSize;
		public Texture2D playerVisionMap;
		
		//todo texture2Darray
		// public Texture2DArray playerVisionMaps;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void UpdateMapSize() {
			mapSize = new Vector2Int(gridDataSO.Width, gridDataSO.Depth);
		}

		private void CreateNewVisionMapTexture() {
			// vision map size -> size * 2 +1
			
			
			
			// playerVisionMap = new Texture2D()
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////		
		
		private void OnEnable() {
			UpdateMapSize();
		}
	}
}
using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace GDP01._Gameplay.Environment.Level.Grid.Types {
	[Serializable]
	public class TileVariance {
		private const int UP = 0;
		private const int N = 1;
		private const int E = 2;
		private const int S = 3;
		private const int W = 4;
		private const int DOWN = 5;
		
		
		public GameObject basicTile;
		
		public GameObject top_sides_0;
		public GameObject top_sides_N;
		public GameObject top_sides_NE;
		public GameObject top_sides_NS;
		public GameObject top_sides_NES;
		public GameObject top_sides_NESW;
		
		public GameObject bottom_sides_0;
		public GameObject bottom_sides_N;
		public GameObject bottom_sides_NE;
		public GameObject bottom_sides_NS;
		public GameObject bottom_sides_NES;
		public GameObject bottom_sides_NESW;

		private Vector3 rotation = Vector3.zero;

		private int GetMask(List<TileTypeSO> neighbours, TileTypeSO current) {
			int mask = 0;
			
			var up = neighbours[UP];
			var north = neighbours[N];
			var east = neighbours[E];
			var south = neighbours[S];
			var west = neighbours[W];
			var down = neighbours[DOWN];

			if ( up == current ) {
				mask |= 1 << 0;
			}

			if ( north == current ) {
				mask |= 1 << 1;
			}
			
			if ( east == current ) {
				mask |= 1 << 2;
			}
			
			if ( south == current ) {
				mask |= 1 << 3;
			}
			
			if ( west == current ) {
				mask |= 1 << 4;
			}

			if ( down == current ) {
				mask |= 1 << 5;
			}

			return mask;
		}
		
		public GameObject GetPrefab(List<TileTypeSO> neighbours, TileTypeSO current) {
			GameObject prefab = basicTile;

			//todo bitmask -> tile variants
			// int mask = GetMask(neighbours, current);
			//
			// if ( ( mask & 1 << 0 ) == 1 ) {
			// 	
			// }
			// else {
			// 	
			// }
			
			return prefab;
		}

		public GameObject CreateTile(Vector3 worldPos, Transform tileParent, TileTypeSO currentType, List<TileTypeSO> neighbours) {
			var prefab = GetPrefab(neighbours, currentType);

			Quaternion rot = rotation.Equals(Vector3.zero)
				? Quaternion.identity
				: Quaternion.LookRotation(rotation);
			
			var obj = prefab != null ? GameObject.Instantiate(prefab, worldPos, rot, tileParent) : null;

			return obj;
		}
	}
}
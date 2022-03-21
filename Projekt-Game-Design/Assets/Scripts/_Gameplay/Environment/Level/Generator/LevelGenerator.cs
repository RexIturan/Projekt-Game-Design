using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace GDP01._Gameplay.Environment.Level.Generator {
	
	[ExecuteAlways]
	public class LevelGenerator : MonoBehaviour {
		[SerializeField] private Texture2D heightmap;
		[SerializeField] private List<Color> colors;
		[SerializeField] private float2 dim;
		[SerializeField] private float2 meshScale = 10f;
		
		
		[SerializeField] private Mesh finalMesh;
		[SerializeField] private MeshFilter finalMeshFilter;
		
		[SerializeField] private Transform visualization;
		[SerializeField] private MeshRenderer visualizationRenderer;
		
		//TODO Move to Mesh Generator
		[SerializeField] private Mesh blockMesh;

		// spawn gameobjects foreach block
		[SerializeField] private Transform parent;
		[SerializeField] private Transform blockPrefab;
		
///// Private Fields

		private CombineInstance[] _combine;
		private float3[,] _positions;

		private Transform[,] blocks; 
			
///// Unity Functions		
		[ContextMenu("Delete Blocks")]
		private void DeleteBlocks() {
			if ( blocks is {} ) {
				blocks.DeleteAll();
			}

			if ( parent.childCount > 0 ) {
				var len = parent.childCount;
				List<GameObject> objects = new List<GameObject>();
				for ( int i = 0; i < len; i++ ) {
					objects.Add(parent.GetChild(i).gameObject);
				}
				
				objects.ForEach(DestroyImmediate);
				objects.Clear();
				objects = null;
			}
		}

		[ContextMenu("Refresh Visuals")]
		private void RefreshVisuals() {
			colors = new List<Color>();
			finalMesh = new Mesh();

			Color[] heightmapColors = heightmap.GetPixels();
			visualizationRenderer.sharedMaterial.mainTexture = heightmap;
			if ( heightmap.isReadable ) {
				colors = heightmapColors.Select(x => x).Distinct().ToList();
				colors.Sort((color, color1) => color.grayscale > color1.grayscale ? 1 : color.grayscale < color1.grayscale ? -1 : 0);
			}

			dim = new float2(heightmap.width, heightmap.height);
			
			float2 localScale = dim / meshScale;
			visualization.localScale = new Vector3(localScale.x, 1, localScale.y);

			//generate meshes and combine them
			int2 size = ( int2 ) dim;
			_positions = new float3[size.x, size.y];
			_combine = new CombineInstance[size.x * size.y];
			
			if ( blocks is {} ) {
				blocks.DeleteAll();
			}
			blocks = new Transform[size.x, size.y];
			
			for ( int y = 0; y < size.y; y++ ) {
				for ( int x = 0; x < size.x; x++ ) {
					Color color = heightmapColors[size.Get1DIndex(x, y)];
					_positions[x, y] = new float3(x, colors.IndexOf(color), y);

					// float4x4 mat = float4x4.identity;
					// mat.c3 += new float4(_positions[x, y], 0);
					//
					// // blockMesh.indexFormat = IndexFormat.UInt32;
					//
					// CombineInstance combineInstance = new CombineInstance {
					// 	mesh = blockMesh,
					// 	transform = mat
					// }; 
					//
					// _combine[size.Get1DIndex(x, y)] = combineInstance;
					blocks[x, y] = Instantiate(blockPrefab, parent);
					blocks[x, y].localPosition = _positions[x, y];
				}	
			}

			// for ( int y = 0; y < size.y; y++ ) {
			// 	for ( int x = 0; x < size.x; x++ ) {
			// 		blocks[x, y].localPosition = _positions[x, y];
			// 	}
			// }
			
			// finalMesh.CombineMeshes(_combine);
			// if ( finalMeshFilter != null ) {
			// 	finalMeshFilter.mesh = finalMesh;
			// }
		}

		private void Update() {
			// if ( finalMeshFilter is { } ) {
			// 	finalMeshFilter.sharedMesh = finalMesh;	
			// }
		}
	}

	public static class ArrayExtensions {
		public static void DeleteAll(this Transform[,] array) {
			foreach ( var obj in array ) {
				if ( obj != null ) {
					GameObject.DestroyImmediate(obj.gameObject);
				}
			}
		}
		
		public static int Get1DIndex<T>(this T[,] array, int x, int y) {
			return x + y * array.Width();
		}
		
		public static int Width<T>(this T[,] array) => array.GetLength(0);
		public static int Height<T>(this T[,] array) => array.GetLength(1);
		
		//int2 part
		public static int Get1DIndex(this int2 size, int x, int y) {
			return x + y * size.Width();
		}
		
		public static int Width(this int2 size) => size.x;
		public static int Height(this int2 size) => size.y;
	}
}
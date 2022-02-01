using System.Collections.Generic;
using GDP01.Util.MeshUtils;
using Grid;
using Level.Grid;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace MeshGenerator {
	public class MeshGenerator : MonoBehaviour {
		private enum DataTile {
			Air = 0,
			Block = 1,
		}

		private enum Direction {
			Top, // y+
			Bottom, // y-
			Right, // x+
			Left, // x-
			Forward, // z+
			Back // z-
		}
		
		private const int Height = 0;
		private const int Depth = 1;
		private const int Width = 2;

		[Header("References")] [SerializeField]
		private GridDataSO globalGridData;

		[SerializeField] private GridContainerSO gridContainer;
		[SerializeField] private TileTypeContainerSO tileTypeContainer;

///// Private Variables ////////////////////////////////////////////////////////////////////////////		
		
		private MeshCollider _meshCollider;
		private MeshFilter _meshFilter;
		// private MeshRenderer _meshRenderer;

		[SerializeField] private Mesh _mesh;
		private List<Vector3> _vertices;

		private List<int> _triangles;

		// private List<Vector3> normals;
		private List<Vector2> _uvs;

		//y, z, x
		private DataTile[,,] _tileData;

///// Properties ///////////////////////////////////////////////////////////////////////////////////

		private Mesh Mesh => _mesh ??= CreateNewMesh();
		private List<Vector3> Vertices => _vertices ??= new List<Vector3>();
		private List<int> Triangles => _triangles ??= new List<int>();
		private List<Vector2> Uvs => _uvs ??= new List<Vector2>();

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private Mesh CreateNewMesh() {
			return MeshCreator.CreateMesh(globalGridData.LevelName);
		}

		private void GenerateMesh(Vector3Int lowerBounds, Vector3Int dimensions) {
			Vertices.Clear();
			Triangles.Clear();
			Uvs.Clear();

			var posOffset = new Vector3(.5f, .5f, .5f);
			var cellDim = Vector3.one;
			var uvs = new Vector2(0, 0);

			for ( int y = lowerBounds.y; y < dimensions.y; y++ ) {
				for ( int z = lowerBounds.z; z < dimensions.z; z++ ) {
					for ( int x = lowerBounds.x; x < dimensions.x; x++ ) {
						if ( _tileData[y, z, x] == DataTile.Block ) {
							var intPos = new Vector3Int(x, y, z);
							var pos = new Vector3(x, y, z) + globalGridData.OriginPosition;

							if ( !TileInDirection(intPos, Direction.Top, lowerBounds, dimensions) ) {
								// draw top
								AddQuadAt(pos, posOffset, cellDim, Direction.Top, uvs);
							}

							if ( !TileInDirection(intPos, Direction.Bottom, lowerBounds, dimensions) ) {
								// draw bottom
								AddQuadAt(pos, posOffset, cellDim, Direction.Bottom, uvs);
							}

							if ( !TileInDirection(intPos, Direction.Right, lowerBounds, dimensions) ) {
								// draw right
								AddQuadAt(pos, posOffset, cellDim, Direction.Right, uvs);
							}

							if ( !TileInDirection(intPos, Direction.Left, lowerBounds, dimensions) ) {
								// draw left
								AddQuadAt(pos, posOffset, cellDim, Direction.Left, uvs);
							}

							if ( !TileInDirection(intPos, Direction.Forward, lowerBounds, dimensions) ) {
								// draw forward
								AddQuadAt(pos, posOffset, cellDim, Direction.Forward, uvs);
							}

							if ( !TileInDirection(intPos, Direction.Back, lowerBounds, dimensions) ) {
								// draw back
								AddQuadAt(pos, posOffset, cellDim, Direction.Back, uvs);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="dir"></param>
		/// <param name="lowerBounds">inclusive</param>
		/// <param name="dimensions">inclusiv</param> 
		/// <returns></returns>
		private bool TileInDirection(Vector3Int pos, Direction dir, Vector3Int lowerBounds,
			Vector3Int dimensions) {
			bool value = true;

			switch ( dir ) {
				case Direction.Top:
					// bounds check
					if ( pos.y == dimensions.y - 1 ) {
						value = false;
					}
					else {
						if ( _tileData[pos.y + 1, pos.z, pos.x] == DataTile.Air ) {
							value = false;
						}
					}

					break;
				case Direction.Bottom:
					// bounds check
					if ( pos.y == lowerBounds.y ) {
						value = false;
					}
					else {
						if ( _tileData[pos.y - 1, pos.z, pos.x] == DataTile.Air ) {
							value = false;
						}
					}

					break;
				case Direction.Right:
					if ( pos.x == dimensions.x - 1 ) {
						value = false;
					}
					else {
						if ( _tileData[pos.y, pos.z, pos.x + 1] == DataTile.Air ) {
							value = false;
						}
					}

					break;
				case Direction.Left:
					if ( pos.x == lowerBounds.x ) {
						value = false;
					}
					else {
						if ( _tileData[pos.y, pos.z, pos.x - 1] == DataTile.Air ) {
							value = false;
						}
					}

					break;
				case Direction.Forward:
					if ( pos.z == dimensions.z - 1 ) {
						value = false;
					}
					else {
						if ( _tileData[pos.y, pos.z + 1, pos.x] == DataTile.Air ) {
							value = false;
						}
					}

					break;
				case Direction.Back:
					if ( pos.z == lowerBounds.z ) {
						value = false;
					}
					else {
						if ( _tileData[pos.y, pos.z - 1, pos.x] == DataTile.Air ) {
							value = false;
						}
					}

					break;
			}

			return value;
		}

		private void AddQuadAt(Vector3 origin, Vector3 posOffset, Vector3 cellDimensions, Direction dir,
			Vector2 uvPos) {
			var cellOffset = cellDimensions * 0.5f;
			var widthOffset = new Vector3(cellOffset.x, 0, 0);
			var depthOffset = new Vector3(0, 0, cellOffset.z);
			var heightOffset = new Vector3(0, cellOffset.y, 0);

			var a = heightOffset - widthOffset - depthOffset;
			var b = heightOffset - widthOffset + depthOffset;
			var c = heightOffset + widthOffset - depthOffset;
			var d = heightOffset + widthOffset + depthOffset;

			Quaternion q = Quaternion.identity;

			switch ( dir ) {
				case Direction.Top:
					q = Quaternion.Euler(0, 0, 0);
					break;
				case Direction.Bottom:
					q = Quaternion.Euler(180, 0, 0);
					break;
				case Direction.Right:
					q = Quaternion.Euler(0, 0, -90);
					break;
				case Direction.Left:
					q = Quaternion.Euler(0, 0, +90);
					break;
				case Direction.Forward:
					q = Quaternion.Euler(90, 0, 0);
					break;
				case Direction.Back:
					q = Quaternion.Euler(-90, 0, 0);
					break;
			}

			//var q = Quaternion.identity.eulerAngles;// Quaternion.RotateTowards(Quaternion.LookRotation(Vector3.up), Quaternion.LookRotation(normal), 180).eulerAngles;//Quaternion.RotateTowards(Quaternion.LookRotation(Vector3.up, Vector3.left), Quaternion.LookRotation(normal), 180);
			a = q * a;
			b = q * b;
			c = q * c;
			d = q * d;
			// Debug.Log($"{a}, {q}"); 

			var v = Vertices.Count;
			Vertices.AddRange(new[] {
				a + origin + posOffset,
				b + origin + posOffset,
				c + origin + posOffset,
				d + origin + posOffset,
			});

			Uvs.AddRange(new[] {
				uvPos,
				uvPos,
				uvPos,
				uvPos
			});


			Triangles.AddRange(new[] {
				v, v + 1, v + 2,
				v + 1, v + 3, v + 2,
			});
		}

///// Public Functions /////////////////////////////////////////////////////////////////////////////	
		
		// real world coords
		/// <summary>
		/// _tileData == [y,z,x]
		/// </summary>
		public void UpdateTileData() {
			// setup 3d tile data representation
			int width = globalGridData.Width;
			int height = globalGridData.Height;
			int depth = globalGridData.Depth;
			//todo maybe use [x,y,z] and not [y,z,x]
			_tileData = new DataTile[height, depth, width];

			// get data from grid container
			for ( int y = 0; y < height; y++ ) {
				var grid = gridContainer.tileGrids[y];

				for ( int z = 0; z < depth; z++ ) {
					for ( int x = 0; x < width; x++ ) {
						Tile tile = grid.GetGridObject(x, z);
						var type = tileTypeContainer.tileTypes[tile.tileTypeID];

						// check if block
						if ( type.properties.HasFlag(TileProperties.Solid) ) {
							_tileData[y, z, x] = DataTile.Block;
						}
						else {
							_tileData[y, z, x] = DataTile.Air;
						}
					}
				}
			}
		}		
		
		// todo max mesh vertices 65,535
		public void GenerateMesh() {
			int width = _tileData.GetLength(Width);
			int height = _tileData.GetLength(Height);
			int depth = _tileData.GetLength(Depth);

			var lowerBounds = Vector3Int.zero;
			var dimensions = new Vector3Int(width, height, depth);

			GenerateMesh(lowerBounds, dimensions);
		}
		
		public void UpdateMesh() {
			_meshFilter = GetComponent<MeshFilter>();
			_meshCollider = GetComponent<MeshCollider>();
			// _meshRenderer = GetComponent<MeshRenderer>();
			
			_mesh = CreateNewMesh();
			Mesh.Clear();

			Mesh.SetVertices(Vertices);
			Mesh.SetTriangles(Triangles, 0);
			Mesh.SetUVs(0, Uvs);
			// mesh.SetNormals(normals);
			Mesh.RecalculateNormals();

			_meshFilter.sharedMesh = Mesh;
			_meshCollider.sharedMesh = _meshFilter.sharedMesh;
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void Awake() {
			_meshFilter = GetComponent<MeshFilter>();
			_meshCollider = GetComponent<MeshCollider>();
		}
	}
}
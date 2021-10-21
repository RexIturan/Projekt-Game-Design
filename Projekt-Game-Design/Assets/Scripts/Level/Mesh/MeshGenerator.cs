using System.Collections.Generic;
using Grid;
using Level.Grid;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace MeshGenerator {
    public class MeshGenerator : MonoBehaviour {
        private static readonly int Height = 0;
        private static readonly int Depth = 1;
        private static readonly int Width = 2;
        
        [Header("References")] 
        [SerializeField] private GridDataSO globalGridData;
        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private TileTypeContainerSO tileTypeContainer;

        private MeshFilter _meshFilter;
        // private MeshRenderer _meshRenderer;
        
        private Mesh _mesh;
        private List<Vector3> _vertices;
        private List<int> _triangles;
        // private List<Vector3> normals;
        private List<Vector2> _uvs;

        private DataTile[,,] _tileData;

        private void Awake() {
            _vertices = new List<Vector3>();
            _triangles = new List<int>();
            _uvs = new List<Vector2>();
            
            _mesh = new Mesh();
            _mesh.name = "generatedMesh";

            _meshFilter = GetComponent<MeshFilter>();
            // _meshRenderer = GetComponent<MeshRenderer>();
        }

        // real world coords
        public void UpdateTileData() {
           
            // setup 3d tile data representation
            int width = globalGridData.Width;
            int height = gridContainer.tileGrids.Count; 
            int depth = globalGridData.Height;
            _tileData = new DataTile[height, depth, width];
            
            // get data from grid container
            for (int y = 0; y < height; y++) {
                var grid = gridContainer.tileGrids[y];
                
                for (int z = 0; z < depth; z++) {
                    for (int x = 0; x < width; x++) {
                        Tile tile = grid.GetGridObject(x, z);
                        var type = tileTypeContainer.tileTypes[tile.tileTypeID];
                        
                        // check if block
                        if (type.properties.HasFlag(TileProperties.Solid)) {
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
            var upperBounds = new Vector3Int(width, height, depth);
            
            GenerateMesh(lowerBounds, upperBounds);
        }

        public void GenerateMesh(Vector3Int lowerBounds, Vector3Int upperBounds) {
            
            _vertices.Clear();
            _triangles.Clear();
            this._uvs.Clear();
            
            var posOffset = new Vector3(.5f, .5f, .5f);
            var cellDim = Vector3.one;
            var uvs = new Vector2(0, 0);
            
            for (int y = lowerBounds.y; y < upperBounds.y; y++) {
                for (int z = lowerBounds.z; z < upperBounds.z; z++) {
                    for (int x = lowerBounds.x; x < upperBounds.x; x++) {
                        if (_tileData[y, z, x] == DataTile.Block) {
                            var intPos = new Vector3Int(x, y, z);
                            var pos = new Vector3(x, y, z) + globalGridData.originPosition;

                            if (!TileInDirection(intPos, Direction.Top, lowerBounds, upperBounds)) {
                                // draw top
                                AddQuadAt(pos, posOffset, cellDim, Direction.Top, uvs);    
                            }
                            
                            if (!TileInDirection(intPos, Direction.Bottom, lowerBounds, upperBounds)) {
                                // draw bottom
                                AddQuadAt(pos, posOffset, cellDim, Direction.Bottom, uvs);    
                            }

                            if (!TileInDirection(intPos, Direction.Right, lowerBounds, upperBounds)) {
                                // draw right
                                AddQuadAt(pos, posOffset, cellDim, Direction.Right, uvs);
                            }
                            
                            if (!TileInDirection(intPos, Direction.Left, lowerBounds, upperBounds)) {
                                // draw left
                                AddQuadAt(pos, posOffset, cellDim, Direction.Left, uvs);
                            }

                            if (!TileInDirection(intPos, Direction.Forward, lowerBounds, upperBounds)) {
                                // draw forward
                                AddQuadAt(pos, posOffset, cellDim, Direction.Forward, uvs);
                            }
                            
                            if (!TileInDirection(intPos, Direction.Back, lowerBounds, upperBounds)) {
                                // draw back
                                AddQuadAt(pos, posOffset, cellDim, Direction.Back, uvs);
                            }
                        }
                    }
                }
            }
        }

        private bool TileInDirection(Vector3Int pos, Direction dir, Vector3Int lowerBounds, Vector3Int upperBounds) {
            bool value = true;
            
            switch (dir) {
                case Direction.Top:
                    // bounds check
                    if (pos.y == upperBounds.y) {
                        value = false;
                    }
                    else {
                        if (_tileData[pos.y + 1, pos.z, pos.x] == DataTile.Air) {
                            value = false;
                        } 
                    }
                    break;
                case Direction.Bottom:
                    // bounds check
                    if (pos.y == lowerBounds.y) {
                        value = false;
                    }
                    else {
                        if (_tileData[pos.y - 1, pos.z, pos.x] == DataTile.Air) {
                            value = false;
                        } 
                    }
                    break;
                case Direction.Right:
                    if (pos.x == upperBounds.x-1) {
                        value = false;
                    }
                    else {
                        if (_tileData[pos.y, pos.z, pos.x + 1] == DataTile.Air) {
                            value = false;
                        } 
                    }
                    break;
                case Direction.Left:
                    if (pos.x == lowerBounds.x) {
                        value = false;
                    }
                    else {
                        if (_tileData[pos.y, pos.z, pos.x - 1] == DataTile.Air) {
                            value = false;
                        } 
                    }
                    break;
                case Direction.Forward:
                    if (pos.z == upperBounds.z-1) {
                        value = false;
                    }
                    else {
                        if (_tileData[pos.y, pos.z + 1, pos.x] == DataTile.Air) {
                            value = false;
                        } 
                    }
                    break;
                case Direction.Back:
                    if (pos.z == lowerBounds.z) {
                        value = false;
                    }
                    else {
                        if (_tileData[pos.y, pos.z - 1, pos.x] == DataTile.Air) {
                            value = false;
                        } 
                    }
                    break;
            }

            return value;
        }

        private void AddQuadAt(Vector3 origin, Vector3 posOffset, Vector3 cellDimensions, Direction dir, Vector2 uvPos) {
            var cellOffset = cellDimensions * 0.5f;
            var widthOffset = new Vector3(cellOffset.x, 0, 0);
            var depthOffset = new Vector3(0, 0, cellOffset.z);
            var heightOffset = new Vector3(0, cellOffset.y, 0);
             
            var a = heightOffset - widthOffset - depthOffset;
            var b = heightOffset - widthOffset + depthOffset;
            var c = heightOffset + widthOffset - depthOffset;
            var d = heightOffset + widthOffset + depthOffset;

            Quaternion q = Quaternion.identity;
            
            switch (dir) {
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
            
            var v = _vertices.Count;
            _vertices.AddRange(new [] {
                a + origin + posOffset,
                b + origin + posOffset,
                c + origin + posOffset,
                d + origin + posOffset,
            });
            
            _uvs.AddRange(new [] {
                uvPos,
                uvPos,
                uvPos,
                uvPos
            });
            
            
            _triangles.AddRange(new [] {
                v, v+1, v+2,
                v+1, v+3, v+2,
            });
        }
        
        // private void AddQuadAt(Vector3 pos, Vector3 normal, Vector2 uvPos) {
        //     // vertices.Append(start)
        //
        //     var v = _vertices.Count;
        //     
        //     _vertices.AddRange(new [] {
        //         new Vector3(pos.x - 0.5f, pos.y, pos.z - 0.5f),
        //         new Vector3(pos.x - 0.5f, pos.y, pos.z + 0.5f),
        //         new Vector3(pos.x + 0.5f, pos.y, pos.z - 0.5f),
        //         new Vector3(pos.x + 0.5f, pos.y, pos.z + 0.5f),
        //     });
        //     
        //     _uvs.AddRange(new [] {
        //         uvPos,
        //         uvPos,
        //         uvPos,
        //         uvPos
        //     });
        //     
        //     _triangles.AddRange(new [] {
        //           v, v+1, v+2,
        //         v+1, v+3, v+2,
        //     });
        // }
        
        public void UpdateMesh() {
            _meshFilter = GetComponent<MeshFilter>();
            // _meshRenderer = GetComponent<MeshRenderer>();
            
            _mesh.Clear();
            
            _mesh.SetVertices(_vertices);
            _mesh.SetTriangles(_triangles, 0);
            _mesh.SetUVs(0, _uvs);
            // mesh.SetNormals(normals);
            _mesh.RecalculateNormals();
            
            _meshFilter.sharedMesh = _mesh;
        }
        
        
        // TODO move to texture util stuff
//         private void SaveTexture(Texture2D texture)
//         {
//             byte[] bytes = texture.EncodeToPNG();
//             var dirPath = Application.dataPath + "/RenderOutput";
//             if (!System.IO.Directory.Exists(dirPath))
//             {
//                 System.IO.Directory.CreateDirectory(dirPath);
//             }
//             System.IO.File.WriteAllBytes(dirPath + "/R_" + Random.Range(0, 100000) + ".png", bytes);
//             Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
// #if UNITY_EDITOR
//             UnityEditor.AssetDatabase.Refresh();
// #endif
//         }
    }

    internal enum DataTile {
        Air = 0,
        Block = 1,
    }
    
    internal enum Direction {
        Top,     // y+
        Bottom,  // y-
        Right,   // x+
        Left,    // x-
        Forward, // z+
        Back     // z-
    }
}

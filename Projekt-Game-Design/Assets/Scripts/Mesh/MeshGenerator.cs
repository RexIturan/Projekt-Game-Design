using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Grid;
using JetBrains.Annotations;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace MeshGenerator {
    public class MeshGenerator : MonoBehaviour {
        private static readonly int HEIGHT = 0;
        private static readonly int DEPTH = 1;
        private static readonly int WIDTH = 2;
        
        [Header("References")] 
        [SerializeField] private GridDataSO globalGridData;
        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private TileTypeContainerSO tileTypeContainer;

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        
        private Mesh mesh;
        private List<Vector3> vertices;
        private List<int> triagles;
        // private List<Vector3> normals;
        private List<Vector2> uvs;

        private Texture2D texture;

        private ETile[,,] tileData;
        
        private void Awake() {
            
            
            vertices = new List<Vector3>();
            triagles = new List<int>();
            uvs = new List<Vector2>();
            
            mesh = new Mesh();
            mesh.name = "generatedMesh";

            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
                
            // AddQuadAt(new Vector3(0, 0, 0), Vector3.up,  new Vector2(0,1));
            // AddQuadAt(new Vector3(0, 0, 1), Vector3.up, new Vector2(0.5f,0));
            // AddQuadAt(new Vector3(0, 0, 2), Vector3.up, new Vector2(1,0));
            // AddQuadAt(new Vector3(2, 0, 0), Vector3.up, new Vector2(0,0));

            // var posOffset = new Vector3(.5f, .5f, .5f);
            
            // AddQuadAt(new Vector3(0, 0, 0), posOffset, Vector3.one, EDirection.top, new Vector2(0,0));
            // AddQuadAt(new Vector3(0, 0, 1), posOffset, Vector3.one, EDirection.top, new Vector2(0,0));
            // AddQuadAt(new Vector3(1, 0, 0), posOffset, Vector3.one, EDirection.top, new Vector2(0,0));
            // AddQuadAt(new Vector3(1, 0, 1), posOffset, Vector3.one, EDirection.top, new Vector2(0,0));
            // AddQuadAt(new Vector3(0, 0, 0), posOffset, Vector3.one, EDirection.bottom, new Vector2(1,0));
            // AddQuadAt(new Vector3(0, 0, 0), posOffset, Vector3.one, EDirection.right, new Vector2(0,0));
            // AddQuadAt(new Vector3(0, 0, 0), posOffset, Vector3.one, EDirection.left, new Vector2(1,0));
            // AddQuadAt(new Vector3(0, 0, 0), posOffset, Vector3.one, EDirection.forward, new Vector2(0,0));
            // AddQuadAt(new Vector3(0, 0, 0), posOffset, Vector3.one, EDirection.back, new Vector2(1,0));
            
            //todo mesh controller 
            // UpdateTileData();
            // GenerateMesh();
            // UpdateMesh();
        }

        // real world coords
        public void UpdateTileData() {
           
            // setup 3d tile data representation
            int width = globalGridData.Width;
            int height = gridContainer.tileGrids.Count; 
            int depth = globalGridData.Height;
            tileData = new ETile[height, depth, width];
            
            // get data from gridcontainer
            for (int y = 0; y < height; y++) {
                var grid = gridContainer.tileGrids[y];
                
                for (int z = 0; z < depth; z++) {
                    for (int x = 0; x < width; x++) {
                        Tile tile = grid.GetGridObject(x, z);
                        var type = tileTypeContainer.tileTypes[tile.tileTypeID];
                        
                        // check if block
                        if (type.flags.HasFlag(ETileFlags.solid)) {
                            tileData[y, z, x] = ETile.block;
                        }
                        else {
                            tileData[y, z, x] = ETile.air;
                        }
                    }
                }
            }
        }

        // todo max mesh vertices 65,535
        public void GenerateMesh() {
            int width = tileData.GetLength(WIDTH);
            int height = tileData.GetLength(HEIGHT); 
            int depth = tileData.GetLength(DEPTH);
            
            var lowerBounds = Vector3Int.zero;
            var upperBounds = new Vector3Int(width, height, depth);
            
            GenerateMesh(lowerBounds, upperBounds);
        }

        public void GenerateMesh(Vector3Int lowerBounds, Vector3Int upperBounds) {
            
            vertices.Clear();
            triagles.Clear();
            this.uvs.Clear();
            
            var posOffset = new Vector3(.5f, .5f, .5f);
            var cellDim = Vector3.one;
            var uvs = new Vector2(0, 0);
            
            for (int y = lowerBounds.y; y < upperBounds.y; y++) {
                for (int z = lowerBounds.z; z < upperBounds.z; z++) {
                    for (int x = lowerBounds.x; x < upperBounds.x; x++) {
                        if (tileData[y, z, x] == ETile.block) {
                            var intPos = new Vector3Int(x, y, z);
                            var pos = new Vector3(x, y, z) + globalGridData.originPosition;

                            if (!TileInDirection(intPos, EDirection.top, lowerBounds, upperBounds)) {
                                // draw top
                                AddQuadAt(pos, posOffset, cellDim, EDirection.top, uvs);    
                            }
                            
                            if (!TileInDirection(intPos, EDirection.bottom, lowerBounds, upperBounds)) {
                                // draw bottom
                                AddQuadAt(pos, posOffset, cellDim, EDirection.bottom, uvs);    
                            }

                            if (!TileInDirection(intPos, EDirection.right, lowerBounds, upperBounds)) {
                                // draw right
                                AddQuadAt(pos, posOffset, cellDim, EDirection.right, uvs);
                            }
                            
                            if (!TileInDirection(intPos, EDirection.left, lowerBounds, upperBounds)) {
                                // draw left
                                AddQuadAt(pos, posOffset, cellDim, EDirection.left, uvs);
                            }

                            if (!TileInDirection(intPos, EDirection.forward, lowerBounds, upperBounds)) {
                                // draw forward
                                AddQuadAt(pos, posOffset, cellDim, EDirection.forward, uvs);
                            }
                            
                            if (!TileInDirection(intPos, EDirection.back, lowerBounds, upperBounds)) {
                                // draw back
                                AddQuadAt(pos, posOffset, cellDim, EDirection.back, uvs);
                            }
                        }
                    }
                }
            }
        }

        private bool TileInDirection(Vector3Int pos, EDirection dir, Vector3 lowerBounds, Vector3 upperBounds) {
            bool value = true;
            
            switch (dir) {
                case EDirection.top:
                    // bounds check
                    if (pos.y == upperBounds.y-1) {
                        value = false;
                    }
                    else {
                        if (tileData[pos.y + 1, pos.z, pos.x] == ETile.air) {
                            value = false;
                        } 
                    }
                    break;
                case EDirection.bottom:
                    // bounds check
                    if (pos.y == lowerBounds.y) {
                        value = false;
                    }
                    else {
                        if (tileData[pos.y - 1, pos.z, pos.x] == ETile.air) {
                            value = false;
                        } 
                    }
                    break;
                case EDirection.right:
                    if (pos.x == upperBounds.x-1) {
                        value = false;
                    }
                    else {
                        if (tileData[pos.y, pos.z, pos.x + 1] == ETile.air) {
                            value = false;
                        } 
                    }
                    break;
                case EDirection.left:
                    if (pos.x == lowerBounds.x) {
                        value = false;
                    }
                    else {
                        if (tileData[pos.y, pos.z, pos.x - 1] == ETile.air) {
                            value = false;
                        } 
                    }
                    break;
                case EDirection.forward:
                    if (pos.z == upperBounds.z-1) {
                        value = false;
                    }
                    else {
                        if (tileData[pos.y, pos.z + 1, pos.x] == ETile.air) {
                            value = false;
                        } 
                    }
                    break;
                case EDirection.back:
                    if (pos.z == lowerBounds.z) {
                        value = false;
                    }
                    else {
                        if (tileData[pos.y, pos.z - 1, pos.x] == ETile.air) {
                            value = false;
                        } 
                    }
                    break;
                default:
                    break;
            }

            return value;
        }

        private void AddQuadAt(Vector3 origin, Vector3 posOffset, Vector3 cellDimensions, EDirection dir, Vector2 uvPos) {
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
                case EDirection.top:
                    q = Quaternion.Euler(0, 0, 0);
                    break;
                case EDirection.bottom:
                    q = Quaternion.Euler(180, 0, 0);
                    break;
                case EDirection.right:
                    q = Quaternion.Euler(0, 0, -90);
                    break;
                case EDirection.left:
                    q = Quaternion.Euler(0, 0, +90);
                    break;
                case EDirection.forward:
                    q = Quaternion.Euler(90, 0, 0);
                    break;
                case EDirection.back:
                    q = Quaternion.Euler(-90, 0, 0);
                    break;
                default:
                    break;
            }
            
            //var q = Quaternion.identity.eulerAngles;// Quaternion.RotateTowards(Quaternion.LookRotation(Vector3.up), Quaternion.LookRotation(normal), 180).eulerAngles;//Quaternion.RotateTowards(Quaternion.LookRotation(Vector3.up, Vector3.left), Quaternion.LookRotation(normal), 180);
            a = q * a;
            b = q * b;
            c = q * c;
            d = q * d;
            // Debug.Log($"{a}, {q}"); 
            
            var v = vertices.Count;
            vertices.AddRange(new [] {
                a + origin + posOffset,
                b + origin + posOffset,
                c + origin + posOffset,
                d + origin + posOffset,
            });
            
            uvs.AddRange(new [] {
                uvPos,
                uvPos,
                uvPos,
                uvPos
            });
            
            
            triagles.AddRange(new [] {
                v, v+1, v+2,
                v+1, v+3, v+2,
            });
        }
        
        private void AddQuadAt(Vector3 pos, Vector3 normal, Vector2 uvPos) {
            // vertices.Append(start)

            var v = vertices.Count;
            
            vertices.AddRange(new [] {
                new Vector3(pos.x - 0.5f, pos.y, pos.z - 0.5f),
                new Vector3(pos.x - 0.5f, pos.y, pos.z + 0.5f),
                new Vector3(pos.x + 0.5f, pos.y, pos.z - 0.5f),
                new Vector3(pos.x + 0.5f, pos.y, pos.z + 0.5f),
            });
            
            uvs.AddRange(new [] {
                uvPos,
                uvPos,
                uvPos,
                uvPos
            });
            
            triagles.AddRange(new [] {
                  v, v+1, v+2,
                v+1, v+3, v+2,
            });
        }
        
        public void UpdateMesh() {
            mesh.Clear();
            
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triagles, 0);
            mesh.SetUVs(0, uvs);
            // mesh.SetNormals(normals);
            mesh.RecalculateNormals();
            // mesh.RecalculateUVDistributionMetrics();
            
            meshFilter.sharedMesh = mesh;
            // meshRenderer.sharedMaterial.mainTexture = texture;
        }
        
        
        // TODO move to texture util stuff
        private void SaveTexture(Texture2D texture)
        {
            byte[] bytes = texture.EncodeToPNG();
            var dirPath = Application.dataPath + "/RenderOutput";
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            System.IO.File.WriteAllBytes(dirPath + "/R_" + Random.Range(0, 100000) + ".png", bytes);
            Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }

    internal enum ETile {
        air = 0,
        block = 1,
    }
    
    internal enum EDirection {
        top,     // y+
        bottom,  // y-
        right,   // x+
        left,    // x-
        forward, // z+
        back     // z-
    }
}
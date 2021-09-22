using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MeshGenerator {
    public class MeshGenerator : MonoBehaviour {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        
        private Mesh mesh;
        private List<Vector3> vertices;
        private List<int> triagles;
        // private List<Vector3> normals;
        private List<Vector2> uvs;

        private Texture2D texture;
        
        private void Start() {
            vertices = new List<Vector3>();
            triagles = new List<int>();
            // normals = new List<Vector3>();
            uvs = new List<Vector2>();
            
            mesh = new Mesh();

            texture = new Texture2D(3, 1);
            
            texture.SetPixel(0,0, Color.yellow);
            texture.SetPixel(1,0, Color.blue);
            texture.SetPixel(2,0, Color.red);
            texture.Apply();
            texture.filterMode = FilterMode.Point;
            // SaveTexture(texture);
            
            mesh.name = "generatedMesh";
            mesh = new Mesh();

            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
                
            AddQuadAt(new Vector3(0, 0, 0), new Vector2(0,.5f));
            AddQuadAt(new Vector3(0, 0, 1), new Vector2(.5f,.5f));
            AddQuadAt(new Vector3(0, 0, 2), new Vector2(1f,.5f));
            UpdateMesh();
        }

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
        
        private void AddQuadAt(Vector3 pos, Vector2 uvPos) {
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
        
        private void UpdateMesh() {
            mesh.Clear();
            
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triagles, 0);
            mesh.SetUVs(0, uvs);
            // mesh.SetNormals(normals);
            mesh.RecalculateNormals();
            mesh.RecalculateUVDistributionMetrics();
            
            meshFilter.sharedMesh = mesh;
            // meshRenderer.sharedMaterial.mainTexture = texture;
        }
    }
}
using System;
using Grid;
using UnityEngine;
using WorldObjects.ScriptableObjects;

namespace Visual {
    public class PrefabGridDrawer : MonoBehaviour, IMapDrawer {

        [SerializeField] private Transform parent;

        [SerializeField] private GridDataSO globalGridData;
        [SerializeField] private WorldObjectGridContainerSO worldObjectGridContainer;
        [SerializeField] private WorldObjectContainerSO worldObjectContainer;

        private GameObject[,] prefabObjects;

        //todo fix pls
        private void Start() {
            ClearPrefabParentChildren();
            prefabObjects = new GameObject[100, 100];
        }

        public void DrawGrid() {
            
            // for each instansiate prefab
            var offset = new Vector2Int((int)globalGridData.OriginPosition.x, (int)globalGridData.OriginPosition.z); 
            
            for (int l = 0; l < worldObjectGridContainer.worldObjectGrids.Count; l++) {
                var worldObjectGrid = worldObjectGridContainer.worldObjectGrids[l];
                for (int x = 0; x < worldObjectGrid.Width; x++) {
                    for (int y = 0; y < worldObjectGrid.Height; y++) {
                        var tile = worldObjectGrid.GetGridObject(x, y).type;
                        
                        if (tile != null) {

                            if (prefabObjects[x, y] == null) {
                                prefabObjects[x,y] = GameObject.Instantiate(
                                    worldObjectGrid.GetGridObject(x, y).type.prefab, 
                                    new Vector3(x + offset.x, l, y + offset.y), 
                                    Quaternion.identity);
                            
                                prefabObjects[x,y].transform.SetParent(parent);    
                            }
                            
                            
                                // worldObjectGrid.GetGridObject(x, y).type.prefab, new Vector3Int(x + offset.x, y + offset.y, l),);
                            // parent
                            // gridTilemap.SetTile(
                            //     new Vector3Int(x + offset.x, y + offset.y, l),
                            //     GetTileFromTileType(worldObjectGrid.GetGridObject(x, y).Type));    
                        }
                        else {
                            Debug.Log("error tile");
                            // gridTilemap.SetTile(
                            //     new Vector3Int(x + offset.x, y + offset.y, l),
                            //     errorTile);    
                        }
                    }
                }
            }
        }

        private void ClearPrefabParentChildren() {
            for (int i = 0; i < parent.childCount; i++) {
                var obj = parent.GetChild(i).gameObject;
                GameObject.DestroyImmediate(obj);
            }
        }

        public void clearCursor() {
            throw new System.NotImplementedException();
        }

        public void DrawBoxCursorAt(Vector3 start, Vector3 end) {
            throw new System.NotImplementedException();
        }

        public void DrawCursorAt(Vector3 pos) {
            throw new System.NotImplementedException();
        }
    }
}
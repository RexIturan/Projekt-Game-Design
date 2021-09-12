using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid {
    public class GridViewer : MonoBehaviour {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase grayTile;
        [SerializeField] private TileBase blueTile;
        [SerializeField] private TileBase redTile;
        [SerializeField] private TileBase orangeTile;


        [SerializeField] private GridContainerSO gridContainer;

        [SerializeField] private TileTypeContainerSO tileTypesContainer;

        public void DrawGrid() {
            tilemap.ClearAllTiles();
            
            for (int l = 0; l < gridContainer.tileGrids.Count; l++) {
                var tileGrid = gridContainer.tileGrids[l];
                for (int x = 0; x < tileGrid.Width; x++) {
                    for (int y = 0; y < tileGrid.Height; y++) {
                        if (tileGrid.GetGridObject(x, y).Type == tileTypesContainer.tileTypes[0]) {
                            tilemap.SetTile(new Vector3Int(x, y, l), grayTile);
                        }
                        if (tileGrid.GetGridObject(x, y).Type == tileTypesContainer.tileTypes[1]) {
                            tilemap.SetTile(new Vector3Int(x, y, l), blueTile);
                        }
                    }
                }
            }
        }
    }
}
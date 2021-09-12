using System;
using Grid;
using UnityEngine;

namespace LevelEditor {
    public class LevelEditor : MonoBehaviour {
        
        [SerializeField] private Cursor cursor;
        [Header("Settings")]
        // [SerializeField] private EOperation operation;
        // selected view
        // 
        // selection
        // [SerializeField] private ;

        [SerializeField] private GridController controller;

        [SerializeField] private TileTypeSO selectedTileType;

        [SerializeField] private TileTypeContainerSO tileTypesContainer;

        public void Awake() {
            selectedTileType = tileTypesContainer.tileTypes[1];
        }

        public void AddMultipleTilesAt(Vector3Int clickPos, Vector3Int dragPos) {
            controller.AddMultipleTilesAt(clickPos, dragPos, selectedTileType);
        }

        public void AddTileAt(Vector3Int clickPos) {
            controller.AddTileAt(clickPos.x, clickPos.y, clickPos.z, selectedTileType);
        }
    }
}
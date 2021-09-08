using Grid;
using UnityEngine;

namespace LevelEditor {
    public class LevelEditor : MonoBehaviour {
        
        [SerializeField] private Cursor cursor;
        [Header("Settings")]
        [SerializeField] private EOperation operation;
        // selected view
        // 
        // selection
        // [SerializeField] private ;

        [SerializeField] private GridController controller;

        public void AddMultipleTilesAt(Vector3Int clickPos, Vector3Int dragPos) {
            
        }

        public void AddTileAt(Vector3Int clickPos) {
            
        }
    }
}
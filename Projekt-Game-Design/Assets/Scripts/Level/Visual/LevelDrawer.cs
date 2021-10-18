using UnityEngine;

namespace Visual {
    public class LevelDrawer : MonoBehaviour {
        
        [SerializeField] private TileMapDrawer drawer;
        
        [Header("Receiving Events On")]
        [SerializeField] private VoidEventChannelSO levelLoaded;
        
        [Header("SendingEventsOn")]
        [SerializeField] private VoidEventChannelSO updateMeshEC;
        
        public void Awake() {
            levelLoaded.OnEventRaised += RedrawLevel;
        }
        
        public void RedrawLevel() {
            drawer.DrawGrid();
            updateMeshEC.RaiseEvent();
        }
    }
}
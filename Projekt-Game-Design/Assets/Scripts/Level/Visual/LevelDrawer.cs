using System;
using UnityEngine;

namespace Visual {
	public class LevelDrawer : MonoBehaviour {
		[Header("Receiving Events On")] 
		[SerializeField] private VoidEventChannelSO levelLoaded;
		[SerializeField] private VoidEventChannelSO redrawLevelEC;
		
		[Header("SendingEventsOn")] [SerializeField]
		private VoidEventChannelSO updateMeshEC;

		
		[SerializeField] private TileMapDrawer drawer;
		[SerializeField] private PrefabGridDrawer objectDrawer;
		
		public void Awake() {
			levelLoaded.OnEventRaised += RedrawLevel;
			redrawLevelEC.OnEventRaised += RedrawLevel;
		}

		private void OnDestroy() {
			levelLoaded.OnEventRaised -= RedrawLevel;
			redrawLevelEC.OnEventRaised -= RedrawLevel;
		}

		public void RedrawLevel() {
			drawer.DrawGrid();
			if ( objectDrawer ) {
				objectDrawer.RedrawItems();	
			}
			updateMeshEC.RaiseEvent();
		}
	}
}
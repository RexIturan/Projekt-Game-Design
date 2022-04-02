using System;
using UnityEngine;

namespace Visual {
	public class LevelDrawer : MonoBehaviour {
///// Data Types ///////////////////////////////////////////////////////////////////////////////////		
		
		private enum ELevelType {
			SingleMesh,
			GameObjectPerTile
		}

///// Serialized Fields ////////////////////////////////////////////////////////////////////////////		
		
		[Header("Receiving Events On")] 
		[SerializeField] private VoidEventChannelSO levelLoaded;
		[SerializeField] private VoidEventChannelSO redrawLevelEC;
		
		[Header("SendingEventsOn")] [SerializeField] private VoidEventChannelSO updateMeshEC;
		[SerializeField] private TileMapDrawer drawer;
		[SerializeField] private PrefabGridDrawer tileObjectDrawer;
		[Header("Settings"), SerializeField] private ELevelType _levelType;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		[ContextMenu("GenerateLevel")]
		private void GenerateLevel() {
			switch ( _levelType ) {
				case ELevelType.SingleMesh:
					drawer?.DrawGrid();
					updateMeshEC.RaiseEvent();	
					break;
				
				case ELevelType.GameObjectPerTile:
					//TODO TileObjectController
					tileObjectDrawer.GenerateTiles();
					break;
			}
		}
		
///// Public Functions /////////////////////////////////////////////////////////////////////////////		
		
		public void RedrawLevel() {
			GenerateLevel();
		}

///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		public void Awake() {
			levelLoaded.OnEventRaised += RedrawLevel;
			redrawLevelEC.OnEventRaised += RedrawLevel;
		}

		private void OnDestroy() {
			levelLoaded.OnEventRaised -= RedrawLevel;
			redrawLevelEC.OnEventRaised -= RedrawLevel;
		}
	}
}
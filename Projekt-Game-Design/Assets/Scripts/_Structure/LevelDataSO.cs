using System;
using System.Collections.Generic;
using Grid;
using SceneManagement.ScriptableObjects;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace GDP01.Structure {
	[CreateAssetMenu(fileName = "LevelData", menuName = "Level/Level Data", order = 0)]
	public class LevelDataSO : ScriptableObject {
		[SerializeField] private string levelName;
		[SerializeField] private GameSceneSO scene;
		[SerializeField] private ELevelType type;
		
		//todo unify below
		[SerializeField] private GridContainerSO gridContainerSO;
		[SerializeField] private GridDataSO gridDataSO;
		[SerializeField] private Mesh mesh;
		
		[SerializeField] private List<LevelConnectorSO> connectors;
		// [SerializeField] private Save gameplaySave;

///// Properties ///////////////////////////////////////////////////////////////////////////////////
		#region Properties

		public GameSceneSO Scene => scene;
		public List<LevelConnectorSO> Connectors => connectors;

		#endregion
		
		
///// Public Functions /////////////////////////////////////////////////////////////////////////////		
		
#if UNITY_EDITOR
		public void AddNewConnector() {
			// TODO generate name from level name + connector + index

			LevelConnectorSO connector = ScriptableObject.CreateInstance<LevelConnectorSO>();
			connector.LevelData = this;
			connector.name = "Connector_SO_" + connectors.Count;
			connector.Id = connectors.Count;
			connectors.Add(connector);
			
			
			AssetDatabase.AddObjectToAsset(connector, this);
			AssetDatabase.SaveAssets();
		}
#endif
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////		

		private void OnValidate() {
#if UNITY_EDITOR
			
			//TODO remove nulls
			//TODO check ids
			//TODO rename if needed
			
#endif
		}
	}

	internal enum ELevelType {
		Tactics,
		Macro
	}
}
﻿using System;
using UnityEngine;

namespace GDP01.Structure {
	// [CreateAssetMenu(fileName = "Connector", menuName = "Level/Connector", order = 0)]
	public class LevelConnectorSO : ScriptableObject {
		#region Public Types

			[Flags]
			public enum ELevelConnectorType {
				None     = 0,
				Entrance = 1,
				Exit     = 2,
			}		

		#endregion

		#region Serialized Fields

			[SerializeField] private string connectorName;
			[SerializeField] private int id;
			[SerializeField] private LevelDataSO levelData;
			[SerializeField] private Vector3Int gridPos;
			[SerializeField] private Vector3Int range;
			[SerializeField] private Vector3Int[] triggerPositions;
			[SerializeField] private ELevelConnectorType type;
			[SerializeField] private LevelConnectorSO target;

		#endregion

		#region Properties
			//todo properties
			
			public int Id {
				get => id;
				set => id = value;
			}
			
			public LevelDataSO LevelData { get => levelData; set => levelData = value; }
			
			public string ConnectorName => connectorName;
			public Vector3Int GridPos => gridPos;
			public Vector3Int Range => range;
			public Vector3Int[] TriggerPositions => triggerPositions;
			public ELevelConnectorType Type => type;
			public LevelConnectorSO Target => target;

		#endregion

		public bool IsExit => type.HasFlag(ELevelConnectorType.Exit);
		public bool IsEntrance => type.HasFlag(ELevelConnectorType.Entrance);
	}
}
using System;
using UnityEngine;

namespace GDP01.Structure {
	// [CreateAssetMenu(fileName = "Connector", menuName = "Level/Connector", order = 0)]
	public class LevelConnectorSO : ScriptableObject {
		
		[SerializeField] private string connectorName;		
		[SerializeField] private int id;
		[SerializeField] private Vector3Int gridPos;
		[SerializeField] private Vector3Int range;
		[SerializeField] private Vector3Int[] triggerPositions;
		[SerializeField] private ELevelConnectorType type;
		[SerializeField] private LevelConnectorSO[] targets;
		
		//todo properties
		
		public int Id {
			get => id;
			set => id = value;
		}
		
		public string ConnectorName => connectorName;
		public Vector3Int GridPos => gridPos;
		public Vector3Int Range => range;
		public Vector3Int[] TriggerPositions => triggerPositions;
		public ELevelConnectorType Type => type;
		public LevelConnectorSO[] Targets => targets;
	}

	[Flags]
	public enum ELevelConnectorType {
		None     = 0,
		Entrance = 1,
		Exit     = 2,
	}
}
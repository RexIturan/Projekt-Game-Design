using UnityEngine;

namespace MeshGenerator {
	public class MeshController : MonoBehaviour {
		
		[Header("Receiving Events On")] [SerializeField]
		private VoidEventChannelSO updateMeshEC;

		[Header("Settings")] [SerializeField] private MeshGenerator meshGenerator;


///// Private Functions ////////////////////////////////////////////////////////////////////////////        

		private void UpdateMesh() {
			// Debug.Log("update Mesh");
			meshGenerator.UpdateTileData();
			meshGenerator.GenerateMesh();
			meshGenerator.UpdateMesh();
		}

///// Callbacks //////////////////////////////////////////////////////////////////////////////////// 

		private void HandleUpdateMesh() {
			UpdateMesh();
		}

///// Public Functions /////////////////////////////////////////////////////////////////////////////

#if UNITY_EDITOR
		public void CreateMesh(){
			UpdateMesh();
		}
#endif

///// Unity Functions //////////////////////////////////////////////////////////////////////////////
		
		private void OnEnable() {
			updateMeshEC.OnEventRaised += HandleUpdateMesh;
		}

		private void OnDisable() {
			updateMeshEC.OnEventRaised -= HandleUpdateMesh;
		}
	}
}
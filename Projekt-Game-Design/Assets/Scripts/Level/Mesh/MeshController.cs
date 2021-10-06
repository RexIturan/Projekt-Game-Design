using System;
using UnityEngine;

namespace MeshGenerator {
    public class MeshController : MonoBehaviour {

        [Header("Receiving Events On")] 
        [SerializeField] private VoidEventChannelSO updateMeshEC;

        [Header("Settings")] 
        [SerializeField] private MeshGenerator meshGenerator;

        private void Awake() {
            updateMeshEC.OnEventRaised += HandleUpdateMesh;
        }

        private void OnDisable() {
            updateMeshEC.OnEventRaised -= HandleUpdateMesh;
        }

        private void HandleUpdateMesh() {
            // Debug.Log("update Mesh");
            meshGenerator.UpdateTileData();
            meshGenerator.GenerateMesh();
            meshGenerator.UpdateMesh();
        }
    }
}
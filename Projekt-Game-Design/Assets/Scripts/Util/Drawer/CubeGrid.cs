using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;

namespace GDP01.Util.Drawer {
	public class CubeGrid : Visualization {
		
///// Abstreact Methode Impl		
		protected override void EnableVisualization(int dataLength, MaterialPropertyBlock propertyBlock) {
		}

		protected override void DisableVisualization() {
		}

		protected override void UpdateVisualization(NativeArray<float3x4> positions, int resolution, JobHandle handle) {
			handle.Complete();
		}
	}
}
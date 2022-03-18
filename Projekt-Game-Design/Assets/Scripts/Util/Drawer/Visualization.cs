using _PseudoRandomNoise.Scripts.Hashing;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;

public abstract class Visualization : MonoBehaviour {
	public enum Shape { Plane, Sphere, Torus }

	static Shapes.ScheduleDelegate[] shapeJobs = {
		Shapes.Job<Shapes.Plane>.ScheduleParallel,
		Shapes.Job<Shapes.Sphere>.ScheduleParallel,
		Shapes.Job<Shapes.Torus>.ScheduleParallel
	};
	
	private static readonly int
		positionsId = Shader.PropertyToID("_Positions"),
		normalsId = Shader.PropertyToID("_Normals"),
		configID = Shader.PropertyToID("_Config");

	[SerializeField] private Mesh instanceMesh;
	[SerializeField] private Material material;
	[SerializeField, Range(1, 1024)] private int resolution = 256;
	[SerializeField, Range(-0.5f, 0.5f)] private float displacement = 0.1f;
	[SerializeField] private Shape shape;
	[SerializeField, Range(0.1f, 10f)] private float instanceScale = 2f;
	
///// Private Variables

	private NativeArray<float3x4> positions, normals;
	private ComputeBuffer positionsBuffer, normalsBuffer;
	private MaterialPropertyBlock propertyBlock;
	private bool isDirty;
	private Bounds bounds;
	
///// Abstract Methodes

	protected abstract void EnableVisualization (int dataLength, MaterialPropertyBlock propertyBlock);
	protected abstract void DisableVisualization ();

	protected virtual void UpdateVisualization(
		NativeArray<float3x4> positions, int resolution, JobHandle handle) {
		handle.Complete();
	}
	
///// Unity Functions

	//Setup
	private void OnEnable() {
		isDirty = true;
		
		int length = resolution * resolution;
		length = length / 4 + (length & 1);
		positions = new NativeArray<float3x4>(length, Allocator.Persistent);
		normals = new NativeArray<float3x4>(length, Allocator.Persistent);
		positionsBuffer = new ComputeBuffer(length * 4, 3 * 4);
		normalsBuffer = new ComputeBuffer(length * 4, 3 * 4);

		propertyBlock ??= new MaterialPropertyBlock();
		EnableVisualization(length, propertyBlock);
		propertyBlock.SetBuffer(positionsId, positionsBuffer);
		propertyBlock.SetBuffer(normalsId, normalsBuffer);
		propertyBlock.SetVector(configID, new Vector4(resolution, instanceScale / resolution, displacement));
	}

	//Cleanup
	private void OnDisable() {
		positions.Dispose();
		normals.Dispose();
		positionsBuffer.Release();
		normalsBuffer.Release();
		positionsBuffer = null;
		normalsBuffer = null;
		DisableVisualization();
	}

	private void OnValidate() {
		if (positionsBuffer != null && enabled) {
			OnDisable();
			OnEnable();
		}
	}

	private void Update() {
		if (isDirty || transform.hasChanged) {
			isDirty = false;
			transform.hasChanged = false;
			
			UpdateVisualization(
				positions, resolution,
				shapeJobs[(int)shape](
					positions, normals, resolution, transform.localToWorldMatrix, default
				)
			);
			
			positionsBuffer.SetData(positions.Reinterpret<float3>(3 * 4 * 4));
			normalsBuffer.SetData(normals.Reinterpret<float3>(3 * 4 * 4));

			// 1x1x1 cube?
			// bounds = new Bounds(
			// 	transform.position,
			// 	float3(2f * cmax(abs(transform.lossyScale)) + displacement));
			
			bounds = new Bounds(
				transform.position,
				float3(resolution * instanceScale));
		}
		
		Graphics.DrawMeshInstancedProcedural(
			instanceMesh, 0, material, bounds,
			resolution * resolution, propertyBlock);
	}
}

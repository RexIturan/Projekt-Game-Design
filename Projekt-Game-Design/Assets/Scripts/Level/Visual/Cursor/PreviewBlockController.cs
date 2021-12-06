using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor {
	public class PreviewBlockController : MonoBehaviour {
		[SerializeField] private CursorMaterialPair[] _pairs;
		[SerializeField] private GameObject modlel;
		[SerializeField] private CursorMode cursorMode = CursorMode.Select;
		[SerializeField] private Vector3 defaultScale = Vector3.one;
		
		private MeshRenderer _meshRenderer;
		private Dictionary<CursorMode, Material> cursorMaterialDict = new Dictionary<CursorMode, Material>();

		private void Awake() {
			InitPreviewBlock();
		}

		private void InitPreviewBlock() {
			_meshRenderer = modlel.GetComponent<MeshRenderer>();
			cursorMaterialDict = new Dictionary<CursorMode, Material>();
			foreach ( var pair in _pairs ) {
				cursorMaterialDict.Add(pair.cursorMode, pair.material);
			}
		}

		public void ResetScale() {
			transform.localScale = defaultScale;
		}
		
		public void UpdatePreviewColor(CursorMode mode) {
			if ( _meshRenderer is null || cursorMaterialDict is null || !cursorMaterialDict.ContainsKey(mode) ) {
				InitPreviewBlock();
			}

			cursorMode = mode;
			var mat = cursorMaterialDict[cursorMode];
			if ( mat is { } ) {
				_meshRenderer.material = mat;				
			}
		}

		public void UpdatePreviewColor() {
			UpdatePreviewColor(cursorMode);
		}

		[Serializable]
		public struct CursorMaterialPair {
			public CursorMode cursorMode;
			public Material material;
		}
		
		
	}
}
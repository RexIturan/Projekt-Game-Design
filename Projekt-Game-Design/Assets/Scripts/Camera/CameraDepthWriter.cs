using System;
using UnityEngine;

namespace DefaultNamespace.Camera {
	public class CameraDepthWriter : MonoBehaviour {
		[SerializeField] private UnityEngine.Camera _camera;
		[SerializeField] private RenderTexture _renderTexture;
		[SerializeField] private Material _material;
		[SerializeField] private int _blendMode;

		private void OnValidate() {
		}

		private void OnRenderImage(RenderTexture src, RenderTexture dest) {
			_camera.depthTextureMode = DepthTextureMode.Depth;
			
			//set our lightmap texture as active render texture
			var active = RenderTexture.active;
			RenderTexture.active = src;
			//clear the lightmap
			GL.Clear(true, true, Color.clear);
			RenderTexture.active = active;
			//RenderTexture is the target texture of the ImageEffectCamera
			_camera.SetTargetBuffers(src.colorBuffer, src.depthBuffer);
			_camera.Render();
 
			// _material.SetTexture("_Overlay", _renderTexture);
			Graphics.Blit(src, _material, (int)_blendMode); 
		}
	}
}
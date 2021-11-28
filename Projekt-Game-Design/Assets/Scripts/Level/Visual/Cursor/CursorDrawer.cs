using System;
using UnityEngine;
using Util.Extensions;

namespace LevelEditor {
	public class CursorDrawer : MonoBehaviour {
		[SerializeField] private GameObject cursorPrefab;
		[SerializeField] private GameObject cursor;

		private PreviewBlockController cursorController;

		private void Awake() {
			InitCursor();
		}

		public void InitCursor() {
			if ( cursor is { } ) {
				GameObject.Destroy(cursor);
			}
			
			cursor = GameObject.Instantiate(cursorPrefab, transform);
			cursorController = cursor.GetComponent<PreviewBlockController>();
		}
		
		public void DrawCursorAt(Vector3 worldPos, PreviewBlockController.CursorMode mode) {
			ShowCursor();
			cursorController.ResetScale();
			cursorController.UpdatePreviewColor(mode);
			cursor.transform.position = worldPos;
		}

		public void DrawBoxCursorAt(Vector3 worldPosStart, Vector3 worldPosEnd, PreviewBlockController.CursorMode mode) {
			var minPos = Vector3.Min(worldPosStart, worldPosEnd);
			var maxPos = Vector3.Max(worldPosStart, worldPosEnd);
			
			var diffPos = maxPos - minPos;
			//todo(vincent) hack -> [- new Vector3(0,0.5f,0)]
			var centerPos = minPos + new Vector3(diffPos.x * 0.5f, 0, diffPos.z  * 0.5f); 
			
			diffPos += Vector3.one;
			
			ShowCursor();
			cursorController.ResetScale();
			cursorController.UpdatePreviewColor(mode);
			cursor.transform.localScale = diffPos.Abs();
			cursor.transform.position = centerPos;
		}
		
		public void ShowCursor() {
			cursor.SetActive(true);
		}
		
		public void HideCursor() {
			cursor.SetActive(false);
		}
	}
}
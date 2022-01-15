using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace WorldObjects.Doors {

	[Serializable]
	public struct LockTransformData {
		public List<Vector3> positions;
		public Vector3 scale;
	}
	
	public class SlidingDoorController : MonoBehaviour {
		[SerializeField] private SlidingBlockAnimator slidingBlockAnimator;
		[SerializeField] private List<LockTransformData> _lockPositions = new List<LockTransformData>();
		[SerializeField] private int lockCount = 1;
		[SerializeField] private GameObject lockPrefab;
		[SerializeField] private GameObject lockParent;
		
		//runtime
		[SerializeField] private List<LockAnimator> lockAnimators = new List<LockAnimator>();

		public void InitSlidingDoor() {
			Reset();
			
			if ( lockAnimators.Count > 0) {
				foreach ( var lockAnimator in lockAnimators ) {
					if ( lockAnimator is { } ) {
						DestroyImmediate(lockAnimator.gameObject);	
					}
				}
				lockAnimators.Clear();
			}
			
			var lockTransformData = _lockPositions.Find(positions => positions.positions.Count == lockCount);
			if ( lockTransformData is { } ) {
				foreach ( var position in lockTransformData.positions ) {
					var lockObj = Instantiate(lockPrefab, lockParent.transform);
					var animator = lockObj.GetComponent<LockAnimator>();
					lockAnimators.Add(animator);
					lockObj.transform.localPosition = position;
					var currentScale = lockObj.transform.localScale;
					currentScale.Scale(lockTransformData.scale);
					lockObj.transform.localScale = currentScale;
				}	
			}
		}

		public void Reset() {
			foreach ( var lockAnimator in lockAnimators ) {
				lockAnimator.Reset();
			}
			slidingBlockAnimator.Reset();
		}
		
		public void OpenDoor() {
			Sequence sequence = DOTween.Sequence();
			
			foreach ( var lockAnimator in lockAnimators ) {
				if(!lockAnimator.IsOpen)
					sequence.Append(lockAnimator.AnimationTween());
				// sequence.AppendInterval(1);
			}
			
			sequence.Append(slidingBlockAnimator.AnimationTween());
			sequence.PlayForward();			
		}
	}
}
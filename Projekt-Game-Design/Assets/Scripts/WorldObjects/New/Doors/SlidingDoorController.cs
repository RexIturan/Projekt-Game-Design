using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace WorldObjects.Doors {

	public enum DoorType {
		Key,
		Switch,
		Proximity
	}
	
	[Serializable]
	public struct LockTransformData {
		public List<Vector3> positions;
		public Vector3 scale;
	}
	
	public class SlidingDoorController : MonoBehaviour {
		[SerializeField] private SlidingBlockAnimator slidingBlockAnimator;
		[SerializeField] private List<LockTransformData> _lockPositions = new List<LockTransformData>();
		[SerializeField] private DoorType _type;
		[SerializeField] private int lockCount = 1;
		[SerializeField] private GameObject lockPrefab;
		[SerializeField] private GameObject keyLockPrefab;
		[SerializeField] private GameObject lockParent;
		[SerializeField] private GameObject keyLockParent;

		private int lastUnlockedIndex = 0;
		
		//runtime
		private List<IKeyAnimator> lockAnimators = new List<IKeyAnimator>();

		public void InitValues(DoorType type, int count) {
			_type = type;
			lockCount = count;
			InitSlidingDoor();
		}
		
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

					GameObject lockObj = null; 
					
					switch ( _type ) {
						case DoorType.Switch:
							lockObj = Instantiate(lockPrefab, lockParent.transform);
							var currentScale = lockObj.transform.localScale;
							currentScale.Scale(lockTransformData.scale);
							lockObj.transform.localScale = currentScale;
							lockAnimators.Add(lockObj.GetComponent<LockAnimator>());
							break;
						
						case DoorType.Key:
							lockObj = Instantiate(keyLockPrefab, keyLockParent.transform);
							lockAnimators.Add(lockObj.GetComponent<KeyLockAnimator>());
							break;
						default:
							break;
					}

					if ( lockObj is { } ) {
						lockObj.transform.localPosition = position;
					}
				}	
			}
		}

		public void Reset() {

			lastUnlockedIndex = 0;
			
			foreach ( var lockAnimator in lockAnimators ) {
				if ( lockAnimator is { } ) {
					lockAnimator.Reset();	
				}
			}
			slidingBlockAnimator.Reset();
			//todo move somewhere else
			foreach ( var lockAnimator in lockAnimators ) {
				if ( lockAnimator is KeyLockAnimator keyLockAnimator ) {
					lockAnimator.transform.localPosition = slidingBlockAnimator.startPos;
				}
			}
		}

		public void OpenLock() {
			OpenLock(lastUnlockedIndex);
		}

		public void OpenLock(int index) {
			if ( index < lockAnimators.Count ) {
				lockAnimators[index].AnimationTween().Play();
				lastUnlockedIndex++;
			}
		}
		
		public void OpenDoor() {
			Sequence sequence = DOTween.Sequence();
			
			foreach ( var lockAnimator in lockAnimators ) {
				if ( lockAnimator is { } ) {
					if ( !lockAnimator.IsOpen ) {
						sequence.Append(lockAnimator.AnimationTween());
					}	
				}
			}
			
			sequence.Append(slidingBlockAnimator.AnimationTween());
			
			foreach ( var lockAnimator in lockAnimators ) {
				if ( lockAnimator is KeyLockAnimator keyLockAnimator ) {
					sequence.Join(slidingBlockAnimator.GetAnimation(lockAnimator.transform));
				}
			}

			sequence.PlayForward();			
		}
	}
}
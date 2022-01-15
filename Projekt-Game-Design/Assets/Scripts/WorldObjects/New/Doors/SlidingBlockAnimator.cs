using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace WorldObjects.Doors {
	public class SlidingBlockAnimator : MonoBehaviour {
		[SerializeField] private Transform doorModel;
		
		public Vector3 startPos;
		[SerializeField] private Vector3 moveDistance;
		[SerializeField] private float moveCycleLength;
		[SerializeField] private Ease moveEase = Ease.Linear;

		public void StartAnimation() {
			AnimationTween().Play();
		}
		
		public Tween AnimationTween() {
			return GetAnimation(doorModel);
		}

		public Tween GetAnimation(Transform transform) {
			transform.localPosition = startPos;
			
			var targetPos = startPos + moveDistance;
			return transform.DOLocalMove(targetPos, moveCycleLength)
				.SetEase(moveEase)
				.OnComplete(() => {
					//do stuff -> maybe callback um das pathfinding zu updaten?
				}).Pause();
		}

		public void Reset() {
			doorModel.localPosition = startPos;
		}
	}
}
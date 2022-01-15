using DG.Tweening;
using UnityEngine;

namespace WorldObjects.Doors {
	public class DoorAnimator : MonoBehaviour {
		[SerializeField] private Transform doorModel;
		
		[SerializeField] private Vector3 startPos;
		[SerializeField] private Vector3 moveDistance;
		[SerializeField] private float moveCycleLength;
		[SerializeField] private Ease moveEase = Ease.Linear;
		
		public void OpenDoor() {
			var targetPos = startPos + moveDistance;
			doorModel.DOLocalMove(targetPos, moveCycleLength)
				.SetEase(moveEase).SetLoops(2, LoopType.Yoyo)
				.OnComplete(() => {
					//do stuff -> maybe callback um das pathfinding zu updaten?
				});
		}
	}
}
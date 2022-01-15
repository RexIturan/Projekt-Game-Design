using DG.Tweening;
using UnityEngine;

namespace WorldObjects.Doors {
	public class LockAnimator : MonoBehaviour {
		[SerializeField] private Transform lockModel;
		[SerializeField] private MeshRenderer meshRenderer;
		
		[SerializeField] private Material activeMaterial;
		[SerializeField] private Material inctiveMaterial;

		[SerializeField] private Vector3 moveDistance;
		[SerializeField] private float moveCycleLength;
		[SerializeField] private bool isOpen = false;
		public Vector3 startPos;
		public bool openRight;
		
		public bool IsOpen => isOpen;
		
		public void OpenLock() {
			AnimationTween().Play();
		}
		
		public Tween AnimationTween() {
			var targetPos = startPos;
			targetPos += openRight ? moveDistance : -moveDistance;
			return lockModel.DOLocalMove(targetPos, moveCycleLength)
				.SetEase(Ease.Linear)
				.OnComplete(() => {
					meshRenderer.material = activeMaterial;
					isOpen = true;
				}).Pause();
		}

		public void Reset() {
			lockModel.localPosition = startPos;
			isOpen = false;
			meshRenderer.material = inctiveMaterial;
		}
	}
}
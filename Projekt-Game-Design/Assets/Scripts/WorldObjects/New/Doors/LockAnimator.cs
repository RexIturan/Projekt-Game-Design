using DG.Tweening;
using UnityEngine;

namespace WorldObjects.Doors {
	public class LockAnimator : IKeyAnimator {
		[SerializeField] private Transform lockModel;
		[SerializeField] private MeshRenderer meshRenderer;
		
		[SerializeField] private Material activeMaterial;
		[SerializeField] private Material inctiveMaterial;

		[SerializeField] private Vector3 moveDistance;
		[SerializeField] private float moveCycleLength;
		[SerializeField] private bool isOpen = false;
		public Vector3 startPos;
		public bool openRight;
		
		public override bool IsOpen {
			get => isOpen;
			set => isOpen = value;
		}

		public void OpenLock() {
			AnimationTween().Play();
		}
		
		public override Tween AnimationTween() {
			var targetPos = startPos;
			targetPos += openRight ? moveDistance : -moveDistance;
			return lockModel.DOLocalMove(targetPos, moveCycleLength)
				.SetEase(Ease.Linear)
				.OnComplete(() => {
					meshRenderer.material = activeMaterial;
					isOpen = true;
				}).Pause();
		}

		public override void Reset() {
			lockModel.localPosition = startPos;
			isOpen = false;
			meshRenderer.material = inctiveMaterial;
		}
	}
}
using DG.Tweening;
using UnityEngine;

namespace WorldObjects.Doors {
	//todo better inheritence
	public class KeyLockAnimator : IKeyAnimator {
		[SerializeField] private Transform lockModel;
		[SerializeField] private Transform keyModel;

		[SerializeField] private GameObject keyPrefab;
		[SerializeField] private Vector3 startRotation;
		[SerializeField] private Vector3 rotationAmount;
		[SerializeField] private float rotationCycleLength;
		
		[SerializeField] private Vector3 keyStartPos;
		[SerializeField] private Vector3 keyMoveDistance;
		[SerializeField] private float keyMoveCycleLength;
		
		[SerializeField] private Vector3 startPos;
		[SerializeField] private Vector3 moveDistance;
		[SerializeField] private float moveCycleLength;
		[SerializeField] private bool isOpen = false;
		[SerializeField] private bool showKey = false;
		
		public override bool IsOpen {
			get => isOpen;
			set => isOpen = value;
		}

		///// Private Functions

		private Tween GetKeyAnimation() {
			keyModel.localRotation = Quaternion.Euler(startRotation);
			keyModel.localPosition = keyStartPos;
			var targetRotation = startRotation + rotationAmount;
			var targetPosition = keyStartPos + keyMoveDistance;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(keyModel.DOLocalMove(targetPosition, keyMoveCycleLength)
				.SetEase(Ease.InQuad));
			sequence.Append(keyModel.DOLocalRotate(targetRotation, rotationCycleLength).SetEase(Ease.InQuad));
			
			return sequence.Pause();
		}

		private Tween GetOpenLockAnimation() {
			lockModel.localPosition = startPos;
			var targetPos = startPos + moveDistance;
			return lockModel.DOLocalMove(targetPos, moveCycleLength).SetEase(Ease.InQuad).Pause();
		}
		
///// Public Functions		

		public void ToggleKey(bool value) {
			showKey = value;
			keyModel.gameObject.SetActive(value);
		}

		public void OpenLock() {
			AnimationTween().Play();
		}
		
		public override Tween AnimationTween() {
			ToggleKey(true);
			
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(0.5f);
			sequence.Append(GetKeyAnimation());
			sequence.Append(GetOpenLockAnimation());
			sequence.onComplete += () => isOpen = true;
			return sequence;
		}
		
		public override void Reset() {
			ToggleKey(false);
			lockModel.localPosition = startPos;
			keyModel.localRotation = Quaternion.Euler(startRotation);
			isOpen = false;
			showKey = false;
		}
	}

}

using DG.Tweening;
using UnityEngine;

namespace WorldObjects.Doors {
	public class LockAnimator : MonoBehaviour {
		[SerializeField] private Transform lockModel;
		[SerializeField] private MeshRenderer meshRenderer;
		
		[SerializeField] private Material activeMaterial;
		[SerializeField] private Material inctiveMaterial;
		
		[SerializeField] private Vector3 startPos;
		[SerializeField] private Vector3 moveDistance;
		[SerializeField] private float moveCycleLength;
		[SerializeField] private bool openRight;

		public void OpenLock() {
			var targetPos = startPos;
			targetPos += openRight ? moveDistance : -moveDistance;
			lockModel.DOLocalMove(targetPos, moveCycleLength)
				.SetEase(Ease.Linear)
				.OnComplete(() => { meshRenderer.material = activeMaterial;});
		}
	}
}
using DG.Tweening;
using UnityEngine;

namespace WorldObjects {
	public class SwitchAnimator : MonoBehaviour {
		[SerializeField] private Transform switchLeaver;
		[SerializeField] private MeshRenderer meshRenderer;

		[SerializeField] private Vector3 startRotation = new Vector3(0, 0, 45);
		[SerializeField] private Vector3 rotationDelta = new Vector3(0, 0, 90);
		[SerializeField] private Material activeMaterial;
		[SerializeField] private Material inctiveMaterial;
		[SerializeField] private float rotationCycleLength = 2;

		private bool SwitchState { get; set; } = false;
		
		private void Start() {
			switchLeaver.rotation = Quaternion.Euler(startRotation);
			meshRenderer.material = inctiveMaterial;
			rotationDelta = startRotation * 2;
		}

		public void FlipSwitch() {
			switchLeaver.DORotate(SwitchState ? -rotationDelta : rotationDelta, rotationCycleLength, RotateMode.WorldAxisAdd)
				.SetEase(Ease.Linear)
				.OnComplete(() => {
					meshRenderer.material = SwitchState ? activeMaterial : inctiveMaterial;
					SwitchState = !SwitchState;
				});
		}
	}
}
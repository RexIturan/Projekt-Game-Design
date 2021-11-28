using System;
using DG.Tweening;
using UnityEngine;

namespace Items {
	public class ItemRotator : MonoBehaviour {
		[SerializeField] private Transform itemModel;

		[SerializeField] private float rotationCycleLength = 2;
		[SerializeField] private float moveCycleDuration = 2;
		[SerializeField] private Ease moveEsse = Ease.InOutBounce;
		[SerializeField] private float moveHeight = 0.1f;
		
		private void Start() {

			var currentPosition = itemModel.position;
			var currentRotation = itemModel.rotation.eulerAngles;
			
			itemModel
				.DORotate(new Vector3(0, 360	, 0) + currentRotation, rotationCycleLength * 0.5f, RotateMode.FastBeyond360)
				.SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

			itemModel.DOMoveY(moveHeight + currentPosition.y, moveCycleDuration * 0.5f, false).SetLoops(-1, LoopType.Yoyo)
				.SetEase(moveEsse);

			// itemModel.transform.DOLocalRotate(
			// 		new Vector3(0, 360, 0), 
			// 		cycleLength * 0.5f, 
			// 		RotateMode.FastBeyond360 )
			// 	.SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
		}
	}
}
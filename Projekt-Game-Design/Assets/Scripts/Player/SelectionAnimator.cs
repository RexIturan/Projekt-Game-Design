using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Player {
	public class SelectionAnimator : MonoBehaviour {
		
		[SerializeField] private Transform model;

		[SerializeField] private Vector3 startSize = new Vector3(1, 1, 1);
		[SerializeField] private Vector3 targetSize = new Vector3(1, 1, 1);
		[SerializeField] private float scaleCycleLength = 2;
		[SerializeField] private Ease scaleEase = Ease.InOutBounce;

		private TweenerCore<Vector3,Vector3,VectorOptions> handle; 
		
		private void Start() {
			model.localScale = startSize;
			
			handle = model.DOScale(targetSize, scaleCycleLength)
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(scaleEase);
		}

		private void OnValidate() {
			handle.Complete();

			model.localScale = startSize;
			
			handle = model.DOScale(targetSize, scaleCycleLength)
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(scaleEase);
		}
	}
}
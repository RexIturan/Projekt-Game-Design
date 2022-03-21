using DG.Tweening;
using UnityEngine;

namespace WorldObjects.Doors {
	public abstract class IKeyAnimator : MonoBehaviour {
		public abstract void Reset();
		public abstract bool IsOpen { get; set; }
		public abstract Tween AnimationTween();
	}
}
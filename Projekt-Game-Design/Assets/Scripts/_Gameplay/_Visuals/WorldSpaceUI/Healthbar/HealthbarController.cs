using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util.Types;

namespace Visual.Healthbar {
	public class HealthbarController : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI _tmpText;
		[SerializeField] private Slider _slider;
		[SerializeField] private Color _color;
		[SerializeField] private Image image;
		
///// Private Functions	////////////////////////////////////////////////////////////////////////////
		private void UpdateText(float value, float max) {
			_tmpText.text = $"{value}/{max}";
		}
		
		private void UpdateSlider(float min, float max, float value) {
			_slider.minValue = min;
			_slider.maxValue = max;
			_slider.value = value;
		}
		
		private void SetFillColor() {
			image.color = _color;
		}
		
		private IEnumerator HideAfterDelay(float waitTime) {
			yield return new WaitForSeconds(waitTime);
			Hide();
		}
		
///// Public Functions /////////////////////////////////////////////////////////////////////////////

		public void SetColor(Color color) {
			_color = color;
			SetFillColor();
		}

		public void StartHideAfterDelay() {
			StartCoroutine(nameof(HideAfterDelay), 1.0F);
		}

		public void Hide() {
			gameObject.SetActive(false);
		}

		public void UpdateVisuals(RangedInt rangedInd) {
			float min = rangedInd.Min;
			float max = rangedInd.Max;
			float value = rangedInd.Value;
			
			UpdateText(value, max);
			UpdateSlider(min, max, value);
		}
		
///// Unity Functions	//////////////////////////////////////////////////////////////////////////////
		private void Awake() {
			// _statistics = GetComponentInParent<Statistics>();
			//
			// if ( _statistics is null ) {
			// 	Debug.LogError("HealthbarController needs a Statistics Component in its Parent");
			// }

			SetFillColor();
		}

		private void Update() {
		}

		private void OnValidate() {
			// SetFillColor();
		}
	}
}
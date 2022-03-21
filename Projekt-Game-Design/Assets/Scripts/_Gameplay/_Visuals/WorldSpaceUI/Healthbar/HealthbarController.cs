using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util.Types;

namespace Visual.Healthbar {
	public class HealthbarController : MonoBehaviour {
		[Header("Receiving events on ")]
		[SerializeField] private VoidEventChannelSO clearPreviewEvent;

		[SerializeField] private TextMeshProUGUI _tmpText;
		[SerializeField] private Slider _slider;
		[SerializeField] private Color _color;
		[SerializeField] private Image image;

		[SerializeField] private GameObject previewBox;
		[SerializeField] private TextMeshProUGUI _previewText;
		[SerializeField] private Color plusColor;
		[SerializeField] private Color minusColor;

		[SerializeField] private RectTransform _previewRect;
		[SerializeField] private Slider _previewSlider;
		[SerializeField] private Color _previewColor;
		[SerializeField] private Image _previewImage;
		
		[SerializeField] private float previewValue;
		
///// Private Functions	////////////////////////////////////////////////////////////////////////////
		private void UpdateText(float value, float max) {
			_tmpText.text = $"{value}/{max}";
		}
		
		private void UpdateSlider(float min, float max, float value) {
			_slider.minValue = min;
			_slider.maxValue = max;
			_slider.value = value;
		}
		
		private void UpdatePreviewSlider() {
			_previewSlider.minValue = _slider.minValue;
			_previewSlider.maxValue = _slider.maxValue;

			// bringing the value between -slider value and max slider value, 
			// so the preview bar can't overlap with bounds
			previewValue = Mathf.Max(previewValue, -_slider.value);
			previewValue = Mathf.Min(previewValue, _slider.maxValue - _slider.value);

			// set preview bar value
			_previewSlider.value = Mathf.Abs(previewValue);

			// the slider is positioned right at the end of the normal bar
			// or has some offset to the left if the preview value is negative
			float xPos = _previewRect.rect.width * ( _slider.normalizedValue - (previewValue < 0 ? _previewSlider.normalizedValue : 0));
			_previewRect.localPosition += Vector3.left * _previewRect.localPosition.x + Vector3.right * xPos;
		}

		private void UpdatePreviewBox() { 
			if(previewValue > 0) {
				previewBox.SetActive(true);
				_previewText.color = plusColor;
				_previewText.text = $"+{previewValue}";
			}
			else if(previewValue < 0) {
				previewBox.SetActive(true);
				_previewText.color = minusColor;
				_previewText.text = $"{previewValue}";
			}
			else {
				previewBox.SetActive(false);
			}
		}
		
		private void SetFillColor() {
			image.color = _color;
		}

		private void SetPreviewColor() {
			_previewImage.color = _previewColor;
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

		public void SetPreviewColor(Color color) {
			_previewColor = color;
			SetPreviewColor();
		}

		public void HidePreview() {
			SetPreviewValue(0);
		}
		
		/// <summary>
		/// Sets preview bar to given value. The higher the absolute value, 
		/// the more is covered by the preview bar. If the value is negative, 
		/// parts of the actual bar is covered by the preview bar. If the
		/// value is positive, the preview bar is on top of the actual bar. 
		/// The scaling corresponds to the values of the usual bar, 
		/// e.g. a preview value of 2 will cover one fourth of a health bar 
		/// with the interval 0 to 8. 
		/// </summary>
		/// <param name="value">Value, preview difference, e.g. +2 health points </param>
		public void SetPreviewValue(float value) {
			previewValue = value;
			UpdatePreviewSlider();
			UpdatePreviewBox();
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
			HidePreview();
			clearPreviewEvent.OnEventRaised += HidePreview;
		}

		private void OnDestroy() {
			clearPreviewEvent.OnEventRaised -= HidePreview;
		}

		private void OnValidate() {
			SetFillColor();
			SetPreviewColor();
		}

	}
}
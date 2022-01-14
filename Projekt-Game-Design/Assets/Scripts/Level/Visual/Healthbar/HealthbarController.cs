using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visual.Healthbar {
	public class HealthbarController : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI _tmpText;
		[SerializeField] private Slider _slider;
		[SerializeField] private Color _color;
		[SerializeField] private Image image;
		
		//todo dont use statistics directly
		private Statistics _statistics;
		
///// Private Functions		
		private void UpdateText(float value, float max) {
			_tmpText.text = $"{value}/{max}";
		}
		
		private void UpdateSlider(float min, float max, float value) {
			_slider.minValue = min;
			_slider.maxValue = max;
			_slider.value = value;
		}

		private IEnumerator HideAfterDelay(float waitTime) {
			while(true){
				yield return new WaitForSeconds(waitTime);
				Hide();
			}
		}
		
///// Public Functions

		public void StartHideAfterDelay() {
			StartCoroutine(nameof(HideAfterDelay), 1.0F);
		}

		public void Hide() {
			gameObject.SetActive(false);
		}

		public void UpdateVisuals() {
			float min = _statistics.StatusValues.HitPoints.min;
			float max = _statistics.StatusValues.HitPoints.max;
			float value = _statistics.StatusValues.HitPoints.value;
			
			UpdateText(value, max);
			UpdateSlider(min, max, value);
		}
		
///// Unity Functions		
		private void Awake() {
			//todo get statistics
			_statistics = GetComponentInParent<Statistics>();

			if ( _statistics is null ) {
				Debug.LogError("HealthbarController needs a Statistics Component in its Parent");
			}

			image.color = _color;
		}

		private void Update() {
			UpdateVisuals();
		}

		private void OnValidate() {
			image.color = _color;
		}
	}
}
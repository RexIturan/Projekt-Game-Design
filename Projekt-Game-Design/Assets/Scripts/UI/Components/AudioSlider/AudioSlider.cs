using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioSlider : VisualElement
{
		private Label label;
		private Slider slider;
		private Toggle toggle;

		private static readonly float MIN_VOLUME_VALUE = 0.0001f; // 10^(-4) for four decibels 
		private static readonly float MAX_VOLUME_VALUE = 1f;

		private static readonly string defaultStyleSheet = "audioSlider";
		private static readonly string className = "audioSlider";
		private static readonly string classNameLabel = "label";
		private static readonly string classNameLowerBox = "lowerBox";
		private static readonly string classNameSlider = "slider";

		private AudioMixer mixer;
		private string volumeParameter;

		private AudioSlider() {
				styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));

				AddToClassList(className);

				// Init label
				label = new Label();
				label.AddToClassList(classNameLabel);
				Add(label);

				// Init lower box that is composed of slider and toggle
				VisualElement lowerBox = new VisualElement();
				lowerBox.AddToClassList(classNameLowerBox);

				// Init slider
				slider = new Slider();
				slider.AddToClassList(classNameSlider);
				slider.lowValue = MIN_VOLUME_VALUE;
				slider.highValue = MAX_VOLUME_VALUE;
				slider.RegisterValueChangedCallback(_ => UpdateVolume());
				lowerBox.Add(slider);

				// Init toggle
				toggle = new Toggle();
				toggle.value = true;
				toggle.RegisterValueChangedCallback(_ => UpdateVolume());
				lowerBox.Add(toggle);

				Add(lowerBox);
		}

		public AudioSlider(string labelName, AudioMixer mixer, string volumeParameterName) : this() {
				InitComponent(labelName, mixer, volumeParameterName);

				float currentVolume;
				mixer.GetFloat(volumeParameter, out currentVolume);
				slider.value = MapToValue(currentVolume);
		}

		private void InitComponent(string labelName, AudioMixer mixer, string volumeParameterName) {
				label.text = labelName;
				this.mixer = mixer;
				volumeParameter = volumeParameterName;
		}

		private void UpdateVolume() {
				if(toggle.value )
						mixer.SetFloat(volumeParameter, MapToVolume(slider.value));
				else
						mixer.SetFloat(volumeParameter, MapToVolume(MIN_VOLUME_VALUE));
		}

		private float MapToVolume(float value) {
				return Mathf.Log10(value) * 20;
		}

		private float MapToValue(float volume) {
				return Mathf.Pow(10, volume / 20);
		}
}

using GDP01.Gameplay.Audio;
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
		private MixerGroupSettingsSO groupSettings;
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
				slider.RegisterValueChangedCallback(_ => HandleSliderChanged());
				lowerBox.Add(slider);

				// Init toggle
				toggle = new Toggle();
				toggle.RegisterValueChangedCallback(_ => HandleToggleChanged());
				lowerBox.Add(toggle);

				Add(lowerBox);
		}

		public AudioSlider(string labelName, AudioMixer mixer, MixerGroupSettingsSO groupSettings) : this() {
				InitComponent(labelName, mixer, groupSettings);
		}

		private void InitComponent(string labelName, AudioMixer mixer, MixerGroupSettingsSO groupSettings) {
				label.text = labelName;
				this.mixer = mixer;
				this.groupSettings = groupSettings;

				volumeParameter = groupSettings.volumeParameterName;

				// apply values of settings
				toggle.value = !groupSettings.muted;
				slider.value = MapToValue(groupSettings.volume);
		}

		private void HandleSliderChanged() {
				groupSettings.volume = MapToVolume(slider.value);
				UpdateVolume();
		}

		private void HandleToggleChanged() {
				groupSettings.muted = !toggle.value;
				UpdateVolume();
		}

		private void UpdateVolume() {
				if(toggle.value )
						mixer.SetFloat(volumeParameter, MapToVolume(slider.value));
				else
						mixer.SetFloat(volumeParameter, MapToVolume(MIN_VOLUME_VALUE));
		}

		private static float MapToVolume(float value) {
				return Mathf.Log10(value) * 20;
		}

		private static float MapToValue(float volume) {
				return Mathf.Pow(10, volume / 20);
		}
}

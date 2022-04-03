using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace GDP01.Gameplay.Audio
{
		[CreateAssetMenu(fileName = "mixerGroupSettings", menuName = "Audio/Mixer Group Settings")]
    public class MixerGroupSettingsSO : ScriptableObject
    {
				private static float MUTE_VOLUME;

				public new string name;
				public string volumeParameterName;
				public float volume;
				public bool muted;

				public AudioMixer mixer;
				
				private void OnEnable () {
						muted = false;
						float defaultVolume;
						mixer.GetFloat(volumeParameterName, out defaultVolume);
						volume = defaultVolume;
				}
		}
}

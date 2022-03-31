using Events.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
		public class AudioManager : MonoBehaviour
		{
				[SerializeField] private SoundEventChannelSO playSoundEC;
				[SerializeField] private VoidEventChannelSO stopAllSounds;
				
				private List<AudioSource> sources;

				private void Awake() {
						sources = new List<AudioSource>();

						playSoundEC.OnEventRaised += PlaySound;
						stopAllSounds.OnEventRaised += StopAllSounds;
				}

				private void OnDestroy() {
						playSoundEC.OnEventRaised -= PlaySound;
						stopAllSounds.OnEventRaised -= StopAllSounds;
				}

				private AudioSource CreateAudioSource(SoundSO sound) {
						AudioSource source = gameObject.AddComponent<AudioSource>();
						source.clip = sound.clip;
						source.volume = sound.volume;
						sources.Add(source);
						return source;
				}

				private void PlaySound(SoundSO sound) {
						if(sound != null) {
								Debug.Log("Playing Sound: " + sound.name);
								CreateAudioSource(sound).Play();
						}
				}

				private void StopAllSounds() {
						foreach(AudioSource source in sources) {
								source.Stop();
						}
				}
		}
}
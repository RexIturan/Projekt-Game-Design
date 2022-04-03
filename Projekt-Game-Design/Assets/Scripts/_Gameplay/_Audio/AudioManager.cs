using Events.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
		public class AudioManager : MonoBehaviour
		{
				[SerializeField] private SoundEventChannelSO playSoundEC;
				[SerializeField] private VoidEventChannelSO stopAllSounds;

				[SerializeField] private AudioMixerGroup musicGroup;
				[SerializeField] private AudioMixerGroup sfxGroup;
				[SerializeField] private AudioMixerGroup otherGroup;

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
						source.loop = sound.looped;
						source.volume = sound.volume;

						// add to mixer group
						switch(sound.type)
						{
								case SoundType.MUSIC:
										source.outputAudioMixerGroup = musicGroup;
										break;
								case SoundType.SFX_GAME:
								case SoundType.SFX_UI:
										source.outputAudioMixerGroup = sfxGroup;
										break;
								default:
										source.outputAudioMixerGroup = otherGroup;
										break;
						}

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

				private void RemoveUnusedAudioSources() {
						for (int i = 0; i < sources.Count;) {
								if(!sources[i].isPlaying) {
										AudioSource source = sources[i];
										sources.RemoveAt(i);
										Destroy(source);
								}
								else
										i++;
						}
				}

				private void Update() {
						RemoveUnusedAudioSources();
				}
		}
}
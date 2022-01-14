using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
		public class SoundManager : MonoBehaviour
		{
				public SoundData[] sounds;
				private AudioSource[] sources;

				public void Awake()
				{
						sources = new AudioSource[sounds.Length];
						
						for(int i = 0; i < sounds.Length; i++)
						{
								AudioSource source = gameObject.AddComponent<AudioSource>();
								source.clip = sounds[i].clip;
								source.volume = sounds[i].volume;

								sources[i] = source;
						}
				}

				public void PlaySound(string name)
				{
						int i = 0;
						while ( i < sounds.Length && !sounds[i].name.Equals(name) )
								i++;

						if ( i < sounds.Length )
						{
								Debug.Log("Playing Sound: " + name);
								sources[i].Play();
						}
				}

				public void StopAllSounds()
				{
						foreach(AudioSource source in sources)
						{
								source.Stop();
						}
				}

				public static SoundManager FindSoundManager() {
					return FindObjectOfType<SoundManager>();
				}
		}
}
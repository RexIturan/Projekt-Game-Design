using UnityEngine;

namespace Audio
{
		[CreateAssetMenu(fileName = "New Sound", menuName = "Audio/Sound")]
		public class SoundSO : ScriptableObject
		{
				public new string name;
				public AudioClip clip;
				public bool looped;
				public float volume;
				public SoundType type;
		}
}
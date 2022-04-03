using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDP01.Gameplay.Audio
{
		[CreateAssetMenu(fileName = "mixerGroupSettings", menuName = "Audio/Mixer Group Settings")]
    public class MixerGroupSettingsSO : ScriptableObject
    {
				public new string name;
				public string volumeParameterName;
				public float volume;
				public bool muted;
    }
}

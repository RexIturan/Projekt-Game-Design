using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
		[System.Serializable]
		public class CharacterIdentificationAppearance
		{
				[SerializeField] private string characterName;
				[SerializeField] private Sprite characterIcon;

				public string CharacterName { get { return characterName; } }
				public Sprite CharacterIcon { get { return characterIcon; } }
		}
}

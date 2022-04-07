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
				[SerializeField] private Mesh headModel;
				[SerializeField] private Mesh bodyModel;

				public string CharacterName { get { return characterName; } }
				public Sprite CharacterIcon { get { return characterIcon; } }
				public Mesh HeadModel { get { return headModel; } }
				public Mesh BodyModel { get { return bodyModel; } }
		}
}

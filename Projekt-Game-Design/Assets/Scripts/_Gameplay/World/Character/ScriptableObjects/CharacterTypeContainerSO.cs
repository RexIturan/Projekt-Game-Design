using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GDP01._Gameplay.World.Character {
	[CreateAssetMenu(fileName = "CharacterTypeContainerSO", menuName = "CharType/CharacterTypeContainerSO", order = 0)]
	public class CharacterTypeContainerSO : ScriptableObject {

		[SerializeField] private CharacterTypeSO defaultCharacterType;
		
		[SerializeField]
		private List<CharacterTypeSO> characterTypes =
			new List<CharacterTypeSO>();

		public CharacterTypeSO DefaultCharacterType => defaultCharacterType;

		public CharacterTypeSO GetCharacterTypeByGuid(string guid) {
			return characterTypes.FirstOrDefault(so => so.Guid.Equals(guid));
		}

		public CharacterTypeSO GetCharacterTypeByName(string name) {
			return characterTypes.FirstOrDefault(so => so.name.Equals(name));
		}
	}
}
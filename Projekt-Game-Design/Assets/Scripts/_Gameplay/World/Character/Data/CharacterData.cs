using System;
using Characters;
using Characters.Types;
using FullSerializer;
using GDP01.Gameplay.SaveTypes;
using UnityEngine;

namespace GDP01._Gameplay.World.Character.Data {
	[Serializable]
	public class CharacterData : SaveObjectCreatorData {
		[SerializeField] protected CharacterTypeSO _type = null;
		public CharacterTypeSO Type { get => _type; set => _type = value; }
		
		[SerializeField, fsIgnore] protected bool _active = false;
		[SerializeField, fsIgnore] protected string _name = "No-Name";
		[SerializeField, fsIgnore] protected Sprite _icon = null;
		[SerializeField, fsIgnore] protected Faction _faction = Faction.None;
		
		public string Name { get => _name; set => _name = value; }
		public bool Active { get => _active; set => _active = value; }
		public Sprite Icon { get => _icon; set => _icon = value; }
		public Faction Faction { get => _faction; set => _faction = value; }
		
		//Override
		//Character type
		//todo -> CharacterSO
		public StatusValues Stats { get; set; } = new StatusValues();
		
		public int MovementPointsPerEnergy { get; set; } = 20;
		public int MovementCostPerTile { get; set; } = 1;
		public AbilitySO[] BasicAbilities { get; set; } = Array.Empty<AbilitySO>();
	}
}
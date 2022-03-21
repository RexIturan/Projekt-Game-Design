using UnityEngine;
using UnityEngine.UIElements;

public class AbilitySlot : VisualElement {
	public readonly Image icon;
	public int abilityID = -1;

	private void SetAbility(Texture texture, int id) {
		icon.image = texture;
		abilityID = id;
	}
	
	public AbilitySlot() {
		//Create a new Image element and add it to the root
		icon = new Image();
		Add(icon);
		//Add USS style properties to the elements
		icon.AddToClassList("slotIcon");
	}

	public void SetAbility(AbilitySO ability) {
		SetAbility(ability.icon.texture, ability.id);
	}
	
	public void ClearAbility() {
		SetAbility(null, -1);
	}
}
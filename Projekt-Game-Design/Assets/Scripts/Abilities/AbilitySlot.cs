using UnityEngine.UIElements;

public class AbilitySlot : VisualElement {
	public readonly Image icon;
	public int abilityID = -1;

	public AbilitySlot() {
		//Create a new Image element and add it to the root
		icon = new Image();
		Add(icon);
		//Add USS style properties to the elements
		icon.AddToClassList("slotIcon");
	}

	public void HoldAbility(AbilitySO ability) {
		icon.image = ability.icon.texture;
		abilityID = ability.abilityID;
		// Debug.Log("Test in HoldItem");
	}

	public void DropAbility() {
		abilityID = -1;
		icon.image = null;
	}
}
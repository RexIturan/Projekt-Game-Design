using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilitySlot : VisualElement
{
    public Image Icon;
    public int AbilityID = -1;
    public AbilitySlot()
    {
        //Create a new Image element and add it to the root
        Icon = new Image();
        Add(Icon);
        //Add USS style properties to the elements
        Icon.AddToClassList("slotIcon");
    }
    
    public void HoldAbility(AbilitySO ability)
    {
        Icon.image = ability.icon.texture;
        AbilityID = ability.abilityID;
        // Debug.Log("Test in HoldItem");
    }
    public void DropAbility()
    {
        AbilityID = -1;
        Icon.image = null;
    }
}

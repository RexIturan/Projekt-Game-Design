using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public Image Icon;
    public int ItemGuid = -1;
    public InventorySlot()
    {
        //Create a new Image element and add it to the root
        Icon = new Image();
        Add(Icon);
        //Add USS style properties to the elements
        Icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");
    }
    
    public void HoldItem(ItemSO item)
    {
        Icon.image = item.icon.texture;
        ItemGuid = item.id;
        Debug.Log("Test in HoldItem");
    }
    public void DropItem()
    {
        ItemGuid = -1;
        Icon.image = null;
    }
}

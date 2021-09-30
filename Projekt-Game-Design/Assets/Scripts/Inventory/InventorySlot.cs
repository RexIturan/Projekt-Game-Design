using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
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
        RegisterCallback<PointerDownEvent>(OnPointerDown);
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
    
    private void OnPointerDown(PointerDownEvent evt)
    {
        //Not the left mouse button
        if (evt.button != 0 || ItemGuid.Equals(""))
        {
            return;
        }
        //Clear the image
        Icon.image = null;
        //Start the drag
        InventoryUIController.StartDrag(evt.position, this);
    }
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}



using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public readonly Image icon;
    public readonly InventorySlotType slotType;
    public int itemGuid = -1;
    
    public InventorySlot()
    {
        //Create a new Image element and add it to the root
        icon = new Image();
        Add(icon);
        //Add USS style properties to the elements
        icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");
        RegisterCallback<PointerDownEvent>(OnPointerDown);
        slotType = InventorySlotType.EquipmentInventory;
    }
    
    public InventorySlot(InventorySlotType type)
    {
        //Create a new Image element and add it to the root
        icon = new Image();
        Add(icon);
        //Add USS style properties to the elements
        icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");
        RegisterCallback<PointerDownEvent>(OnPointerDown);
        slotType = type;
    }
    
    public void HoldItem(ItemSO item)
    {
        icon.image = item.icon.texture;
        itemGuid = item.id;
        // Debug.Log("Test in HoldItem");
    }

    public void DropItem()
    {
        itemGuid = -1;
        icon.image = null;
    }
    
    private void OnPointerDown(PointerDownEvent evt)
    {
        //Not the left mouse button
        if (itemGuid == -1 || evt.button != 0)
        {
            return;
        }
        //Clear the image
        icon.image = null;
        //Start the drag
        InventoryUIController.StartDrag(evt.position, this);
    }

    public enum InventorySlotType
    {
        None,
        NormalInventory,
        EquipmentInventory,
        Trashcan
    }
    
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}


